using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections;

namespace Trukman
{
	public enum UserRole {
		UserRoleOwner,
		UserRoleDispatch,
		UserRoleDriver
	};

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
		void Init ();
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
		Task<IList<Job>> GetJobList(string company);
		Task SaveDriverLocation(UserLocation location);
		//Task<Job> GetCurrentDriverJob ();

		Task<IList<Trukman.User>> GetDriverList (string companyName);
		Task<IList<Trukman.User>> GetDispatchList(string companyName);
		Task RemoveCompanyUser (string companyName, Trukman.User _user);

		Task SendComcheckRequest(ComcheckRequestType RequestType);
		Task<ComcheckRequest> GetComcheckRequest(ComcheckRequestType RequestType);
	}
}