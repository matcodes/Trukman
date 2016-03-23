using System;

namespace Trukman
{
	public class Job
	{
		public string Name{get;set;}
		public string Description {get;set;}
	}

	public class User
	{
		public string UserName{get;set;}
		public string Email{get;set;}
		public UserRole Role{get;set;}
		public UserLocation location{get;set;}
	}
}

