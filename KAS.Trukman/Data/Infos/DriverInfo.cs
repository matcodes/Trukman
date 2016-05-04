using KAS.Trukman.Data.Interfaces;
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

        public ICompany Company { get; set; }
    }
    #endregion
}
