using System;
using Trukman.Interfaces;
using KAS.Trukman.Data.Classes;
using KAS.Trukman.Data.Interfaces;
using Xamarin.Forms.Maps;
using KAS.Trukman.Classes;
using SQLite;

namespace KAS.Trukman.Data.Classes
{
    #region User
    public class User : MainData, IUser
	{
		public string UserName
		{
			get { return (string)this.GetValue("UserName"); }
			set { this.SetValue("UserName", value); }
		}

		public string Email
		{
			get { return (string)this.GetValue("Email"); }
			set { this.SetValue("Email", value); }
		}

        public string Phone
        {
            get { return (string)this.GetValue("Phone"); }
            set { this.SetValue("Phone", value); }
        }

		public UserRole Role
		{
			get { return (UserRole)this.GetValue("Role", 0); }
			set { this.SetValue("Role", value); }
		}

        [Ignore]
		public Position Position 
		{
			get { return (Position)this.GetValue ("Position", default(Position)); }
			set { this.SetValue ("Position", value); }
		}

        public string FirstName
        {
            get { return (string)this.GetValue("FirstName"); }
			set { this.SetValue("FirstName", value); }
        }

        public string LastName
        {
            get { return (string)this.GetValue("LastName"); }
			set { this.SetValue("LastName", value); }
        }

        public int Status
        {
            get { return (int)this.GetValue("Status"); }
            set { this.SetValue("Status", value); }
        }
	}
    #endregion
}
