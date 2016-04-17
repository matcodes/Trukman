using System;
using Parse;
using Trukman.Interfaces;

namespace Trukman.Droid.ParseClasses
{
	[ParseClassName("Alerts")]
	public class Alerts: ParseObject, IAlerts
	{
		#region IAlerts implementation

		public string Id { get; set; }

		#endregion



		public Alerts ()
		{
		}
	}
}

