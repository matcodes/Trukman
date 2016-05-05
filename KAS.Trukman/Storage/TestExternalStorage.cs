using System;
using System.Collections.Generic;
using System.Text;
using KAS.Trukman.Data.Classes;
using System.Threading;
using Trukman.Droid.Helpers;
using KAS.Trukman.Data.Interfaces;
using Xamarin.Forms.Maps;
using System.Threading.Tasks;
using KAS.Trukman.Data.Infos;
using KAS.Trukman.Data.Enums;

namespace KAS.Trukman.Storage
{
    #region TestExternalStorage
    public class TestExternalStorage : IExternalStorage
    {
        private DateTime _startTime = DateTime.Now;

        private bool _isCanceled = false;
        private bool _isAccepted = false;
        private string _declineReason = "";
        private bool _isPickup = false;
        private bool _isDelivery = false;

        //        private ServerManager _serverManager = null;

        public TestExternalStorage()
        {
            //           _serverManager = new ServerManager();
        }

        private Trip CreateTrip()
        {
            var shipper = new Contractor
            {
                ID = "FOyWILhzOR",
                Name = "ООО РОГА И КОПЫТА",
                Phone = "+7 (498) 555 55 55",
                Fax = "+7 (498) 555 55 56",
                Address = "Московская область, Ольявидово, д. 16"
            };
            var receiver = new Contractor
            {
                ID = "ZS8gxqXGqe",
                Name = "ООО КОПЫТА И РОГА",
                Phone = "+7 (498) 666 66 66",
                Fax = "+7 (498) 666 66 67",
                Address = "Московская область, Мытищи, Летная, д. 30 к. 3"
            };

            var now = DateTime.Now;
            var pickupTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0).AddMinutes(60);
            var deliveryTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0).AddMinutes(60).AddHours(2);

            var trip = new Trip
            {
                ID = "v2U05igxVp",
                Shipper = shipper,
                Receiver = receiver,
                JobCompleted = false,
                JobCancelled = _isCanceled,
                PickupDatetime = pickupTime,
                DeliveryDatetime = deliveryTime,
                Points = 500,
                DriverAccepted = _isAccepted,
                DeclineReason = _declineReason,
                IsPickup = _isPickup,
                IsDelivery = _isDelivery,
                UpdateTime = DateTime.Now
            };

            return trip;
        }

        #region IExternalStorage
        public Trip CheckNewTripForDriver(string userID)
        {
            Trip trip = null;
            var time = DateTime.Now - _startTime;
            if (time.TotalSeconds > 20)
            {
                trip = this.CreateTrip();
            }
            return trip;
        }

        public Trip SelectTripByID(string id)
        {
            Thread.Sleep(500);
            var trip = this.CreateTrip();
            return trip;
        }

        public Trip AcceptTrip(string id)
        {
            _isAccepted = true;
            var trip = this.SelectTripByID(id);
            Thread.Sleep(500);
            return trip;
        }

        public Trip DeclineTrip(string id, string reasonText)
        {
            _declineReason = reasonText;
            var trip = this.SelectTripByID(id);
            Thread.Sleep(500);
            return trip;
        }

        public Trip CompleteTrip(string id)
        {
            var trip = this.SelectTripByID(id);
            Thread.Sleep(500);
            return trip;
        }

        public Trip TripInPickup(string id, int minutes)
        {
            _isPickup = true;
            var trip = this.SelectTripByID(id);
            Thread.Sleep(500);
            return trip;
        }

        public Trip TripInDelivery(string id, int minutes)
        {
            _isDelivery = true;
            var trip = this.SelectTripByID(id);
            Thread.Sleep(500);
            return trip;
        }

        public Trip SendPhoto(string id, byte[] data, string kind)
        {
            var trip = this.SelectTripByID(id);
            Thread.Sleep(3000);
            return trip;
        }

        public Trip AddLocation(string id, Position location)
        {
            var trip = this.SelectTripByID(id);
            Thread.Sleep(500);
            return trip;
        }

        public Trip SaveLocation(string id, Position location)
        {
            var trip = this.SelectTripByID(id);
            Thread.Sleep(500);
            return trip;
        }

        public async Task<Company[]> SelectCompanies(string filter)
        {
            await Task.Run(() => { });
            return new Company[] { };
        }

        public async Task<Company> SelectCompanyByName(string company)
        {
            await Task.Run(() => { });
            return null;
        }

        public async Task<Company> SelectUserCompanyAsync()
        {
            await Task.Run(() => { });
            return null;
        }

        public async Task<Trip[]> SelectActiveTrips()
        {
            await Task.Run(() => { });
            return new Trip[] { };
        }

        public async Task<Position> SelectDriverPosition(string tripID)
        {
            await Task.Run(() => { });
            return new Position(0, 0);
        }

        public void InitializeOwnerNotification()
        {
        }

        public async Task<User> BecomeAsync(string session)
        {
            await Task.Run(() => { });
            return null;
        }

        public async Task<User> SignUpAsync(User user)
        {
            await Task.Run(() => { });
            return null;
        }

        public async Task<User> LogInAsync(string userName, string password)
        {
            await Task.Run(() => { });
            return null;
        }

        public async Task<bool> UserExistAsync(string userName)
        {
            await Task.Run(() => { });
            return false;
        }

        public async Task<Company> RegisterCompany(CompanyInfo companyInfo)
        {
            await Task.Run(() => { });
            return null;
        }

        public async Task<Company> RegisterDriver(DriverInfo driverInfo)
        {
            await Task.Run(() => { });
            return null;
        }

        public async Task<User> GetCurrentUser()
        {
            await Task.Run(() => { });
            return null;
        }

        public async Task<string> GetSessionToken()
        {
            await Task.Run(() => { });
            return "";
        }

        public async Task<User> SelectRequestedUser(string companyID)
        {
            await Task.Run(() => { });
            return null;
        }

        public async Task<DriverState> GetDriverState()
        {
            await Task.Run(() => { });
            return DriverState.Declined;
        }

        public async Task AcceptDriverToCompany(User user)
        {
            await Task.Run(() => { });
        }

        public async Task DeclineDriverToCompany(User user)
        {
            await Task.Run(() => { });
        }

        public async Task<Notification> GetNotification()
        {
            await Task.Run(() => { });
            return null;
        }

        public async Task SendNotification(Trip trip, string message)
        {
            await Task.Run(() => { });
        }
        #endregion
    }
    #endregion
}
