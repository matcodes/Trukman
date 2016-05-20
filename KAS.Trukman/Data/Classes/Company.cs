using System;

namespace KAS.Trukman.Data.Classes
{
    #region Company
    public class Company : MainData
	{
        public Company()
        {
        }

        #region ICompany implementation
        public string Name
        {
			get { return (string)this.GetValue ("Name"); }
			set { this.SetValue ("Name", value); }
		}

		public string DisplayName
        {
			get { return (string)this.GetValue ("DisplayName"); }
			set { this.SetValue ("DisplayName", value); }
		}

        public int FleetSize
        {
            get { return (int)this.GetValue("FleetSize", (int)0); }
            set { this.SetValue("FleetSize", value); }
        }
		#endregion
	}
    #endregion
}

