using KAS.Trukman.Data.Classes;
using KAS.Trukman.Data.Enums;
using KAS.Trukman.Data.Infos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;
using KAS.Trukman.Storage.ParseClasses;
using Parse;
using Trukman.Helpers;
using KAS.Trukman.Data.Route;

namespace KAS.Trukman.Storage
{
    #region IExternalStorage
    public interface IExternalStorage
    {
        Task<Trip> CheckNewTripForDriver(string userID);

        Task<Trip> SelectTripByID(string id);

        Task<Trip> AcceptTrip(string id);

        Task<Trip> CompleteTrip(string id);

        Task<Trip> DeclineTrip(string id, int declineReason, string reasonText);

        Task<Trip> TripInPickup(string id, int minutes);

        Task<Trip> TripInDelivery(string id, int minutes);

        Task<Trip> SendPhoto(string id, byte[] data, PhotoKind kind);

        Task<Trip> AddLocation(string id, Position location);

        Task<Trip> SaveLocation(string id, Position location);

        Task<Company[]> SelectCompanies(string filter);

        //Task<Company> SelectCompanyByName(string name);

        Task<Company> SelectUserCompanyAsync();

        Task<Trip[]> SelectActiveTrips();

        Task<Trip[]> SelectCompletedTrips();

        Task<Position> SelectDriverPosition(string tripID);

        void InitializeOwnerNotification();

        //User Become(string session);

        void Become(User user);

        //Task<User> SignUpAsync(User user);

        //Task<User> LogInAsync(string userName, string password);

        //Task<bool> UserExistAsync(string userName);

        Task<Company> RegisterCompany(CompanyInfo companyInfo);

        Task<Company> RegisterDriver(DriverInfo driverInfo);

        Task<User> GetCurrentUser();

        //Task<string> GetSessionToken();

        Task<User> SelectRequestedUser(string companyID);

        Task<DriverState> GetDriverState(string companyID, string driverID);

        Task CancelDriverRequest(string companyID, string driverID);

        //Task AcceptDriverToCompany(User user);

        Task AcceptDriverToCompany(string companyID, string driverID);

        //Task DeclineDriverToCompany(User user);

        Task DeclineDriverToCompany(string companyID, string driverID);

        Task<JobNotification> GetNotification();

        Task SendNotification(Trip trip, string message);

		Task<ComcheckRequestState> GetComcheckStateAsync (string tripID, ComcheckRequestType requestType);

		Task<string> GetComcheckAsync (string tripID, ComcheckRequestType requestType);
	
		Task SendComcheckRequestAsync (string tripID, ComcheckRequestType requestType);

		Task CancelComcheckRequestAsync (string tripID, ComcheckRequestType requestType);

		Task SendJobAlertAsync (string tripID, int alertType, string alertText);

        Task<JobAlert[]> SelectJobAlertsAsync();

        Task SetJobAlertIsViewedAsync(string jobAlertID, bool isViewed);

        Task<Advance[]> SelectFuelAdvancesAsync(int requestType);

		Task SetAdvanceStateAsync (Advance advance);

		Task<User[]> SelectBrockersAsync ();

		Task<User[]> SelectDriversAsync ();

		//Task<Trip> CreateTripAsync (Trip trip);

		Task<Photo[]> SelectPhotosAsync ();

        Task<string> CreateInvoiceForJobAsync(string tripID);

        //Task AddPointsAsync(string jobID, string text, int points);

        //Task<int> GetPointsByJobIDAsync(string jobID);

		Task<int> GetPointsByDriverIDAsync (string driverID);

        Task<JobPoint[]> SelectJobPointsAsync();

        Task<Position> GetPositionByAddress(string address);

        Task<string> GetAddressByPosition(Position position);

        Task<RouteResult> GetMapRoute(Position startPosition, Position endPosition);
    }
    #endregion
}
