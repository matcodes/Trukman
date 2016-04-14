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
using Trukman.Data.Interfaces;
using Trukman.Data.Classes;

[assembly: Dependency(typeof(ServerManager))]

namespace Trukman.Droid
{
	public class ServerManager : IServerManager
	{
		static string ServerCompany = "Company";
		//static string ServerUser = "_User";
		static string ServerName = "name";
		static string ServerUserName = "username";
		static string ServerRequesting = "requesting";
		static string ServerOwner = "owner";
		static string ServerRole = "role";
		static string ServerDispatchers = "dispatchers";
		static string ServerDrivers = "drivers";
		static string ServerLocation = "Location";
		//static string ServerDispatch = "Dispatch";
		static string ServerJobClass = "Job";

		static string ServerObjectId = "objectId";
		static string ServerDescription = "description";
		static string ServerFromAddress = "FromAddress";
		static string ServerToAddress = "ToAddress";
		static string ServerDriver = "Driver";
		static string ServerCreateDispatcher = "createDispatcher";
		static string ServerJobCompleted = "JobCompleted";
		static string ServerPickupDatetime = "PickupDatetime";
		static string ServerDeliveryDatettime = "DeliveryDatetime";
		//static string ServerDriverOnTimePuckup = "DriverOnTimePuckup";
		//static string ServerDriverOnTimeDelivery = "DriverOnTimeDelivery";
		static string ServerJobPrice = "Price";
		//static string ServerDriverAccepted = "DriverAccepted";
		//static string ServerDeclineReason = "DeclineReason";
		static string ServerJobCancelled = "JobCancelled";

		static string ServerComcheckRequest = "ComcheckRequest";
		static string ServerState = "State";
		static string ServerRequestDatetime = "RequestDatetime";
		static string ServerRequestType = "RequestType";
		static string ServerComcheck = "Comcheck";

		//static string CompanyName = "CompanyName";
		//static string RejectedCounter = "RejectedCounter";
		//static string LastRejectedTime = "LastRejectedTime";
		//static string CurrentJobId = "CurrentJobId";
		//static string FuelId = "FuelId";
		//static string Lumper = "Lumper";
		//static int MaxRejectedRequestCount = 3;

		Timer timerForRequest;

		public ServerManager ()
		{
			ParseClient.Initialize ("NsNjjvCGhqVKOZqCro2WOEr6gZHGTC9YlVB5jZqe", "WvSfa6MIvTb9L6BucGIiCQgV1zBc4OCR0UTS7D2L");
		}

		public async Task LogIn(string name, string pass) {
			await ParseUser.LogInAsync (name, pass);
		}

		public async Task LogOut()
		{
			await ParseUser.LogOutAsync ();
		}

		public bool IsAuthorizedUser()
		{
			return (ParseUser.CurrentUser != null);
		}

		async Task<ParseUser> GetUser(string name)
		{
			var query = ParseUser.Query.WhereEqualTo (ServerUserName, name);
			return await query.FirstOrDefaultAsync ();
		}

		public async Task Register(string name, string pass, UserRole role) 
		{
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

		public async Task AddCompany (string name, string DBA, string address, string phone, string email, string fleetSize) 
		{
			// Добавление наименования компании в текущие настройки (для владельца компании)
			//SettingsService.AddOrUpdateSetting(CompanyName, name);

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

		async Task<ParseObject> GetCompany(string name)
		{
			var query = ParseObject.GetQuery (ServerCompany).WhereEqualTo (ServerName, name);
			return await query.FirstOrDefaultAsync ();
		}

		public async Task<bool> FindCompany(string name)
		{
			return await GetCompany(name) != null;
		}

		async Task<bool> IsUserJoinedToCompany (ParseObject company)
		{
			UserRole role = (UserRole)ParseUser.CurrentUser.Get<int> (ServerRole);
			ParseRelation<ParseUser> companyUsers = null;
			if (role == UserRole.UserRoleDispatch)
				companyUsers = company.GetRelation<ParseUser> (ServerDispatchers);
			else if (role == UserRole.UserRoleDriver)
				companyUsers = company.GetRelation<ParseUser> (ServerDrivers);
			var companyUser = await companyUsers.Query.WhereEqualTo (ServerUserName, ParseUser.CurrentUser.Username).FirstOrDefaultAsync ();

			return (companyUser != null);
		}

		async Task<bool> IsUserRequestingCompany(ParseObject company)
		{
			ParseRelation<ParseUser> requesting = company.GetRelation<ParseUser> (ServerRequesting);
			var requestingUser = await requesting.Query.WhereEqualTo (ServerUserName, ParseUser.CurrentUser.Username).FirstOrDefaultAsync();
			return (requestingUser != null);
		}

		/*public bool IsFrozenAuthorization()
		{
			int counterValue = SettingsService.GetSetting<int> (RejectedCounter, 0);
			DateTime lastRejectedTime = SettingsService.GetSetting<DateTime> (LastRejectedTime, DateTime.MinValue);
			int hours = (DateTime.Now - lastRejectedTime).Hours;

			// Последняя неудачная попытка была более 24-х часов назад, обнуляем счетчик
			if (hours >= 24) {
				counterValue = 0;
				SettingsService.AddOrUpdateSetting<int> (RejectedCounter, counterValue);
			}

			// Вход для пользователя заморожен
			if (counterValue >= MaxRejectedRequestCount)
				return true;

			return false;
		}*/

		public async Task<AuthorizationRequestStatus> GetAuthorizationStatus(string companyName)
		{
			//int counterValue = SettingsService.GetSetting<int> (RejectedCounter, 0);

			//if (string.IsNullOrEmpty(companyName))
			//	companyName = SettingsService.GetSetting<string> (CompanyName, "");

			//var query = ParseObject.GetQuery (ServerCompany).WhereEqualTo (ServerName, companyName);
			var company = await GetCompany(companyName); // query.FirstOrDefaultAsync ();

			if (await IsUserJoinedToCompany (company)) {
				//counterValue = 0;
				//SettingsService.AddOrUpdateSetting (RejectedCounter, counterValue);

				return AuthorizationRequestStatus.Authorized;
			}
			else {
				if (await IsUserRequestingCompany (company))
					return AuthorizationRequestStatus.Pending;
				// Отклонена авторизация
				else {
					//counterValue++;
					//SettingsService.AddOrUpdateSetting (RejectedCounter, counterValue);
					//SettingsService.AddOrUpdateSetting (LastRejectedTime, DateTime.Now);

					/*if (counterValue >= MaxRejectedRequestCount)
						return AuthorizationRequestStatus.Frozen;
					else {
						SettingsService.AddOrUpdateSetting (LastRejectedTime, DateTime.Now);
					*/
					return AuthorizationRequestStatus.Declined;
					//}
				}
			}
		}

		async void AddUserToRequesting (ParseObject company)
		{
			ParseRelation<ParseUser> companyRequesting = company.GetRelation<ParseUser> (ServerRequesting);
			companyRequesting.Add (ParseUser.CurrentUser);
			await company.SaveAsync ();
		}

		public async Task<bool> RequestToJoinCompany (string name) {
			// Добавление наименования компании в текущие настройки (для диспетчеров и водителей)
			//SettingsService.AddOrUpdateSetting (CompanyName, name);

			var company = await GetCompany (name);

			var joined = await IsUserJoinedToCompany(company);
			// Если пользователь еще не был добавлен к компании
			// Прикрепляем НОВОГО пользователя (диспетчера/водителя) к указанной компании
			if (!joined) {
				AddUserToRequesting (company);
			}

			return joined;
		}

		// TODO: перенести в бизнес-логику 
		public async void CheckRequests () {
			var query = ParseObject.GetQuery (ServerCompany).WhereEqualTo (ServerOwner, ParseUser.CurrentUser);
			ParseObject company = await query.FirstOrDefaultAsync ();
			var requestRelation = company.GetRelation<ParseUser> (ServerRequesting);
			IEnumerable<ParseUser> users = await requestRelation.Query.FindAsync ();
			foreach (ParseUser user in users) {
				Device.BeginInvokeOnMainThread (async delegate {
					UserRole role = (UserRole)user.Get<int> (ServerRole);

					bool answer = false;
					/*if (role == UserRole.UserRoleDispatch)
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
					} 
					// Отклонена авторизация
					else {
						// Удаляем пользователя из класса User
						await user.DeleteAsync();
						requestRelation.Remove (user);
						await company.SaveAsync();
					}*/
				});
			}
		}

		void TimerForRequestFires (object state) {
			CheckRequests ();
		}

		public void StartTimerForRequest () {
			TimerCallback callback = new TimerCallback (TimerForRequestFires);
			timerForRequest = new Timer (callback, null, 0, 5* 15000);
		}

		public UserRole GetCurrentUserRole()
		{
			return (UserRole)(ParseUser.CurrentUser.Get<int>(ServerRole));
		}

		public string GetCurrentUserName()
		{
			return ParseUser.CurrentUser.Username;
		}

		/*public string GetCurrentCompanyName()
		{
			return SettingsService.GetSetting<string> (CompanyName, "");
		}*/

		public bool IsOwner()
		{
			return (ParseUser.CurrentUser.Get<int> (ServerRole) == 0);
		}

		public async Task SaveJob(string name, string description, string shipperAddress, 
			string receiveAddress, string driverName, string company)
		{
			var job = new ParseObject (ServerJobClass);
			job [ServerName] = name;
			job [ServerDescription] = description;
			job [ServerFromAddress] = shipperAddress;
			job [ServerToAddress] = receiveAddress;
			job [ServerJobCompleted] = false;

			ParseUser driver = await GetUser (driverName);
			job [ServerDriver] = driver;

			//string companyName = SettingsService.GetSetting<string> (CompanyName, "");
			job [ServerCompany] = await GetCompany (company);

			job [ServerCreateDispatcher] = ParseUser.CurrentUser;
			await job.SaveAsync();
		}

		/*public async Task<IList<Job>> GetJobList(string company)
		{
			//var companyName = SettingsService.GetSetting<string>(CompanyName, "");
			ParseObject companyData = await GetCompany (company);

			var query = ParseObject.GetQuery (ServerJobClass);
			// Фильтруем работы по компании
			if (companyData != null)
				query = query.WhereEqualTo (ServerCompany, company);

			if (GetCurrentUserRole () == UserRole.UserRoleDriver)
				query = query.WhereEqualTo (ServerDriver, ParseUser.CurrentUser);

			var parseList = await query.FindAsync();

			ObservableCollection<Job> resultList = new ObservableCollection<Job> ();
			foreach (var parseData in parseList) 
			{
				var job = ConvertParseJob (parseData);
				resultList.Add (job);
			}
			return resultList;
		}

		Job ConvertParseJob (ParseObject parseData)
		{
			Job job = new Job ();
			job.Id = parseData.ObjectId;
			job.Name = (string)parseData [ServerName];
			job.Description = (string)parseData [ServerDescription];
			job.ShipperAddress = (string)parseData [ServerFromAddress];
			job.ReceiverAddress = (string)parseData [ServerToAddress];
			job.PickupDateTime = (DateTime)parseData [ServerPickupDatetime];
			job.DeliveryDateTime = (DateTime)parseData [ServerDeliveryDatettime];
			job.Price = (int)parseData [ServerJobPrice];
			job.Cancelled = (bool)parseData [ServerJobCancelled];
			//job [ServerDriver] = this.FindUser (driver);
			return job;
		}*/

		/*public async Task<Job> GetCurrentDriverJob(string currentJobId)
		{
			//string currentJobId = SettingsService.GetSetting<string> (CurrentJobId, "");
			ParseObject parseData;
			if (string.IsNullOrEmpty (currentJobId)) {
				var query = ParseObject.GetQuery (ServerJobClass);
				if (GetCurrentUserRole () == UserRole.UserRoleDriver)
					query = query.WhereEqualTo (ServerDriver, ParseUser.CurrentUser);
				// Берем ближайшую по времени запись из Job, не принятую, не отмененную 
				parseData = await query.WhereEqualTo (ServerDriverAccepted, false)
					.WhereEqualTo (ServerJobCancelled, false)
					.WhereGreaterThan (ServerPickupDatetime, DateTime.Now)
					.OrderBy (ServerPickupDatetime)
					.FirstOrDefaultAsync ();

				currentJobId = parseData.ObjectId;
				SettingsService.AddOrUpdateSetting (CurrentJobId, currentJobId);
			}
			else
			{
				parseData = await ParseObject.GetQuery (ServerJobClass)
					.WhereEqualTo (ServerObjectId, currentJobId)
					.FirstOrDefaultAsync ();
			}

			var job = ConvertParseJob (parseData);
			if (job.Cancelled) {
				SettingsService.AddOrUpdateSetting<string> (CurrentJobId, null);
			}

			return job;
		}*/

		public async Task SaveDriverLocation(IUserLocation location)
		{
			if (ParseUser.CurrentUser != null && this.GetCurrentUserRole() == UserRole.UserRoleDriver) {
				var point = new ParseGeoPoint (location.Latitude, location.Longitude);
				var user = ParseUser.CurrentUser;
				user [ServerLocation] = point;
				await user.SaveAsync ();
			} else
				await Task.FromResult (false);
		}

		async Task<IEnumerable<ParseUser>> GetUserList (string companyName, UserRole  requestRole)
		{
			//string companyName = SettingsService.GetSetting<string> (CompanyName, "");
			//var query = ParseObject.GetQuery (ServerCompany).WhereEqualTo (ServerName, companyName);
			var company = await GetCompany(companyName); // await query.FirstOrDefaultAsync ();
			ParseRelation<ParseUser> userRelation;
			if (requestRole == UserRole.UserRoleDispatch)
				userRelation = company.GetRelation<ParseUser> (ServerDispatchers);
			else 
				userRelation = company.GetRelation<ParseUser> (ServerDrivers);

			IEnumerable<ParseUser> userEnum = await userRelation.Query.FindAsync ();

			return userEnum;
		}

		public async Task<IList<IUser>> GetDispatchList(string companyName)
		{
			var dispatchEnum = await GetUserList (companyName, UserRole.UserRoleDispatch);

			ObservableCollection<IUser> userList = new ObservableCollection<IUser> (); // new List<User> ();
			foreach (var dispatch in dispatchEnum) {
				var user = new User ();
				user.UserName = dispatch.Username;
				user.Email = dispatch.Email;
				user.Role = UserRole.UserRoleDispatch;

				userList.Add (user);
			}

			return userList;
		}

		public async Task<IList<IUser>> GetDriverList(string companyName)
		{
			var driverEnum = await GetUserList (companyName, UserRole.UserRoleDriver);

			ObservableCollection<IUser> userList = new ObservableCollection<IUser> ();
			foreach (var driver in driverEnum) {
				var user = new User ();
				user.UserName = driver.Username;
				user.Role = UserRole.UserRoleDriver;

				if (driver.Keys.Contains (ServerLocation)) {
					ParseGeoPoint point = (ParseGeoPoint)driver[ServerLocation]; 
					if (point.Longitude != 0 || point.Latitude != 0)
						user.location = new UserLocation {
						Latitude = point.Latitude,
						Longitude = point.Longitude,
						//updatedAt = driver.UpdatedAt.GetValueOrDefault ()
					};
				}
				userList.Add (user);
			}

			return userList;
		}

		public async Task RemoveCompanyUser (string companyName, IUser _user)
		{
			//var companyName = SettingsService.GetSetting<string> (CompanyName);
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

		public async Task SendComcheckRequest(ComcheckRequestType RequestType)
		{
			ParseObject parseData = new ParseObject (ServerComcheckRequest);
			parseData [ServerDriver] = ParseUser.CurrentUser;
			//parseData[ServerDispatch] = 
			parseData [ServerState] = (int)ComcheckRequestState.Requested;
			parseData [ServerRequestDatetime] = DateTime.Now;
			parseData [ServerRequestType] = (int)RequestType;

			await parseData.SaveAsync ();

			string id = parseData.ObjectId;
			//SettingsService.AddOrUpdateSetting<string> (FuelId, id);
		}

/*		public async Task<ComcheckRequest> GetComcheckRequest (ComcheckRequestType RequestType)
		{
			string id = "";
			if (!string.IsNullOrEmpty (id)) {
				var data = await ParseObject.GetQuery (ServerComcheckRequest)
					.WhereEqualTo (ServerObjectId, id)
					.FirstOrDefaultAsync ();

				ComcheckRequest request = ConvertComcheckRequest(data);

				return request;
			} else
				return null;
		}

		ComcheckRequest ConvertComcheckRequest(ParseObject parseData)
		{
			var request = new ComcheckRequest ();
			//request.Driver
			//request.Dispatch
			request.State = (ComcheckRequestState)((int)parseData[ServerState]);
			request.RequestDatetime = (DateTime)parseData[ServerRequestDatetime];
			request.RequestType = (ComcheckRequestType)((int)parseData [ServerRequestType]);
			if (request.State == ComcheckRequestState.Visible)
				request.Comcheck = (string)parseData [ServerComcheck];

			return request;
		}*/
	}
}