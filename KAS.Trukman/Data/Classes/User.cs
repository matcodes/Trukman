using System;
using Trukman.Interfaces;
using KAS.Trukman.Data.Classes;
using KAS.Trukman.Data.Interfaces;
using Xamarin.Forms.Maps;

namespace KAS.Trukman
{
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

		public UserRole Role
		{
			get { return (UserRole)this.GetValue("Role", 0); }
			set { this.SetValue("Role", value); }
		}

		public Position position 
		{
			get { return (Position)this.GetValue ("position", default(Position)); }
			set { this.SetValue ("position", value); }
		}

        public string FirstName
        {
            get { return (string)this.GetValue("firstName"); }
			set { this.SetValue("firstName", value); }
        }

        public string LastName
        {
            get { return (string)this.GetValue("lastName"); }
			set { this.SetValue("lastName", value); }
        }
	}
}
