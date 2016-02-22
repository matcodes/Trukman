using System;
using Xamarin;
using System.Threading.Tasks;
using Parse;
using System.Collections.Generic;
using System.Threading;
using System.Collections;

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
			await ParseUser.LogOutAsync ();
		}

		async Task<ParseUser> FindUser(string name)
		{
			var query = ParseUser.Query.WhereEqualTo (ServerUserName, name);
			return await query.FirstOrDefaultAsync ();
		}

		public async Task Register(string name, string pass, UserRole role) {
			ParseObject _user = await FindUser(name);
			if (_user == null) {
			//if (!ParseUser.CurrentUser.IsAuthenticated) {
				ParseUser user = new ParseUser {
					Username = name,
					Password = pass
				};
				user [ServerRole] = (int)role;
				await user.SignUpAsync ();
			} else
				//Task.FromResult (false);
				await this.LogIn (name, pass);

			_userRole = role;
		}

		public async Task AddCompany (string name) {
			var query = ParseObject.GetQuery (ServerCompany)
				.WhereEqualTo (ServerName, name);
			IEnumerator<ParseObject> companyEnum = (await query.FindAsync ()).GetEnumerator ();
			companyEnum.MoveNext ();
			ParseObject company = companyEnum.Current;
			if (company == null) {
				company = new ParseObject (ServerCompany);
				company [ServerName] = name;
				company [ServerOwner] = ParseUser.CurrentUser;
				await company.SaveAsync ();
			} else
				await Task.FromResult (false);
		}

		public async Task RequestToJoinCompany (string name) {
			var query = ParseObject.GetQuery(ServerCompany)
				.WhereEqualTo(ServerName, name);
			IEnumerator<ParseObject> companyEnum = (await query.FindAsync()).GetEnumerator();
			companyEnum.MoveNext();
			ParseObject company = companyEnum.Current;
			if (company != null) {
				ParseRelation<ParseUser> companyRequesting = company.GetRelation<ParseUser> (ServerRequesting);
				companyRequesting.Add (ParseUser.CurrentUser);
				await company.SaveAsync ();
			}
		}

		public async void CheckRequests () {
			var query = ParseObject.GetQuery (ServerCompany)
				.WhereEqualTo (ServerOwner, ParseUser.CurrentUser);
			ParseObject company = await query.FirstOrDefaultAsync ();
			var requestRelation = company.GetRelation<ParseUser> (ServerRequesting);
			IEnumerable<ParseUser> users = await requestRelation.Query.FindAsync ();
			foreach (ParseUser user in users) {
				//NSObject excecuter = new NSObject ();
				Action action = async delegate {
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
							var dispatchRelation = company.GetRelation<ParseObject> (ServerDrivers);
							dispatchRelation.Add (user);
							await company.SaveAsync ();
						}
					} else {
						requestRelation.Remove (user);
					}
				};
				action ();
			}
		}

		void TimerForRequestFires (object state) {
			CheckRequests ();
		}

		public void StartTimerForRequest () {
			// TODO: код запускается по таймеру. Надо разобраться, для чего нужен здесь таймер
			/*TimerCallback callback = new TimerCallback (TimerForRequestFires);
			timerForRequst = new Timer (callback, null, 0, 5000);*/
			CheckRequests ();
		}

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
			string receiveAddress, string driver) //, string company)
		{
			var job = new ParseObject (ServerJobClass);
			job [ServerName] = name;
			job [ServerDescription] = description;
			job [ServerShipperAddrress] = shipperAddress;
			job [ServerReceivedAddress] = receiveAddress;
			job [ServerCompleted] = false;
			job [ServerDriver] = this.FindUser (driver);
			//string company = (string)ParseUser.CurrentUser [ServerCompany];
			//job [ServerCompany] = this.GetCompany (company);
			job [ServerCreateDispatcher] = ParseUser.CurrentUser;
			await job.SaveAsync();
		}

		public List<Job> GetJobList(string companyName = "", string driverName = "")
		{
			var query = ParseObject.GetQuery (ServerJobClass);
			if (!string.IsNullOrEmpty (companyName))
				query = query.WhereEqualTo (ServerCompany, companyName);
/*			if (!string.IsNullOrEmpty(driverName))
			{*/
				if (_userRole == UserRole.UserRoleDriver)

				//ParseObject driver = FindUser (driverName);
				query = query.WhereEqualTo (ServerDriver, ParseUser.CurrentUser);
			//}

			//IEnumerator<ParseObject> jobEnum = (await query.FindAsync()).GetEnumerator();
			IEnumerable<ParseObject> jobList = query.FindAsync().Result;

			List<Job> resultList = new List<Job> ();
			foreach (var parseObj in jobList) 
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

		/*public IEnumerable GetDriversInternal()
		{
			try{
			var query = ParseUser.Query.
				WhereEqualTo("role", (long)UserRole.UserRoleDriver);
				return query.FindAsync ().Result;
			
			}
			catch(Exception exc) {
				throw new Exception ();
			}
		}*/

		public List<Driver> GetDriverList(string companyName = "")
		{
			var query = ParseUser.Query.
				WhereEqualTo("role", (int)UserRole.UserRoleDriver);
			var parseDrivers = query.FindAsync ().Result;
			List<Driver> drivers = new List<Driver> ();
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

			return drivers;
		}
	}
}