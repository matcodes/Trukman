using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Trukman
{
	public interface IServerManager
	{
		void Init ();
		Task LogIn (string name, string pass);
		Task Register (string name, string pass);
		Task AddCompany (string name);
		Task RequestToJoinCompany (string name);
		void StartTimerForRequest ();
	}
}