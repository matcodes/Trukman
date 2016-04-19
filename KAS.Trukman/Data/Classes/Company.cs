using System;
using KAS.Trukman.Data.Classes;
using Trukman.Interfaces;

namespace Trukman.Classes
{
	public class Company: MainData, ICompany
	{
		#region ICompany implementation

		/*public string Id { 
			get { return this.GetValue ("Id"); }
			set { this.SetValue ("Id", value); }
		}*/

		public string Name {
			get { return (string)this.GetValue ("Name"); }
			set { this.SetValue ("Name", value); }
		}

		#endregion

		public Company ()
		{
		}
	}
}

