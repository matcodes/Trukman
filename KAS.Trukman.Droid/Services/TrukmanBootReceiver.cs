using Android.App;
using Android.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KAS.Trukman.Droid.Services
{
    #region TrukmanBootReceiver
    [BroadcastReceiver]
    [IntentFilter(new[] { "android.intent.action.BOOT_COMPLETED", "android.intent.action.MY_PACKAGE_REPLACED" })]
    public class TrukmanBootReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var serviceIntent = new Intent(context, typeof(TrukmanService));
            context.StartService(serviceIntent);
        }
    }
    #endregion
}