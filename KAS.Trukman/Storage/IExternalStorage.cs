using KAS.Trukman.Data.Classes;
using KAS.Trukman.Data.Enums;
using KAS.Trukman.Data.Infos;
using KAS.Trukman.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace KAS.Trukman.Storage
{
    #region IExternalStorage
    public interface IExternalStorage
    {
        Trip CheckNewTripForDriver(string userID);

        Trip SelectTripByID(string id);

        Trip AcceptTrip(string id);

        Trip CompleteTrip(string id);

        Trip DeclineTrip(string id, string reasonText);

        Trip TripInPickup(string id, int minutes);

        Trip TripInDelivery(string id, int minutes);

        Trip SendPhoto(string id, byte[] data, string kind);

        Trip AddLocation(string id, Position location);

        Trip SaveLocation(string id, Position location);

        Task<Company[]> SelectCompanies(string filter);

        Task<Company> SelectCompanyByName(string name);

        Task<Company> SelectUserCompanyAsync();

        Task<Trip[]> SelectActiveTrips();

        Task<Position> SelectDriverPosition(string tripID);

        void InitializeOwnerNotification();

        Task<User> BecomeAsync(string session);

        Task<User> SignUpAsync(User user);

        Task<User> LogInAsync(string userName, string password);

        Task<bool> UserExistAsync(string userName);

        Task<Company> RegisterCompany(CompanyInfo companyInfo);

        Task<Company> RegisterDriver(DriverInfo driverInfo);

        Task<User> GetCurrentUser();

        Task<string> GetSessionToken();

        Task<User> SelectRequestedUser(string companyID);

        Task<DriverState> GetDriverState();

        Task AcceptDriverToCompany(User user);

        Task DeclineDriverToCompany(User user);

        Task<Notification> GetNotification();

        Task SendNotification(Trip trip, string message);
    }
    #endregion
}
