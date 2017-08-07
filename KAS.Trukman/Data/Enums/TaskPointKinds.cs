using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.Enums
{
    #region TaskPointKinds
    public enum TaskPointKinds
    {
        ArrivalLoading = 0,
        EndLoading = 1,
        ArrivalUnloading = 2,
        EndUnloading = 3,
        DoneTask = 4,
        ArrivalLoadingInTime = 5,
        ArrivalLoading15MinEarly = 6,
        ArrivalLoadingLate = 7,
        ArrivalUnloadingInTime = 8,
        ArrivalUnloading15MinEarly = 9,
        ArrivalUnloadingLate = 10,
        SendPhoto = 11
    }
    #endregion
}
