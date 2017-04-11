using KAS.Trukman.Data.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.Infos
{
    #region DriverInfo
    public class DriverInfo
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string EMail { get; set; }

        public Company Company { get; set; }
    }
    #endregion
}
