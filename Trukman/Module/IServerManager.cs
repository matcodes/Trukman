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
		Task Register (string name, string pass, UserRole role);
		Task AddCompany (string name);
		Task<Boolean> RequestToJoinCompany (string name);
		void StartTimerForRequest ();
		//void DisposeTimerForRequest();

		UserRole GetCurrentUserRole();
		string GetCurrentUserName();

		Task SaveJob (string name, string description, string shipperAddress, 
		              string receiveAddress, string driver); //, string company);
		Task<List<Job>> GetJobList(string companyName = "", string driverName = "");
		Task SaveDriverLocation(GPSLocation location);
		Task<List<Driver>> GetDriverList ();
	}
}