using KAS.Trukman.Classes;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Storage
{
    #region SettingsItem
    public class SettingsItem : BaseData
    {
        [PrimaryKey]
        public string Key
        {
            get { return (string)this.GetValue("Key"); }
            set { this.SetValue("Key", value); }
        }

        public string Value
        {
            get { return (string)this.GetValue("Value"); }
            set { this.SetValue("Value", value); }
        }
    }
    #endregion
}
