using System;
using Xamarin;
using System.Threading.Tasks;
using Parse;
using System.Collections.Generic;
using System.Threading;
using System.Collections;
using Xamarin.Forms;
using Trukman.Droid;
using Trukman.Droid.Helpers;

[assembly: Dependency(typeof(ServerManager))]

namespace Trukman.Droid
{
	public class ServerManager : IServerManager
	{
		static string ServerCompany = "Company";
		static string ServerUser = "_User";
		static string ServerName = "name";
		static string ServerUserName = "username";
		static string ServerRequesting = "requesting";
		static string ServerOwner = "owner";
		static string ServerRole = "role";
		static string ServerDispatchers = "dispatchers";
		static string ServerDrivers = "drivers";
		static string ServerLocation = "Location";
		static string ServerJobClass = "Job";

		static string ServerDescription = "description";
		static string ServerShipperAddrress = "ShipperAddress";
		static string ServerReceivedAddress = "ReceivedAddress";
		static string ServerDriver = "Driver";
		static string ServerCreateDispatcher = "createDispatcher";
		static string ServerCompleted = "Completed";
	
		static string ServerCompanyName = "CompanyName";

		Timer timerForRequst;

		UserRole _userRole;

		public ServerManager ()
		{
		}

		public void Init () {
			ParseClient.Initialize ("NsNjjvCGhqVKOZqCro2WOEr6gZHGTC9YlVB5jZqe", "WvSfa6MIvTb9L6BucGIiCQgV1zBc4OCR0UTS7D2L");
		}

		public async Task LogIn(string name, string pass) {
			await ParseUser.LogInAsync (name, pass);
		}

		public async Task LogOut()
		{
			//timerForRequst.Dispose ();
			await ParseUser.LogOutAsync ();
		}

		async Task<ParseUser> FindUser(string name)
		{
			var query = ParseUser.Query.WhereEqualTo (ServerUserName, name);
			return await query.FirstOrDefaultAsync ();
		}

		public async Task Register(string name, string pass, UserRole role) {
			ParseUser _user = await FindUser(name);
			if (_user == null) {
				ParseUser user = new ParseUser {
					Username = name,
					Password = pass
				};
				user [ServerRole] = (int)role;
				await user.SignUpAsync ();
			} else {
				await this.LogIn (name, pass);
			}
			_userRole = role;
		}

		public async Task AddCompany (string name) {
			// Добавление наименования компании в текущие настройки (для владельца компании)
			SettingsService.AddOrUpdateSetting(ServerCompanyName, name);

			/*var session = await ParseSession.GetCurrentSessionAsync ();
			string currCompanyName = "";
			// На всякий случай удаляем из сессии название компании и пишем новое
			if (session.TryGetValue (ServerCompanyName, out currCompanyName))
				session.Remove (ServerCompanyName);
			session.Add (ServerCompanyName, name);*/

			var query = ParseObject.GetQuery (ServerCompany).WhereEqualTo (ServerName, name);
			ParseObject company = await query.FirstOrDefaultAsync ();
			if (company == null) {
				company = new ParseObject (ServerCompany);
				company [ServerName] = name;
				company [ServerOwner] = ParseUser.CurrentUser;
				await company.SaveAsync ();
			} else
				await Task.FromResult (false);
		}

		public async Task<Boolean> RequestToJoinCompany (string name) {
			// Добавление наименования компании в текущие настройки (для диспетчеров и водителей)
			SettingsService.AddOrUpdateSetting(ServerCompanyName, name);

			/*var session = await ParseSession.GetCurrentSessionAsync ();
			string currCompanyName = "";
			// На всякий случай удаляем из сессии название компании и пишем новое
			if (session.TryGetValue (ServerCompanyName, out currCompanyName))
				session.Remove (ServerCompanyName);
			session.Add (ServerCompanyName, name);*/

			// Прикрепляем НОВОГО пользователя (диспетчера/водителя) к указанной компании
			var query = ParseObject.GetQuery (ServerCompany).WhereEqualTo (ServerName, name);
			var company = await query.FirstOrDefaultAsync ();
			if (company != null) {
				UserRole role = (UserRole)ParseUser.CurrentUser.Get<int> (ServerRole);
				ParseRelation<ParseUser> companyUsers = null;
				if (role == UserRole.UserRoleDispatch)
					companyUsers = company.GetRelation<ParseUser> (ServerDispatchers);
				else if (role == UserRole.UserRoleDriver)
					companyUsers = company.GetRelation<ParseUser> (ServerDrivers);

				var companyUser = await companyUsers.Query.WhereEqualTo (ServerUserName, ParseUser.CurrentUser.Username).FirstOrDefaultAsync ();

				//if (ParseUser.CurrentUser.IsNew) {
				// Если пользователь еще не был добавлен к компании
				if (companyUser == null) {
					ParseRelation<ParseUser> companyRequesting = company.GetRelation<ParseUser> (ServerRequesting);
					companyRequesting.Add (ParseUser.CurrentUser);
					await company.SaveAsync ();

					Device.BeginInvokeOnMainThread (async delegate {
						await AlertHandler.ShowAlert (string.Format("The owner of the company {0} has not yet added you to the company", name));
					});
						
					return false;
				} else
					return true;
			} else {
				Device.BeginInvokeOnMainThread (async delegate {
					await AlertHandler.ShowCheckRequestCompany (name);
				});
				return false;
			}
		}

		public async void CheckRequests () {
			var query = ParseObject.GetQuery (ServerCompany).WhereEqualTo (ServerOwner, ParseUser.CurrentUser);
			ParseObject company = await query.FirstOrDefaultAsync ();
			var requestRelation = company.GetRelation<ParseUser> (ServerRequesting);
			IEnumerable<ParseUser> users = await requestRelation.Query.FindAsync ();
			foreach (ParseUser user in users) {
				Device.BeginInvokeOnMainThread (async delegate {
					UserRole role = (UserRole)user.Get<int> (ServerRole);

					Boolean answer = false;
					if (role == UserRole.UserRoleDispatch)
						answer = await AlertHandler.ShowCheckDispatch (user.Username);
					else if (role == UserRole.UserRoleDriver)
						answer = await AlertHandler.ShowCheckDriver (user.Username);
					if (answer) {
						requestRelation.Remove (user);
						if (role == UserRole.UserRoleDispatch) {
							var dispatchRelation = company.GetRelation<ParseObject> (ServerDispatchers);
							dispatchRelation.Add (user);
							await company.SaveAsync ();
						} else if (role == UserRole.UserRoleDriver) {
							var driverRelation = company.GetRelation<ParseObject> (ServerDrivers);
							driverRelation.Add (user);
							await company.SaveAsync ();
						}
					} else {
						requestRelation.Remove (user);
					}
				});
			}
		}

		void TimerForRequestFires (object state) {
			CheckRequests ();
		}

		public void StartTimerForRequest () {
			TimerCallback callback = new TimerCallback (TimerForRequestFires);
			timerForRequst = new Timer (callback, null, 0, 5000);
			//CheckRequests ();
		}

/*		public void DisposeTimerForRequest()
		{
			if (timerForRequst != null)
				timerForRequst.Dispose ();
		}*/

		public UserRole GetCurrentUserRole()
		{
			return _userRole;
		}

		public string GetCurrentUserName()
		{
			return ParseUser.CurrentUser.Username;
		}

		async Task<ParseObject> GetCompany(string name)
		{
			var query = ParseObject.GetQuery(ServerCompany).
				WhereEqualTo(ServerName, name);
			return await query.FirstOrDefaultAsync ();
		}

		public async Task SaveJob(string name, string description, string shipperAddress, 
			string receiveAddress, string driverName) //, string company)
		{
			var job = new ParseObject (ServerJobClass);
			job [ServerName] = name;
			job [ServerDescription] = description;
			job [ServerShipperAddrress] = shipperAddress;
			job [ServerReceivedAddress] = receiveAddress;
			job [ServerCompleted] = false;
			ParseUser driver = await FindUser (driverName);
			job [ServerDriver] = driver;
			//string company = (string)ParseUser.CurrentUser [ServerCompany];
			//job [ServerCompany] = await GetCompany (company);
			job [ServerCreateDispatcher] = ParseUser.CurrentUser;
			await job.SaveAsync();
		}

		public async Task<List<Job>> GetJobList(string companyName = "", string driverName = "")
		{
			var query = ParseObject.GetQuery (ServerJobClass);
			if (!string.IsNullOrEmpty (companyName))
				query = query.WhereEqualTo (ServerCompany, companyName);
			if (_userRole == UserRole.UserRoleDriver)
				query = query.WhereEqualTo (ServerDriver, ParseUser.CurrentUser);

			var parseJobs = await query.FindAsync();

			List<Job> resultList = new List<Job> ();
			foreach (var parseObj in parseJobs) 
			{
				//ParseObject parseObj = jobEnum.Current;
				Job job = new Job ();
				job.Name = (string)parseObj[ServerName];
				job.Description = (string)parseObj [ServerDescription];
/*				job [ServerShipperAddrress] = shipperAddress;
				job [ServerReceivedAddress] = receiveAddress;
				job [ServerCompleted] = false;
				job [ServerDriver] = this.FindUser (driver);
				//string company = (string)ParseUser.CurrentUser [ServerCompany];
				//job [ServerCompany] = this.GetCompany (company);
				job [ServerCreateDispatcher] = ParseUser.CurrentUser;*/
				resultList.Add (job);
			}
			return resultList;
		}

		public async Task SaveDriverLocation(GPSLocation location)
		{
			if (ParseUser.CurrentUser != null && _userRole == UserRole.UserRoleDriver) {
				var point = new ParseGeoPoint (location.Latitude, location.Longitude);
				var user = ParseUser.CurrentUser;
				user [ServerLocation] = point;
				await user.SaveAsync ();
			} else
				await Task.FromResult (false);
		}

		public async Task<List<Driver>> GetDriverList()
		{
			List<Driver> drivers = new List<Driver> ();
			/*var session = await ParseSession.GetCurrentSessionAsync();
			string companyName = "";
			companyName = session.Get<string>(ServerCompanyName);*/
			string companyName = SettingsService.GetSetting<string> (ServerCompanyName, "");
			var query = ParseObject.GetQuery (ServerCompany).WhereEqualTo (ServerName, companyName);
			var company = await query.FirstOrDefaultAsync ();
			if (company != null) {
				ParseRelation<ParseUser> driversRelation = company.GetRelation<ParseUser> (ServerDrivers);
				IEnumerable<ParseUser> parseDrivers = await driversRelation.Query.FindAsync ();

			//var query = ParseUser.Query.WhereEqualTo("role", (int)UserRole.UserRoleDriver);
			//var parseDrivers = await query.FindAsync ();

				foreach (var parseDriver in parseDrivers) {
					var driver = new Driver ();
					driver.Name = parseDriver.Username;
					if (parseDriver.Keys.Contains (ServerLocation)) {
						ParseGeoPoint point = (ParseGeoPoint)parseDriver [ServerLocation]; 
						if (point.Longitude != 0 || point.Latitude != 0)
							driver.location = new GPSLocation{ Latitude = point.Latitude, Longitude = point.Longitude };
					}
					drivers.Add (driver);
				}
			}

			return drivers;
		}
	}
}