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

	public interface IServerManager
	{	
		void Init ();
		Task LogIn (string name, string pass);
		Task LogOut();
		bool IsAuthorized ();
		Task Register (string name, string pass, UserRole role);
		Task AddCompany (string name);
		Task<bool> RequestToJoinCompany (string name);
		void StartTimerForRequest ();

		Task<bool> FindCompany(string name);
		UserRole GetCurrentUserRole();
		string GetCurrentUserName();
		bool IsOwner();
		Task<bool> IsUserJoinedToCompany (string companyName = "");

		Task SaveJob (string name, string description, string shipperAddress, 
		              string receiveAddress, string driver);
		Task<IList<Job>> GetJobList(string driverName = "");
		Task SaveDriverLocation(UserLocation location);

		Task<IList<User>> GetDriverList ();
		Task<IList<User>> GetDispatchList();
		Task RemoveCompanyUser (Trukman.User _user);
	}
}