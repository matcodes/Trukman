using KAS.Trukman.Classes;
using KAS.Trukman.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.ViewModels.Commands
{
    #region PageCommandsViewModel
    public class PageCommandsViewModel : BaseViewModel
    {
        public PageCommandsViewModel() 
            : base("", "")
        {
            this.HomeCommand = new VisualCommand(this.Home);
            this.MailCommand = new VisualCommand(this.Mail);
            this.MapCommand = new VisualCommand(this.Map);
            this.LocationCommand = new VisualCommand(this.Location);
            this.CameraCommand = new VisualCommand(this.Camera);
        }

        public void DisableCommands()
        {
            Device.BeginInvokeOnMainThread(() => {
                this.HomeCommand.IsEnabled = false;
                this.MailCommand.IsEnabled = false;
                this.MapCommand.IsEnabled = false;
                this.LocationCommand.IsEnabled = false;
                this.CameraCommand.IsEnabled = false;
            });
        }

        public void EnableCommands()
        {
            Device.BeginInvokeOnMainThread(() => {
                this.HomeCommand.IsEnabled = true;
                this.MailCommand.IsEnabled = true;
                this.MapCommand.IsEnabled = true;
                this.LocationCommand.IsEnabled = true;
                this.CameraCommand.IsEnabled = true;
            });
        }

        private void Home(object parameter)
        {
            this.DisableCommands();
            try
            {
                PopToRootPageMessage.Send();
            }
            catch (Exception exception)
            {
                // To do: Show exception message
                Console.WriteLine(exception);
            }
            finally
            {
                this.EnableCommands();
            }
        }

        private void Mail(object parameter)
        {
            this.DisableCommands();
            try
            {
                // To do: Show mail page
            }
            catch (Exception exception)
            {
                // To do: Show exception message
                Console.WriteLine(exception);
            }
            finally
            {
                this.EnableCommands();
            }
        }

        private void Map(object parameter)
        {
            this.DisableCommands();
            try
            {
                // To do: Show map page
            }
            catch (Exception exception)
            {
                // To do: Show exception message
                Console.WriteLine(exception);
            }
            finally
            {
                this.EnableCommands();
            }
        }

        private void Location(object parameter)
        {
            this.DisableCommands();
            try
            {
                // To do: Show location page
            }
            catch (Exception exception)
            {
                // To do: Show exception message
                Console.WriteLine(exception);
            }
            finally
            {
                this.EnableCommands();
            }
        }

        private void Camera(object parameter)
        {
            this.DisableCommands();
            try
            {
                TakePhotoFromCameraMessage.Send();
            }
            catch (Exception exception)
            {
                // To do: Show exception message
                Console.WriteLine(exception);
            }
            finally
            {
                this.EnableCommands();
            }
        }

        public VisualCommand HomeCommand { get; private set; }

        public VisualCommand MailCommand { get; private set; }

        public VisualCommand MapCommand { get; private set; }

        public VisualCommand LocationCommand { get; private set; }

        public VisualCommand CameraCommand { get; private set; }
    }
    #endregion
}
