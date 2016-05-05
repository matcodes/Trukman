using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace KAS.Trukman.Droid.Services
{
    #region TrukmanServiceConnection
    public class TrukmanServiceConnection : Java.Lang.Object, IServiceConnection
    {
        private readonly TrukmanServiceHelper _trukmanServiceHelper = null;

        public TrukmanServiceConnection(TrukmanServiceHelper trukmanServiceHelper)
        {
            _trukmanServiceHelper = trukmanServiceHelper;
        }

        #region IServiceConnection
        public void OnServiceConnected(ComponentName name, IBinder service)
        {
            var serviceBinder = (service as TrukmanServiceBinder);
            if (serviceBinder != null)
            {
                _trukmanServiceHelper.Binder = serviceBinder;
                _trukmanServiceHelper.IsBound = true;
            }
        }

        public void OnServiceDisconnected(ComponentName name)
        {
            _trukmanServiceHelper.IsBound = false;
        }
        #endregion
    }
    #endregion
}