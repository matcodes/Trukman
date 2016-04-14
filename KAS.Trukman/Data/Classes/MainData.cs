using KAS.Trukman.Classes;
using KAS.Trukman.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.Classes
{
    #region MainData
    public class MainData : BaseData, IMainData
    {
        #region IMainData
        public string ID
        {
            get { return (string)this.GetValue("ID"); }
            set { this.SetValue("ID", value); }
        }
        #endregion
    }
    #endregion
}
