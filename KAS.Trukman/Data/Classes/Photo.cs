using KAS.Trukman.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace KAS.Trukman.Data.Classes
{
    #region Photo
    public class Photo : MainData
    {
        public string PhotoID
        {
            get { return (string)this.GetValue("PhotoID"); }
            set { this.SetValue("PhotoID", value); }
        }

        public string TripID
        {
            get { return (string)this.GetValue("TripID"); }
            set { this.SetValue("TripID", value); }
        }

        public string Type
        {
            get { return (string)this.GetValue("Type"); }
            set { this.SetValue("Type", value); }
        }

		[Ignore]
		public Trip Job
		{
			get { return (this.GetValue ("Job") as Trip); }
			set { this.SetValue ("Job", value); }
		}

		[Ignore]
		public Company Company {
			get { return (this.GetValue ("Company") as Company); }
			set { this.SetValue ("Company", value); }
		}

		[Ignore]
		public Uri Uri
		{
			get { return (this.GetValue ("Uri") as Uri); }
			set { this.SetValue ("Uri", value); }
		}

        public byte[] Data
        {
            get { return (byte[])this.GetValue("Data"); }
            set { this.SetValue("Data", value); }
        }

		public bool IsViewed
		{
			get { return (bool)this.GetValue ("IsViewed", false); }
			set { this.SetValue ("IsViewed", value); }
		}
    }
    #endregion
}
