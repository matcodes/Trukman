using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections;
using Trukman.Interfaces;
using KAS.Trukman.Data.Interfaces;
using Xamarin.Forms.Maps;

namespace Trukman.Helpers
{
	public enum AuthorizationRequestStatus
	{
		Authorized,
		Pending,
		Declined
		//Frozen
	}

	public enum ComcheckRequestType
	{
		FuelAdvance,
		Lumper
	}

	public enum ComcheckRequestState
	{
		None,
		Requested,
		Received,
		Visible
	}

	public interface IServerManager
	{	
		Task LogIn (string name, string pass);
		Task LogOut();
		bool IsAuthorizedUser ();
		Task Register (string name, string pass, UserRole role);
		Task AddCompany (string name, string DBA, string address, string phone, string email, string fleetSize);
		Task<bool> RequestToJoinCompany (string name);
		void StartTimerForRequest ();

		Task<bool> FindCompany(string name);
		UserRole GetCurrentUserRole();
		string GetCurrentUserName ();
		//string GetCurrentCompanyName();
		bool IsOwner();
		Task<AuthorizationRequestStatus> GetAuthorizationStatus (string companyName);
		//bool IsFrozenAuthorization();

		Task SaveJob (string name, string description, string shipperAddress, 
			string receiveAddress, string driver, string company);
		Task<ITrip> GetNewOrCurrentTrip(string currentTripId = "");
		Task AcceptTrip (string TripId);
		Task DeclineTrip (string TripId, string reason);
		Task<IList<ITrip>> GetTripList(string company);
		Task SaveDriverLocation(string TripId, Position position);
		Task SetDriverPickupOnTime (string TripId, bool isOnTime);
		Task SetDriverDestinationOnTime (string TripId, bool isOnTime);
		Task<bool> IsCompletedTrip (string TripId);
		//Task<Job> GetCurrentDriverJob ();

		Task<IList<IUser>> GetDriverList (string companyName);
		Task<IList<IUser>> GetDispatchList(string companyName);
		Task RemoveCompanyUser (string companyName, IUser _user);

		Task SendComcheckRequest (string TripId, ComcheckRequestType RequestType);
		Task CancelComcheckRequest (string TripId, ComcheckRequestType RequestType);
		Task<ComcheckRequestState> GetComcheckState (string TripId, ComcheckRequestType RequestType);
		Task<string> GetComcheck (string TripId, ComcheckRequestType RequestType);
		Task SendJobAlert(string alert, string tripId);
		Task<IEnumerable<IAlerts>> GetPossibleAlerts();

		Task<ICompany> GetUserCompany ();
	}
}