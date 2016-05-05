using Android.OS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace KAS.Trukman.Droid.Services
{
    #region TrukmanServiceBinder
    public class TrukmanServiceBinder : Binder
    {
        public TrukmanServiceBinder(TrukmanService trukmanService)
            : base()
        {
            this.TrukmanService = trukmanService;
        }

        public TrukmanService TrukmanService { get; set; }
    }
    #endregion
}