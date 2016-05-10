using System;
using Xamarin.Forms.Maps;

namespace KAS.Trukman.Data.Interfaces
{
    #region UserRole
    public enum UserRole
    {
		UserRoleOwner = 0,
		UserRoleDispatch = 1,
		UserRoleDriver = 2
	};
    #endregion

    #region IUser
    public interface IUser : IMainData
	{
		string UserName { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }

        string Email { get; set; }

        string Phone { get; set; }

        UserRole Role{ get; set; }

        Position Position { get; set; }

        int Status { get; set; }
	}
    #endregion
}

