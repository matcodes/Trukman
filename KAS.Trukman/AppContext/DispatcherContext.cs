using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KAS.Trukman.Storage;
using System.Threading.Tasks;
using KAS.Trukman.Messages;
using KAS.Trukman.Extensions;

namespace KAS.Trukman.AppContext
{
    #region DispatcherContext
    public class DispatcherContext
    {
        private LocalStorage _localStorage = null;

        private bool _isNotification = false;

        public DispatcherContext(LocalStorage localStorage) : base()
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
            Task.Run(async () =>
            {
                if (!_isNotification)
                {
                    _isNotification = true;
                    try
                    {
                        var notification = await _localStorage.GetNotification();
                        if (notification != null)
                            ShowNotificationMessage.Send(notification.Text);
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
            }).LogExceptions("DispatcherContext CheckNotification");
        }
    }
    #endregion
}