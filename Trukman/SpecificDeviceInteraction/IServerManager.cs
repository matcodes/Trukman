using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Trukman
{
	public interface IServerManager
	{
		void Init ();
		void LogIn(string name, string pass);
		void Register(string name, string pass);
	}
}

