using System;
using Xamarin.Forms.Maps;

namespace Trukman.Interfaces
{
	public enum UserRole {
		UserRoleOwner = 0,
		UserRoleDispatch = 1,
		UserRoleDriver = 2
	};

	public interface IUser
	{
		string UserName { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
		string Email { get; set; }
		UserRole Role{ get; set; }
		Position position { get; set; }
	}
}

