﻿using KAS.Trukman.Data.Classes;
using KAS.Trukman.Data.Enums;
using KAS.Trukman.Data.Infos;
using KAS.Trukman.Data.Interfaces;
using KAS.Trukman.Languages;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trukman.Interfaces;
using Xamarin.Forms.Maps;

namespace KAS.Trukman.Storage
{
    #region LocalStorage
    public class LocalStorage
    {
        #region Static member
        public static readonly string DATABASE_NAME = "trukman.db";

        public static readonly string USER_ID_SETTINGS_KEY = "UserID";
        public static readonly string COMPANY_ID_SETTINGS_KEY = "CompanyID";
        public static readonly string TRIP_ID_SETTINGS_KEY = "TripID";
        public static readonly string TRIP_STATE_SETTINGS_KEY = "TripState";
        public static readonly string USER_SESSION_ID_KEY = "SessionID";
        #endregion

        private SQLiteConnection _connection;

        private IExternalStorage _externalStorage;

        public LocalStorage()
        {
            try
            {
                var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                _connection = new SQLiteConnection(System.IO.Path.Combine(path, DATABASE_NAME));

                _connection.CreateTable<SettingsItem>();
                _connection.CreateTable<User>();
                _connection.CreateTable<Company>();
                _connection.CreateTable<Contractor>();
                _connection.CreateTable<Trip>();

                _externalStorage = new ParseExternalStorage(); // new TestExternalStorage();

                //                this.TestInitialize();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public void SynchronizeDriverContext()
        {
            var tripID = this.GetSettings(TRIP_ID_SETTINGS_KEY);
            if (String.IsNullOrEmpty(tripID))
                this.CheckNewTrip();
            else
                this.SynchronizeTrip(tripID, null);
        }

        public void SynchronizeOwnerContext()
        {
        }

        public void SynchronyzeDispatcherContex()
        {
        }

        private void CheckNewTrip()
        {
            var userID = this.GetSettings(USER_ID_SETTINGS_KEY);
            var trip = _externalStorage.CheckNewTripForDriver(userID);
            if (trip != null)
            {
                this.SetSettings(TRIP_ID_SETTINGS_KEY, trip.ID);
                this.SaveTrip(trip);
            }
        }

        public int GetTripState()
        {
            var result = (int)0;
            var tripState = this.GetSettings(TRIP_STATE_SETTINGS_KEY);
            if (!String.IsNullOrEmpty(tripState))
                int.TryParse(tripState, out result);
            return result;
        }

        public void SetTripState(int tripState)
        {
            this.SetSettings(TRIP_STATE_SETTINGS_KEY, tripState.ToString());
        }

        public bool TripIsCancelled(string tripID)
        {
            bool isCancelled = false;
            try
            {
                this.SynchronizeTrip(tripID, null);

                var trip = this.SelectTripByID(tripID);
                isCancelled = ((trip != null) && (trip.JobCancelled));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw new Exception(AppLanguages.CurrentLanguage.CheckInternetConnectionErrorMessage);
            }
            return isCancelled;
        }

        public void TripAccepted(string tripID)
        {
            try
            {
                var trip = _externalStorage.AcceptTrip(tripID);
                this.SynchronizeTrip(tripID, trip);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw new Exception(AppLanguages.CurrentLanguage.CheckInternetConnectionErrorMessage);
            }
        }

        public void TripDeclined(string tripID, string reasonText)
        {
            try
            {
                var trip = _externalStorage.DeclineTrip(tripID, reasonText);
                this.RemoveTrip(trip);
                this.SetSettings(TRIP_ID_SETTINGS_KEY, "");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw new Exception(AppLanguages.CurrentLanguage.CheckInternetConnectionErrorMessage);
            }
        }

        public void TripCompleted(string tripID)
        {
            try
            {
                var trip = _externalStorage.CompleteTrip(tripID);
                this.RemoveTrip(trip);
                this.SetSettings(TRIP_ID_SETTINGS_KEY, "");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw new Exception(AppLanguages.CurrentLanguage.CheckInternetConnectionErrorMessage);
            }
        }

        public void TripInPickup(string tripID, int minutes)
        {
            try
            {
                var trip = _externalStorage.TripInPickup(tripID, minutes);
                this.SynchronizeTrip(tripID, trip);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw new Exception(AppLanguages.CurrentLanguage.CheckInternetConnectionErrorMessage);
            }
        }

        public void TripInDelivery(string tripID, int minutes)
        {
            try
            {
                var trip = _externalStorage.TripInDelivery(tripID, minutes);
                this.SynchronizeTrip(tripID, trip);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw new Exception(AppLanguages.CurrentLanguage.CheckInternetConnectionErrorMessage);
            }
        }

        public void SendPhoto(string tripID, byte[] data, string kind)
        {
            try
            {
                var trip = _externalStorage.SendPhoto(tripID, data, kind);
                this.SynchronizeTrip(tripID, trip);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw new Exception(AppLanguages.CurrentLanguage.CheckInternetConnectionErrorMessage);
            }
        }

        public void AddLocation(string tripID, Position location)
        {
            try
            {
                var trip = _externalStorage.AddLocation(tripID, location);
                this.SynchronizeTrip(tripID, trip);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw new Exception(AppLanguages.CurrentLanguage.CheckInternetConnectionErrorMessage);
            }
        }

        public void SaveLocation(string tripID, Position location)
        {
            try
            {
                var trip = _externalStorage.SaveLocation(tripID, location);
                this.SynchronizeTrip(tripID, trip);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw new Exception(AppLanguages.CurrentLanguage.CheckInternetConnectionErrorMessage);
            }
        }

        public void RemoveTrip(string tripID)
        {
            try
            {
                var trip = this.SelectTripByID(tripID);
                if (trip != null)
                    this.RemoveTrip(trip);
                this.SetSettings(TRIP_ID_SETTINGS_KEY, "");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw new Exception(AppLanguages.CurrentLanguage.CheckInternetConnectionErrorMessage);
            }
        }

        public async Task<Company[]> SelectCompanies(string filter)
        {
            var companies = new Company[] { };
            try
            {
                companies = await _externalStorage.SelectCompanies(filter);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw new Exception(AppLanguages.CurrentLanguage.CheckInternetConnectionErrorMessage);
            }
            return companies;
        }

        public async Task<Company> SelectCompanyByName(string name)
        {
            Company company = null;
            try
            {
                company = await _externalStorage.SelectCompanyByName(name);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw new Exception(AppLanguages.CurrentLanguage.CheckInternetConnectionErrorMessage);
            }
            return company;
        }

        public async Task<Company> SelectUserCompany()
        {
            Company company = null;
            try
            {
                company = await _externalStorage.SelectUserCompanyAsync();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw new Exception(AppLanguages.CurrentLanguage.CheckInternetConnectionErrorMessage);
            }
            return company;
        }

        private void SynchronizeTrip(string tripID, Trip externalTrip)
        {
            var localTrip = this.SelectTripByID(tripID);
            if (externalTrip == null)
                externalTrip = _externalStorage.SelectTripByID(tripID);
            if ((localTrip != null) && (externalTrip != null) && (localTrip.UpdateTime < externalTrip.UpdateTime.AddMilliseconds(externalTrip.UpdateTime.Millisecond * -1)))
                this.SaveTrip(externalTrip);
        }

        public async Task JoinDriver()
        {
            try
            {
                var userInfo = await App.ServerManager.GetCurrentUser();
                var companyInfo = await this.SelectUserCompany();
                if ((userInfo != null) && (companyInfo != null))
                {
                    var user = new User
                    {
                        ID = userInfo.ID,
                        UserName = userInfo.UserName,
                        FirstName = userInfo.FirstName,
                        LastName = userInfo.LastName,
                        Phone = userInfo.Phone,
                        Email = userInfo.Email,
                        Role = userInfo.Role
                    };
                    this.SaveUser(user);
                    this.SetSettings(USER_ID_SETTINGS_KEY, user.ID);

                    var company = new Company
                    {
                        ID = companyInfo.ID,
                        DisplayName = companyInfo.DisplayName,
                        Name = companyInfo.Name
                    };
                    this.SaveCompany(company);
                    this.SetSettings(COMPANY_ID_SETTINGS_KEY, company.ID);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public async Task<Trip[]> SelectActiveTrips()
        {
            Trip[] trips = new Trip[] { };
            try
            {
                trips = await _externalStorage.SelectActiveTrips();

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw new Exception(AppLanguages.CurrentLanguage.CheckInternetConnectionErrorMessage);
            }
            return trips;
        }

        public async Task<Position> SelectDriverPosition(string tripID)
        {
            var position = new Position(0, 0);
            try
            {
                position = await _externalStorage.SelectDriverPosition(tripID);

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw new Exception(AppLanguages.CurrentLanguage.CheckInternetConnectionErrorMessage);
            }
            return position;
        }

        public Trip SelectTripByIDExternal(string id)
        {
            Trip trip = null;
            try
            {
                trip = _externalStorage.SelectTripByID(id);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw new Exception(AppLanguages.CurrentLanguage.CheckInternetConnectionErrorMessage);
            }
            return trip;
        }

        private void TestInitialize()
        {
            var userID = "9V1Y3Qh20m";
            var companyID = "k3OWFMMp0W";

            var item = new SettingsItem
            {
                Key = USER_ID_SETTINGS_KEY,
                Value = userID
            };
            this.SaveSettings(item);

            item = new SettingsItem
            {
                Key = COMPANY_ID_SETTINGS_KEY,
                Value = companyID
            };
            this.SaveSettings(item);

            var user = new User
            {
                ID = userID,
                Role = UserRole.UserRoleDriver,
                UserName = "alex flex",
                Email = "rudy@rudy.con",
                FirstName = "Alex",
                LastName = "Flex"
            };
            this.SaveUser(user);

            var company = new Company
            {
                ID = companyID,
                Name = "ultimate freight inc",
                DisplayName = "ULTIMATE FREIGHT INC"
            };
            this.SaveCompany(company);
        }

        public async Task<User> BecomeAsync()
        {
            User user = null;
            try
            {
                var session = this.GetSettings(USER_SESSION_ID_KEY);
                if (!String.IsNullOrEmpty(session))
                    user = await _externalStorage.BecomeAsync(session);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                this.SetSettings(USER_SESSION_ID_KEY, "");
                //                throw new Exception(AppLanguages.CurrentLanguage.CheckInternetConnectionErrorMessage);
            }
            return user;
        }

        public async Task<User> SignUpAsync(User user)
        {
            User currentUser = null;
            try
            {
                currentUser = await _externalStorage.SignUpAsync(user);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw new Exception(AppLanguages.CurrentLanguage.CheckInternetConnectionErrorMessage);
            }
            return currentUser;
        }

        public async Task<User> LogInAsync(string userName, string password)
        {
            User user = null;
            try
            {
                user = await _externalStorage.LogInAsync(userName, password);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw new Exception(AppLanguages.CurrentLanguage.CheckInternetConnectionErrorMessage);
            }
            return user;
        }

        public async Task<bool> UserExist(string userName)
        {
            var exist = false;
            try
            {
                exist = await _externalStorage.UserExistAsync(userName);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw new Exception(AppLanguages.CurrentLanguage.CheckInternetConnectionErrorMessage);
            }
            return exist;
        }

        public async Task<Company> RegisterCompanyAsync(CompanyInfo companyInfo)
        {
            Company company = null;
            try
            {
                company = await _externalStorage.RegisterCompany(companyInfo);
                var sessionToken = await _externalStorage.GetSessionToken();
                this.SetSettings(USER_SESSION_ID_KEY, sessionToken);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw exception;
            }
            return company;
        }

        public async Task<Company> RegisterDriverAsync(DriverInfo driverInfo)
        {
            Company company = null;
            try
            {
                company = await _externalStorage.RegisterDriver(driverInfo);
                var sessionToken = await _externalStorage.GetSessionToken();
                this.SetSettings(USER_SESSION_ID_KEY, sessionToken);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw exception;
            }
            return company;
        }

        public async Task<User> GetCurrentUser()
        {
            User user = null;
            try
            {
                user = await _externalStorage.GetCurrentUser();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw exception;
            }
            return user;
        }

        public async Task<User> SelectRequestedUser(string companyID)
        {
            User user = null;
            try
            {
                user = await _externalStorage.SelectRequestedUser(companyID);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw new Exception(AppLanguages.CurrentLanguage.CheckInternetConnectionErrorMessage);
            }
            return user;
        }

        public async Task<DriverState> GetDriverState()
        {
            DriverState state = DriverState.Declined;
            try
            {
                state = await _externalStorage.GetDriverState();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw new Exception(AppLanguages.CurrentLanguage.CheckInternetConnectionErrorMessage);
            }
            return state;
        }

        public async Task AcceptDriverToCompany(User user)
        {
            try
            {
                await _externalStorage.AcceptDriverToCompany(user);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw new Exception(AppLanguages.CurrentLanguage.CheckInternetConnectionErrorMessage);
            }
        }

        public async Task DeclineDriverToCompany(User user)
        {
            try
            {
                await _externalStorage.DeclineDriverToCompany(user);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw new Exception(AppLanguages.CurrentLanguage.CheckInternetConnectionErrorMessage);
            }
        }

        public async Task<Notification> GetNotification()
        {
            try
            {
                var notification = await _externalStorage.GetNotification();
                return notification;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw new Exception(AppLanguages.CurrentLanguage.CheckInternetConnectionErrorMessage);
            }
        }

        public async Task SendNotification(Trip trip, string message)
        {
            try
            {
                await _externalStorage.SendNotification(trip, message);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw new Exception(AppLanguages.CurrentLanguage.CheckInternetConnectionErrorMessage);
            }
        }

        #region SettingsItem
        public string GetSettings(string key)
        {
            var value = "";
            var item = this.GetSettingsByKey(key);
            if (item != null)
                value = item.Value;
            return value;
        }

        public void SetSettings(string key, string value)
        {
            var settingsItem = new SettingsItem
            {
                Key = key,
                Value = value
            };
            this.SaveSettings(settingsItem);
        }

        public SettingsItem GetSettingsByKey(string key)
        {
            var item = _connection.Table<SettingsItem>().Where(si => si.Key == key).FirstOrDefault();
            return item;
        }

        public SettingsItem SaveSettings(SettingsItem item)
        {
            if (this.SettingsExist(item.Key))
                _connection.Update(item);
            else
                _connection.Insert(item);
            return item;
        }

        private bool SettingsExist(string key)
        {
            return (_connection.Table<SettingsItem>().Where(si => si.Key == key).Count() > 0);
        }
        #endregion

        #region User
        public User GetUserByID(string id)
        {
            var user = _connection.Table<User>().Where(u => u.ID == id).FirstOrDefault();
            return user;
        }

        public User SaveUser(User user)
        {
            if (this.UserExistDb(user.ID))
                _connection.Update(user);
            else
                _connection.Insert(user);
            return user;
        }

        private bool UserExistDb(string id)
        {
            return (_connection.Table<User>().Where(u => u.ID == id).Count() > 0);
        }
        #endregion

        #region Company
        public Company GetCompanyByID(string id)
        {
            var company = _connection.Table<Company>().Where(c => c.ID == id).FirstOrDefault();
            return company;
        }

        public Company SaveCompany(Company company)
        {
            if (this.CompanyExist(company.ID))
                _connection.Update(company);
            else
                _connection.Insert(company);
            return company;
        }

        private bool CompanyExist(string id)
        {
            return (_connection.Table<Company>().Where(c => c.ID == id).Count() > 0);
        }
        #endregion

        #region Contractor
        public Contractor SelectContractorByID(string id)
        {
            var contractor = _connection.Table<Contractor>().Where(c => c.ID == id).FirstOrDefault();
            return contractor;
        }

        public Contractor SaveContractor(Contractor contractor)
        {
            if (this.ContractorExist(contractor.ID))
                _connection.Update(contractor);
            else
                _connection.Insert(contractor);
            return contractor;
        }

        private bool ContractorExist(string id)
        {
            return (_connection.Table<Contractor>().Where(c => c.ID == id).Count() > 0);
        }
        #endregion

        #region Trip
        public Trip SelectTripByID(string id)
        {
            var trip = _connection.Table<Trip>().Where(t => t.ID == id).FirstOrDefault();
            if (trip != null)
            {
                trip.Shipper = this.SelectContractorByID(trip.ShipperID);
                trip.Receiver = this.SelectContractorByID(trip.ReceiverID);
            }
            return trip;
        }

        public Trip SaveTrip(Trip trip)
        {
            var shipper = (trip.Shipper as Contractor);
            if (shipper != null)
            {
                trip.ShipperID = shipper.ID;
                this.SaveContractor(shipper);
            }
            var receiver = (trip.Receiver as Contractor);
            if (receiver != null)
            {
                trip.ReceiverID = receiver.ID;
                this.SaveContractor(receiver);
            }
            if (this.TripExist(trip.ID))
                _connection.Update(trip);
            else
                _connection.Insert(trip);
            return trip;
        }

        public Trip RemoveTrip(Trip trip)
        {
            _connection.Delete(trip);
            return trip;
        }

        public void InitializeOwnerNotification()
        {
            _externalStorage.InitializeOwnerNotification();
        }

        private bool TripExist(string id)
        {
            return (_connection.Table<Trip>().Where(t => t.ID == id).Count() > 0);
        }
        #endregion
    }
    #endregion
}
