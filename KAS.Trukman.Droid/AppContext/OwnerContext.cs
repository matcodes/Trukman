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
using KAS.Trukman.Storage;
using Parse;
using System.Threading.Tasks;
using KAS.Trukman.Messages;

namespace KAS.Trukman.Droid.AppContext
{
    #region OwnerContext
    public class OwnerContext
    {
        private LocalStorage _localStorage = null;

        private bool _isNotification = false;

        public OwnerContext(LocalStorage localStorage)
            : base()
        {
            _localStorage = localStorage;

            _localStorage.InitializeOwnerNotification();
        }

        public void Synchronize()
        {
            this.CheckNotification();
        }

        private void CheckNotification()
        {
            Task.Run(async () => {
                if (!_isNotification)
                {
                    _isNotification = true;
                    try
                    {
                        var notification = await _localStorage.GetNotification();
                        if (notification != null)
                            TrukmanContext.SendSystemMessage(notification.Text);
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                        ShowToastMessage.Send(exception.Message);
                    }
                    finally
                    {
                        _isNotification = false;
                    }
                }
            });
        }
    }
    #endregion
}