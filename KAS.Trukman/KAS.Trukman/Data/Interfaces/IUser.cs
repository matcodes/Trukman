using System;
using KAS.Trukman.Data.Interfaces;

namespace Trukman.Data.Interfaces
{
	public enum UserRole 
	{
		UserRoleOwner,
		UserRoleDispatch,
		UserRoleDriver
	};

	public interface IUser : IMainData
	{
		string UserName{ get; set; }
		string Email{ get; set; }
		UserRole Role{ get; set; }
		IUserLocation location{ get; set; }	
	}
}

