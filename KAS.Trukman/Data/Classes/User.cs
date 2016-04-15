using System;
using Trukman.Interfaces;
using KAS.Trukman.Data.Classes;
using KAS.Trukman.Data.Interfaces;

namespace KAS.Trukman
{
	public class User : MainData, IUser
	{
		public string UserName{ get; set; }
		public string Email{ get; set; }
		public UserRole Role{ get; set; }
		public IUserLocation location{ get; set; }
	}
}
