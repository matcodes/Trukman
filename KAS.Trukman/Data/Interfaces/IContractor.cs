using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.Interfaces
{
    #region IContractor
    public interface IContractor : IMainData
    {
        string Name { get; set; }

        string Phone { get; set; }

        string Fax { get; set; }

        string Address{ get; set; }
    }
    #endregion
}
