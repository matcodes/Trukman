using System;
using Parse;

namespace KAS.Trukman
{
	#region ParseJobAlert
	[ParseClassName("JobAlert")]
	public class ParseJobAlert : ParseObject
	{
		[ParseFieldName("AlertType")]
		public int AlertType
		{
			get { return this.GetProperty<int> ((int)0); }
			set { this.SetProperty<int> (0); }
		}

		[ParseFieldName("AlertText")]
		public string AlertText
		{
			get { return this.GetProperty<string> (); }
			set { this.SetProperty<string> (value); }
		}
	}
	#endregion
}

