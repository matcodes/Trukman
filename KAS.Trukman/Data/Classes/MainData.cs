using KAS.Trukman.Classes;
using KAS.Trukman.Data.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.Classes
{
    #region MainData
    public class MainData : BaseData, IMainData
    {
        public MainData()
        {
            this.UpdateTime = DateTime.Now;
        }

        #region IMainData
        [PrimaryKey]
        public string ID
        {
            get { return (string)this.GetValue("ID"); }
            set { this.SetValue("ID", value); }
        }

        public DateTime UpdateTime
        {
            get { return (DateTime)this.GetValue("UpdateTime", DateTime.MinValue); }
            set { this.SetValue("UpdateTime", value); }
        }
        #endregion
    }
    #endregion
}
