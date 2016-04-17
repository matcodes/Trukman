using System;

namespace Trukman.Interfaces
{
	public enum UserRole {
		UserRoleOwner,
		UserRoleDispatch,
		UserRoleDriver
	};

	public interface IUser
	{
		string UserName { get; set; }
		string Email { get; set; }
		UserRole Role{ get; set; }
		IUserLocation location { get; set; }
	}
}

