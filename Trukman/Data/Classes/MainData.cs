using Trukman.Data.Classes;
using Trukman.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Trukman.Data.Classes
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
