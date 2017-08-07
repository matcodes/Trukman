using KAS.Trukman.Data.Enums;
using KAS.Trukman.Languages;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Extensions
{
    public static class TaskPointKindsExtension
    {
        public static string ToText(this TaskPointKinds kind)
        {
            if (kind == TaskPointKinds.ArrivalLoading)
                return AppLanguages.CurrentLanguage.ArrivalLoadingJobPointsText;
            else if (kind == TaskPointKinds.EndLoading)
                return AppLanguages.CurrentLanguage.EndLoadingJobPointsText;
            else if (kind == TaskPointKinds.ArrivalUnloading)
                return AppLanguages.CurrentLanguage.ArrivalUnloadingJobPointsText;
            else if (kind == TaskPointKinds.EndUnloading)
                return AppLanguages.CurrentLanguage.EndUnloadingJobPointsText;
            else if (kind == TaskPointKinds.DoneTask)
                return AppLanguages.CurrentLanguage.DoneTaskJobPointsText;
            else if (kind == TaskPointKinds.ArrivalLoadingInTime)
                return AppLanguages.CurrentLanguage.PickUpOnTimeJobPointsText;
            else if (kind == TaskPointKinds.ArrivalLoading15MinEarly)
                return AppLanguages.CurrentLanguage.PickUpOnTimeEarlyJobPointsText;
            else if (kind == TaskPointKinds.ArrivalLoadingLate)
                return AppLanguages.CurrentLanguage.PickUpLateJobPointsText;
            else if (kind == TaskPointKinds.ArrivalUnloadingInTime)
                return AppLanguages.CurrentLanguage.DeliveryOnTimeJobPointsText;
            else if (kind == TaskPointKinds.ArrivalUnloading15MinEarly)
                return AppLanguages.CurrentLanguage.DeliveryOnTimeEarlyJobPointsText;
            else if (kind == TaskPointKinds.ArrivalUnloadingLate)
                return AppLanguages.CurrentLanguage.DeliveryLateJobPointsText;
            else if (kind == TaskPointKinds.SendPhoto)
                return AppLanguages.CurrentLanguage.SendPhotoJobPointsText;
            else
                return "";
        }
    }
}
