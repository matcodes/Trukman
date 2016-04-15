using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections;
using Trukman.Interfaces;
using KAS.Trukman.Data.Interfaces;

namespace Trukman.Helpers
{
	public enum AuthorizationRequestStatus
	{
		Authorized,
		Pending,
		Declined,
		Frozen
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
		Task<ITrip> GetCurrentTrip(string currentTripId = "");
		Task<IList<ITrip>> GetTripList(string company);
		Task SaveDriverLocation(IUserLocation location);
		//Task<Job> GetCurrentDriverJob ();

		Task<IList<IUser>> GetDriverList (string companyName);
		Task<IList<IUser>> GetDispatchList(string companyName);
		Task RemoveCompanyUser (string companyName, IUser _user);

		Task SendComcheckRequest(ComcheckRequestType RequestType);
		//Task<ComcheckRequest> GetComcheckRequest(ComcheckRequestType RequestType);
	}
}