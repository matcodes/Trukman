using System;
using KAS.Trukman.Data.Classes;
using Xamarin.Forms.Maps;
using KAS.Trukman.Classes;
using SQLite;
using KAS.Trukman.Data.Enums;

namespace KAS.Trukman.Data.Classes
{
    #region User
    public class User : MainData
	{
		public string UserName
		{
			get { return (string)this.GetValue("UserName", default(string)); }
			set { this.SetValue("UserName", value); }
		}

		public string Email
		{
			get { return (string)this.GetValue("Email", default(string)); }
			set { this.SetValue("Email", value); }
		}

        public string Password
        {
            get { return (string)this.GetValue("Password", default(string)); }
            set { this.SetValue("Password", value); }
        }

        public string Phone
        {
            get { return (string)this.GetValue("Phone", default(string)); }
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
            get { return (string)this.GetValue("FirstName", default(string)); }
			set { this.SetValue("FirstName", value); }
        }

        public string LastName
        {
            get { return (string)this.GetValue("LastName", default(string)); }
			set { this.SetValue("LastName", value); }
        }

        public int Status
        {
            get { return (int)this.GetValue("Status", 0); }
            set { this.SetValue("Status", value); }
        }

		[Ignore]
		public string FullName
		{
			get 
			{
				var fullName = String.Format ("{0} {1}", this.FirstName, this.LastName);
				if (string.IsNullOrEmpty(fullName.Trim()))
					fullName = this.UserName;
				return fullName;
			}
		}

        public string Token
        {
            get { return (string)this.GetValue("Token", default(string)); }
            set { this.SetValue("Token", value); }
        }

        [Ignore]
        public bool Verified
        {
            get
            {
                return !string.IsNullOrEmpty(this.Token);
            }
        }
    }
    #endregion
}
