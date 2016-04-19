using System;
using Xamarin;
using System.Threading.Tasks;
using Parse;
using System.Collections.Generic;
using System.Threading;
using System.Collections;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Trukman.Helpers;
using Trukman.Interfaces;
using KAS.Trukman.Data.Interfaces;
using KAS.Trukman.Data.Classes;
using Trukman.Droid.Helpers;
using KAS.Trukman;
using Xamarin.Forms.Maps;
using Trukman.Droid.ParseClasses;
using System.Linq;
using Trukman.Classes;

[assembly: Dependency(typeof(ServerManager))]

namespace Trukman.Droid.Helpers
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
		static string ServerDriverOnTimePickup = "DriverOnTimePickup";
		static string ServerDriverOnTimeDelivery = "DriverOnTimeDelivery";
		static string ServerJobPrice = "Price";
		static string ServerDriverAccepted = "DriverAccepted";
		static string ServerDeclineReason = "DeclineReason";
		static string ServerJobCancelled = "JobCancelled";

		static string ServerComcheckRequest = "ComcheckRequest";
		static string ServerState = "State";
		static string ServerRequestDatetime = "RequestDatetime";
		static string ServerRequestType = "RequestType";

		static string ServerGeoLocation = "GeoLocation";
		static string ServerPointCreatedAt = "PointCreatedAt";
		static string ServerLocationHistory = "LocationHistory";

		static string ServerComcheckDispatch = "Dispatch";
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
			ParseObject.RegisterSubclass<Alerts> ();
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
			name = name.ToLower ();
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
			name = name.ToLower ();
			
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
			var companyData = await query.FirstOrDefaultAsync ();
			return companyData;
		}

		public async Task<bool> FindCompany(string name)
		{
			return await GetCompany(name.ToLower()) != null;
		}

		private async Task<bool> IsUserJoinedToCompany (ParseObject company)
		{
			UserRole role = (UserRole)ParseUser.CurrentUser.Get<int> (ServerRole);
			ParseRelation<ParseUser> companyUsers = null;
			if (role == UserRole.UserRoleDispatch)
				companyUsers = company.GetRelation<ParseUser> (ServerDispatchers);
			else if (role == UserRole.UserRoleDriver)
				companyUsers = company.GetRelation<ParseUser> (ServerDrivers);
			var companyUser = await companyUsers.Query.WhereEqualTo (ServerUserName, ParseUser.CurrentUser.Username).FirstOrDefaultAsync ();

			if (companyUser != null) {
				ParseRelation<ParseUser> requestRelation = company.GetRelation<ParseUser> (ServerRequesting);
				requestRelation.Remove (companyUser);
				await company.SaveAsync();
			}

			return (companyUser != null);
		}

		private async Task<bool> IsUserRequestingCompany(ParseObject company)
		{
			ParseRelation<ParseUser> requesting = company.GetRelation<ParseUser> (ServerRequesting);
			var requestingUser = await requesting.Query.WhereEqualTo (ServerUserName, ParseUser.CurrentUser.Username).FirstOrDefaultAsync();
			return (requestingUser != null);
		}

		public async Task<AuthorizationRequestStatus> GetAuthorizationStatus(string companyName)
		{
			companyName = companyName.ToLower ();

			var company = await GetCompany(companyName); 

			if (await IsUserJoinedToCompany (company)) {
				// Пользователь авторизован
				return AuthorizationRequestStatus.Authorized;
			}
			else {
				// Ожидание авторизации
				if (await IsUserRequestingCompany (company))
					return AuthorizationRequestStatus.Pending;
				// Отклонена авторизация
				else {
					return AuthorizationRequestStatus.Declined;
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
			name = name.ToLower ();

			//var query = new ParseQuery
			var query = ParseObject.GetQuery (ServerCompany).WhereEqualTo (ServerName, name);

			var company = await GetCompany (name);

			var joined = await IsUserJoinedToCompany(company);
			// Если пользователь еще не был добавлен к компании
			// Прикрепляем НОВОГО пользователя (диспетчера/водителя) к указанной компании
			if (!joined) {
				AddUserToRequesting (company);
			}

			return joined;
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
					{
						//answer = await AlertHandler.ShowCheckDispatch (user.Username);
					}
					else if (role == UserRole.UserRoleDriver)
					{
						//answer = await AlertHandler.ShowCheckDriver (user.Username);
					}
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
					}
				});
			}
		}

		private void TimerForRequestFires (object state) {
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

		public bool IsOwner()
		{
			return (UserRole)(ParseUser.CurrentUser.Get<int> (ServerRole)) == UserRole.UserRoleOwner;
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

			driverName = driverName.ToLower ();
			ParseUser driver = await GetUser (driverName);
			job [ServerDriver] = driver;

			//string companyName = SettingsService.GetSetting<string> (CompanyName, "");
			job [ServerCompany] = await GetCompany (company);

			job [ServerCreateDispatcher] = ParseUser.CurrentUser;
			await job.SaveAsync();
		}

		private T GetField<T>(ParseObject data, string field)
		{
			if (data.Keys.Contains (field))
				return (T)data [field];
			else
				return default(T);
		}

		private async Task<ParseObject> GetTrip (string TripId)
		{
			if (!string.IsNullOrEmpty (TripId)) {
				var parseData = await ParseObject.GetQuery (ServerJobClass).WhereEqualTo (ServerObjectId, TripId).FirstOrDefaultAsync ();
				return parseData;
			} else
				return null;
		}

		public async Task<ITrip> GetNewOrCurrentTrip(string currentTripId = "")
		{
			//if (GetCurrentUserRole () == UserRole.UserRoleDriver)

			ParseObject parseData;
			if (string.IsNullOrEmpty (currentTripId)) {			
				var query = ParseObject.GetQuery (ServerJobClass)
					.WhereEqualTo (ServerDriver, ParseUser.CurrentUser)
				// Берем ближайшую по времени запись из Job, не принятую, не отмененную
					//parseData = await query
					.WhereNotEqualTo (ServerDriverAccepted, false)
					.WhereNotEqualTo (ServerJobCancelled, true)
					.WhereNotEqualTo (ServerJobCompleted, true)
					.WhereGreaterThan (ServerDeliveryDatettime, DateTime.Now)
					.OrderBy (ServerPickupDatetime);
				parseData = await query.FirstOrDefaultAsync ();
			} else {
				parseData = await GetTrip (currentTripId);
			}
			
			Trip trip = null;
			if (parseData != null)
				trip = (ConvertTrip (parseData) as Trip);

			return trip;
		}

		ITrip ConvertTrip (ParseObject parseData)
		{
			var trip = new Trip ();
			trip.TripId = parseData.ObjectId;
			trip.Shipper = new Shipper {
				AddressLineFirst = GetField<string>(parseData, ServerFromAddress),
				//AddressLineSecond = GetField<string>(parseData, ServerFromAddress)
			};
			trip.Receiver = new Receiver {
				AddressLineFirst = GetField<string>(parseData, ServerToAddress),
				//AddressLineSecond = GetField<string>(parseData, ServerFromAddress)
			};
			//trip.Time = (string)parseData [ServerToAddress];
			trip.PickupDatetime = GetField<DateTime>(parseData, ServerPickupDatetime);
			trip.DeliveryDatetime = GetField<DateTime>(parseData, ServerDeliveryDatettime);
			trip.DriverOnTimePickup = (int)(GetField<long>(parseData, ServerDriverOnTimePickup));
			trip.DriverOnTimeDelivery = (int)(GetField<long>(parseData, ServerDriverOnTimeDelivery));
			trip.JobCompleted = GetField<bool>(parseData, ServerJobCompleted);
			trip.Points = (int)(GetField<long> (parseData, ServerJobPrice));
			trip.DriverAccepted = GetField<bool?>(parseData, ServerDriverAccepted);
			trip.DeclineReason = GetField<string>(parseData, ServerDeclineReason);
			trip.JobCancelled = GetField<bool>(parseData, ServerJobCancelled);
			//job [ServerDriver] = this.FindUser (driver);

			return trip;
		}

		public async Task AcceptTrip(string TripId)
		{
			var data = await GetTrip (TripId);
			if (data != null) 
			{
				data [ServerDriverAccepted] = true;
				await data.SaveAsync ();
			}
		}

		public async Task DeclineTrip(string TripId, string reason)
		{
			var data = await GetTrip (TripId);
			if (data != null) 
			{
				data [ServerDriverAccepted] = false;
				data [ServerDeclineReason] = reason;
				await data.SaveAsync ();
			}
		}

		public async Task SetDriverPickupOnTime (string TripId, bool isOnTime)
		{
			var data = await GetTrip (TripId);
			if (data != null) 
			{
				data [ServerDriverOnTimePickup] = (isOnTime ? 1 : 0);
				await data.SaveAsync ();
			}
		}

		public async Task SetDriverDestinationOnTime (string TripId, bool isOnTime)
		{
			var data = await GetTrip (TripId);
			if (data != null) 
			{
				data [ServerDriverOnTimeDelivery] = (isOnTime ? 1 : 0);
				await data.SaveAsync ();
			}
		}

		public async Task<bool> IsCompletedTrip (string TripId)
		{
			var data = await GetTrip (TripId);
			return (bool)data [ServerJobCompleted];
		}

		public async Task<IList<ITrip>> GetTripList(string company)
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

			ObservableCollection<ITrip> resultList = new ObservableCollection<ITrip> ();
			foreach (var parseData in parseList) 
			{
				var trip = ConvertTrip(parseData);
				resultList.Add (trip);
			}
			return resultList;
		}

		private async Task<ParseObject> SaveGeoLocation(Position position)
		{
			ParseObject pointData = new ParseObject (ServerGeoLocation);
			ParseGeoPoint point = new ParseGeoPoint (position.Latitude, position.Longitude);
			pointData [ServerLocation] = point;
			pointData [ServerPointCreatedAt] = DateTime.Now;
			await pointData.SaveAsync ();

			return pointData;
		}

		public async Task SaveDriverLocation(string TripId, Position position)
		{
			if (ParseUser.CurrentUser != null) // && this.GetCurrentUserRole() == UserRole.UserRoleDriver) 
			{
				var tripData = await GetTrip (TripId);
				if (tripData != null) {
					var point = new ParseGeoPoint (position.Latitude, position.Longitude);
					tripData [ServerLocation] = point;

					var geoLocationData = await SaveGeoLocation (position);
					var historyRelation = tripData.GetRelation<ParseObject> (ServerLocationHistory);
					historyRelation.Add (geoLocationData);
					await tripData.SaveAsync ();
				}
			} else
				await Task.FromResult (false);
		}

		async Task<IEnumerable<ParseUser>> GetUserList (string companyName, UserRole requestRole)
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
						user.position = new Position (point.Latitude, point.Longitude);
						//updatedAt = driver.UpdatedAt.GetValueOrDefault ()
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

		public async Task SendComcheckRequest(string TripId, ComcheckRequestType RequestType)
		{
			ParseObject comcheck = new ParseObject (ServerComcheckRequest);
			comcheck [ServerDriver] = ParseUser.CurrentUser;
			comcheck [ServerState] = (int)ComcheckRequestState.Requested;
			comcheck [ServerRequestDatetime] = DateTime.Now;
			comcheck [ServerRequestType] = (int)RequestType;

			if (RequestType == ComcheckRequestType.FuelAdvance) {
				comcheck [ServerComcheck] = "fuel advance";
			} else {
				comcheck [ServerComcheck] = "lumper advance";
			};
		
			var trip = await GetTrip (TripId);
			if (trip != null) {
				//comcheck [ServerJobClass] = true;
				comcheck [ServerComcheckDispatch] = GetField<ParseObject>(trip, "Dispatcher");
				await comcheck.SaveAsync ();

				var advances = trip.GetRelation<ParseObject> ("Advances");
				advances.Add (comcheck);

				await trip.SaveAsync ();
			} else {
				
			}
			//SettingsService.AddOrUpdateSetting<string> (FuelId, id);
		}

		public async Task CancelComcheckRequest (string TripId, ComcheckRequestType RequestType)
		{
			var trip = await GetTrip (TripId);
			var relation = trip.GetRelation <ParseObject>("Advances");

			var relationQuery = relation.Query.OrderByDescending ("createdAt").WhereEqualTo(ServerRequestType, (int)RequestType);
			var comcheckData = await relationQuery.FirstAsync ();
			relation.Remove (comcheckData);
			await trip.SaveAsync ();
			await comcheckData.DeleteAsync ();
		}

		public async Task<ComcheckRequestState> GetComcheckState(string TripId, ComcheckRequestType RequestType)
		{
			var trip = await GetTrip (TripId);
			var relation = trip.GetRelation <ParseObject>("Advances");

			var relationQuery = relation.Query.OrderByDescending ("createdAt").WhereEqualTo(ServerRequestType, (int)RequestType);
		 
			var comcheckData = await relationQuery.FirstAsync ();
			if (comcheckData != null) {
				var state = GetField<long>(comcheckData, ServerState);
				return (ComcheckRequestState)state;
			} else {
				return ComcheckRequestState.None;
			};
		}

		public async Task<string> GetComcheck (string TripId, ComcheckRequestType RequestType)
		{
			var trip = await GetTrip (TripId);
			var relation = trip.GetRelation <ParseObject>("Advances");

			var relationQuery = relation.Query.OrderByDescending ("createdAt").WhereEqualTo(ServerRequestType, (int)RequestType);

			var comcheckData = await relationQuery.FirstAsync ();
			if (comcheckData != null)
				return (string)comcheckData [ServerComcheck];
			else
				return null;
		}

		public async Task SendJobAlert(string alert, string tripId)
		{
			ParseObject jobAlert = new ParseObject ("JobAlert");
			jobAlert ["AlertText"] = alert; // alert ["AlertText"];
			//jobAlert ["Alert"] = alert;
			await jobAlert.SaveAsync ();

			var trip = await GetTrip (tripId);
			var jobAlerts = trip.GetRelation<ParseObject> ("JobAlerts");
			jobAlerts.Add (jobAlert);

			await trip.SaveAsync ();
		}

		public async Task<IEnumerable<IAlerts>> GetPossibleAlerts()
		{
			var parseQuery = new ParseQuery<Alerts> ();
			/*var parseQuery = from alerts in new ParseQuery<Alerts>()
				select alerts;*/
			var data = await parseQuery.FindAsync ();
			return data;
		}

		public async Task<ICompany> GetUserCompany()
		{
			var query = ParseObject.GetQuery(ServerCompany)
				.WhereEqualTo(ServerDrivers, ParseUser.CurrentUser);

			var companyData = await query.FirstOrDefaultAsync ();
			if (companyData != null) {
				var company = new Company {
					ID = GetField<string> (companyData, ServerObjectId),
					Name = GetField<string> (companyData, ServerName),
					
				};

				return company;
			} else
				return null;
		}
	}
}