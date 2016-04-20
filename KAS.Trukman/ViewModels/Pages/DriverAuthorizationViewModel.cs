using KAS.Trukman.Classes;
using KAS.Trukman.Languages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trukman.Interfaces;

namespace KAS.Trukman.ViewModels.Pages
{
    #region DriverAuthorizationViewModel
    public class DriverAuthorizationViewModel : PageViewModel
    {
        public DriverAuthorizationViewModel() 
            : base()
        {
            this.ShowOwnerMainMenuCommand = new VisualCommand(this.ShowOwnerMainMenu);
            this.AuthorizeCommand = new VisualCommand(this.Authorize);
            this.DeclineCommand = new VisualCommand(this.Decline);
        }

        public override void Initialize(params object[] parameters)
        {
            base.Initialize(parameters);

            this.CompanyName = (parameters != null && parameters.Length > 0 ? (parameters[0].ToString()) : "");
            this.Driver = (parameters != null && parameters.Length > 1 ? (parameters[1] as IUser) : null);

            this.AssignIDNumber = "";
        }

        public override void Appering()
        {
            base.Appering();
        }

        public override void Disappering()
        {
            base.Disappering();
        }

        protected override void Localize()
        {
            base.Localize();

            this.Title = AppLanguages.CurrentLanguage.DriverAuthorizePageName;
            this.CommonText = String.Format(AppLanguages.CurrentLanguage.DriverAuthorizationCommonLabel, this.FirstName, this.LastName);
        }

        protected override void DoPropertyChanged(string propertyName)
        {
            if (propertyName == "Driver")
            {
                this.FirstName = (this.Driver != null ? this.Driver.FirstName : "");
                this.LastName = (this.Driver != null ? this.Driver.LastName : "");
            }
            else if ((propertyName == "FirstName") || (propertyName == "LastName"))
                this.Localize();

            base.DoPropertyChanged(propertyName);
        }

        private void ShowOwnerMainMenu(object parameter)
        {
        }

        private void Authorize(object parameter)
        {
            Task.Run(() => {
                this.IsBusy = true;
                this.DisableCommands();
                try
                {
//                    App.ServerManager.AcceptUserToCompany()
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    // To do: Show exception message
                }
                finally
                {
                    this.EnabledCommands();
                    this.IsBusy = false;
                }
            });
        }

        private void Decline(object parameter)
        {
            Task.Run(() => {
                this.IsBusy = true;
                this.DisableCommands();
                try
                {
                    //                    App.ServerManager.AcceptUserToCompany()
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    // To do: Show exception message
                }
                finally
                {
                    this.EnabledCommands();
                    this.IsBusy = false;
                }
            });
        }

        public string CompanyName
        {
            get { return (string)this.GetValue("CompanyName"); }
            set { this.SetValue("CompanyName", value); }
        }

        public IUser Driver
        {
            get { return (this.GetValue("Driver") as IUser); }
            set { this.SetValue("Driver", value); }
        }

        public string FirstName
        {
            get { return (string)this.GetValue("FirstName"); }
            set { this.SetValue("FirstName", value); }
        }

        public string LastName
        {
            get { return (string)this.GetValue("LastName"); }
            set { this.SetValue("LastName", value); }
        }

        public string AssignIDNumber
        {
            get { return (string)this.GetValue("AssignIDNumber"); }
            set { this.SetValue("AssignIDNumber", value); }
        }

        public string CommonText
        {
            get { return (string)this.GetValue("CommonText"); }
            set { this.SetValue("CommonText", value); }
        }

        public VisualCommand ShowOwnerMainMenuCommand { get; private set; }

        public VisualCommand AuthorizeCommand { get; private set; }

        public VisualCommand DeclineCommand { get; private set; }
    }
    #endregion
}
