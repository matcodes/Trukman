using System;

namespace KAS.Trukman.Data.Interfaces
{
    #region ICompany
    public interface ICompany : IMainData
	{
		string Name {get; set;}

        string DisplayName{ get; set;}
	}
    #endregion
}

