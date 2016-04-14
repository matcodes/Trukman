using System;
using KAS.Trukman.Data.Classes;
using Trukman.Data.Interfaces;

namespace Trukman.Data.Classes
{
	public class User : MainData, IUser
	{
		public User () : base()
		{
		}

		#region IUser interface
		public string UserName {
			get { return (string)this.GetValue ("UserName"); }
			set { this.SetValue ("UserName", value); }
		}

		public string Email {
			get { return (string)this.GetValue ("Email"); }
			set { this.SetValue ("Email", value); }
		}

		public UserRole Role {
			get { return (UserRole)this.GetValue ("Role"); }
			set { this.SetValue ("Role", value); }
		}

		public IUserLocation location {
			get { return (this.GetValue ("location") as IUserLocation); }
			set { this.SetValue ("location", value); }
		}
		#endregion
	}
}

