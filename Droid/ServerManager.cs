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
using System.Collections.ObjectModel;

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
			await ParseUser.LogOutAsync ();
		}

		public bool IsAuthorized()
		{
			return ParseUser.CurrentUser != null;
		}

		async Task<ParseUser> GetUser(string name)
		{
			var query = ParseUser.Query.WhereEqualTo (ServerUserName, name);
			return await query.FirstOrDefaultAsync ();
		}

		public async Task Register(string name, string pass, UserRole role) {
			ParseUser _user = await GetUser(name);
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
		}

		public async Task AddCompany (string name) {
			// Добавление наименования компании в текущие настройки (для владельца компании)
			SettingsService.AddOrUpdateSetting(ServerCompanyName, name);

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

		private async Task<ParseObject> GetCompany(string name)
		{
			var query = ParseObject.GetQuery (ServerCompany).WhereEqualTo (ServerName, name);
			return await query.FirstOrDefaultAsync ();
		}

		public async Task<bool> FindCompany(string name)
		{
			return await GetCompany(name) != null;
		}

		public async Task<bool> IsUserJoinedToCompany(string companyName = "")
		{
			if (string.IsNullOrEmpty(companyName))
				companyName = SettingsService.GetSetting<string> (ServerCompanyName, "");

			// Прикрепляем НОВОГО пользователя (диспетчера/водителя) к указанной компании
			var query = ParseObject.GetQuery (ServerCompany).WhereEqualTo (ServerName, companyName);
			var company = await query.FirstOrDefaultAsync ();

			UserRole role = (UserRole)ParseUser.CurrentUser.Get<int> (ServerRole);
			ParseRelation<ParseUser> companyUsers = null;
			if (role == UserRole.UserRoleDispatch)
				companyUsers = company.GetRelation<ParseUser> (ServerDispatchers);
			else if (role == UserRole.UserRoleDriver)
				companyUsers = company.GetRelation<ParseUser> (ServerDrivers);

			var companyUser = await companyUsers.Query.WhereEqualTo (ServerUserName, ParseUser.CurrentUser.Username).FirstOrDefaultAsync ();

			// Если пользователь еще не был добавлен к компании
			if (companyUser == null) {
				ParseRelation<ParseUser> companyRequesting = company.GetRelation<ParseUser> (ServerRequesting);
				companyRequesting.Add (ParseUser.CurrentUser);
				await company.SaveAsync ();

				return false;
			} else
				return true;
		}

		public async Task<bool> RequestToJoinCompany (string name) {
			// Добавление наименования компании в текущие настройки (для диспетчеров и водителей)
			SettingsService.AddOrUpdateSetting (ServerCompanyName, name);

			return await IsUserJoinedToCompany (name);
		}

		public async void CheckRequests () {
			var query = ParseObject.GetQuery (ServerCompany).WhereEqualTo (ServerOwner, ParseUser.CurrentUser);
			ParseObject company = await query.FirstOrDefaultAsync ();
			var requestRelation = company.GetRelation<ParseUser> (ServerRequesting);
			IEnumerable<ParseUser> users = await requestRelation.Query.FindAsync ();
			foreach (ParseUser user in users) {
				Device.BeginInvokeOnMainThread (async delegate {
					UserRole role = (UserRole)user.Get<int> (ServerRole);

					bool answer = false;
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
			timerForRequst = new Timer (callback, null, 0, 15000);
		}

		public UserRole GetCurrentUserRole()
		{
			return (UserRole)(ParseUser.CurrentUser.Get<int>(ServerRole));
		}

		public string GetCurrentUserName()
		{
			return ParseUser.CurrentUser.Username;
		}

		public bool IsOwner()
		{
			return (UserRole)(ParseUser.CurrentUser.Get<int> (ServerRole)) == UserRole.UserRoleOwner;
		}

		public async Task SaveJob(string name, string description, string shipperAddress, 
			string receiveAddress, string driverName)
		{
			var job = new ParseObject (ServerJobClass);
			job [ServerName] = name;
			job [ServerDescription] = description;
			job [ServerShipperAddrress] = shipperAddress;
			job [ServerReceivedAddress] = receiveAddress;
			job [ServerCompleted] = false;

			ParseUser driver = await GetUser (driverName);
			job [ServerDriver] = driver;

			string companyName = SettingsService.GetSetting<string> (ServerCompanyName, "");
			job [ServerCompany] = await GetCompany (companyName);

			job [ServerCreateDispatcher] = ParseUser.CurrentUser;
			await job.SaveAsync();
		}

		public async Task<IList<Job>> GetJobList(string driverName = "")
		{
			var companyName = SettingsService.GetSetting<string>(ServerCompanyName, "");
			var compQuery = ParseObject.GetQuery (ServerCompany).WhereEqualTo (ServerCompanyName, companyName);
			ParseObject company = await compQuery.FirstOrDefaultAsync ();

			var query = ParseObject.GetQuery (ServerJobClass);
			// Фильтруем работы по компании
			if (company != null)
				query = query.WhereEqualTo (ServerCompany, company);
			
			if (GetCurrentUserRole()  == UserRole.UserRoleDriver)
				query = query.WhereEqualTo (ServerDriver, ParseUser.CurrentUser);

			var parseJobs = await query.FindAsync();

			ObservableCollection<Job> resultList = new ObservableCollection<Job> ();
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

		public async Task SaveDriverLocation(UserLocation location)
		{
			if (ParseUser.CurrentUser != null && this.GetCurrentUserRole() == UserRole.UserRoleDriver) {
				var point = new ParseGeoPoint (location.Latitude, location.Longitude);
				var user = ParseUser.CurrentUser;
				user [ServerLocation] = point;
				await user.SaveAsync ();
			} else
				await Task.FromResult (false);
		}

		async Task<IEnumerable<ParseUser>> GetUserList (UserRole requestRole)
		{
			string companyName = SettingsService.GetSetting<string> (ServerCompanyName, "");
			var query = ParseObject.GetQuery (ServerCompany).WhereEqualTo (ServerName, companyName);
			var company = await query.FirstOrDefaultAsync ();
			ParseRelation<ParseUser> userRelation;
			if (requestRole == UserRole.UserRoleDispatch)
				userRelation = company.GetRelation<ParseUser> (ServerDispatchers);
			else 
				userRelation = company.GetRelation<ParseUser> (ServerDrivers);

			IEnumerable<ParseUser> userEnum = await userRelation.Query.FindAsync ();

			return userEnum;
		}

		public async Task<IList<Trukman.User>> GetDispatchList()
		{
			var dispatchEnum = await GetUserList (UserRole.UserRoleDispatch);

			ObservableCollection<Trukman.User> userList = new ObservableCollection<User> (); // new List<User> ();
			foreach (var dispatch in dispatchEnum) {
				var user = new Trukman.User ();
				user.UserName = dispatch.Username;
				user.Email = dispatch.Email;
				user.Role = UserRole.UserRoleDispatch;

				userList.Add (user);
			}

			return userList;
		}

		public async Task<IList<Trukman.User>> GetDriverList()
		{
			var driverEnum = await GetUserList (UserRole.UserRoleDriver);

			ObservableCollection<Trukman.User> userList = new ObservableCollection<User> ();
			foreach (var driver in driverEnum) {
				var user = new Trukman.User ();
				user.UserName = driver.Username;
				user.Role = UserRole.UserRoleDriver;

				if (driver.Keys.Contains (ServerLocation)) {
					ParseGeoPoint point = (ParseGeoPoint)driver[ServerLocation]; 
					if (point.Longitude != 0 || point.Latitude != 0)
						user.location = new UserLocation {
							Latitude = point.Latitude,
							Longitude = point.Longitude,
							updatedAt = driver.UpdatedAt.GetValueOrDefault ()
						};
				}
				userList.Add (user);
			}

			return userList;
		}

		public async Task RemoveCompanyUser (User _user)
		{
			var companyName = SettingsService.GetSetting<string> (ServerCompanyName);
			var company = await GetCompany (companyName);
			ParseRelation<ParseUser> relation;
			if (_user.Role == UserRole.UserRoleDispatch)
				relation = company.GetRelation<ParseUser> (ServerDispatchers);
			else 
				relation = company.GetRelation<ParseUser> (ServerDrivers);

			ParseUser user = await GetUser (_user.UserName);
			relation.Remove (user);
			await company.SaveAsync ();
		}
	}
}