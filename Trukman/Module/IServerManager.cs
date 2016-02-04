using System;
using System.Threading.Tasks;
using System.Collections.Generic;

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
		Task Register (string name, string pass, UserRole role);
		Task AddCompany (string name);
		Task RequestToJoinCompany (string name);
		void StartTimerForRequest ();
	}
}