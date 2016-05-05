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
    #region ServiceHelper
    public class TrukmanServiceHelper
    {
        private MainActivity _activity;
        private TrukmanServiceConnection _trukmanServiceConnection;

        public TrukmanServiceHelper(MainActivity activity)
        {
            _activity = activity;
        }

        public void OnCreate()
        {
            this.StartService();
        }

        public void OnStop()
        {
            this.UnbindService();
        }

        public void OnDestroy()
        {
            this.UnbindService();
        }

        public void OnStart()
        {
            this.BindService();
        }

        private void StartService()
        {
            try
            {
                var intent = new Intent(_activity, typeof(TrukmanService));
                _activity.StartService(intent);
            }
            // Analysis disable once EmptyGeneralCatchClause
            catch
            {
            }
        }

        private void BindService()
        {
            if (!IsBound)
            {
                var serviceIntent = new Intent(_activity, typeof(TrukmanService));
                _trukmanServiceConnection = new TrukmanServiceConnection(this);
                _activity.BindService(serviceIntent, _trukmanServiceConnection, Bind.AutoCreate);
            }
        }

        private void UnbindService()
        {
            if (this.IsBound)
            {
                _activity.UnbindService(_trukmanServiceConnection);
                this.IsBound = false;
            }
        }

        public bool IsBound { get; set; }

        public TrukmanServiceBinder Binder { get; set; }
    }
    #endregion
}