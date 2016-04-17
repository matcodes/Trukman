using System;
using Trukman.Interfaces;
using Parse;

namespace Trukman.Droid.ParseClasses
{
	[ParseClassName("JobAlert")]
	public class JobAlert : ParseObject, IJobAlert
	{
		public JobAlert ()
		{
		}
	}
}

