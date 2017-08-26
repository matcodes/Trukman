using KAS.Trukman.Classes;
using KAS.Trukman.Data.Classes;
using KAS.Trukman.AppContext;
using KAS.Trukman.Languages;
using KAS.Trukman.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KAS.Trukman.Extensions;
using KAS.Trukman.Data.Enums;

namespace KAS.Trukman.ViewModels.Pages
{
    #region OwnerUserAuthorizationViewModel
    public class OwnerUserAuthorizationViewModel : PageViewModel
    {
        public OwnerUserAuthorizationViewModel() : base()
        {
            this.ShowOwnerMainMenuCommand = new VisualCommand(this.ShowOwnerMainMenu);
            this.AuthorizeCommand = new VisualCommand(this.Authorize);
            this.DeclineCommand = new VisualCommand(this.Decline);
        }

        public override void Initialize(params object[] parameters)
        {
            base.Initialize(parameters);

            this.CompanyName = (parameters != null && parameters.Length > 0 ? (parameters[0].ToString()) : "");
            this.User = (parameters != null && parameters.Length > 1 ? (parameters[1] as User) : null);
            this.CompanyID = (parameters != null && parameters.Length > 0 ? (parameters[2].ToString()) : "");

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

            this.Title = AppLanguages.CurrentLanguage.UserAuthorizePageName;
            if (this.User != null)
            {
                var role = "";
                if (this.User.Role == UserRole.Dispatch)
                    role = "dispatcher";
                else if (this.User.Role == UserRole.Driver)
                    role = "driver";
                this.CommonText = String.Format(AppLanguages.CurrentLanguage.UserAuthorizationCommonLabel, this.FirstName, this.LastName, role);
            }
        }

        protected override void DoPropertyChanged(string propertyName)
        {
            if (propertyName == "User")
            {
                this.FirstName = (this.User != null ? this.User.FirstName : "");
                this.LastName = (this.User != null ? this.User.LastName : "");

                // To do: get first and last names from user object
                if (((String.IsNullOrEmpty(this.FirstName)) || (String.IsNullOrEmpty(this.LastName))) && (this.User != null))
                {
                    var names = this.User.UserName.Split(' ');
                    if (String.IsNullOrEmpty(FirstName))
                        this.FirstName = (names.Length > 0 ? names[0] : "");
                    if (String.IsNullOrEmpty(this.LastName))
                        this.LastName = (names.Length > 1 ? names[1] : "");
                }
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
            Task.Run(async () =>
            {
                this.IsBusy = true;
                this.DisableCommands();
                try
                {
                    await TrukmanContext.AcceptUserToCompany(this.CompanyID, this.User); // await App.ServerManager.AcceptUserToCompany(this.CompanyName, this.User);
                    PopPageMessage.Send();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    ShowToastMessage.Send(exception.Message);
                }
                finally
                {
                    this.EnabledCommands();
                    this.IsBusy = false;
                }
            }).LogExceptions("OwnerUserAuthorizationViewModel Authorize");
        }

        private void Decline(object parameter)
        {
            Task.Run(async () =>
            {
                this.IsBusy = true;
                this.DisableCommands();
                try
                {
                    await TrukmanContext.DeclineUserToCompany(this.CompanyID, this.User); //  await App.ServerManager.DeclineUserFromCompany(this.CompanyName, this.User);
                    PopPageMessage.Send();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    ShowToastMessage.Send(exception.Message);
                }
                finally
                {
                    this.EnabledCommands();
                    this.IsBusy = false;
                }
            }).LogExceptions("OwnerUserAuthorizationViewModel Decline");
        }

        public string CompanyName
        {
            get { return (string)this.GetValue("CompanyName"); }
            set { this.SetValue("CompanyName", value); }
        }

        public User User
        {
            get { return (this.GetValue("User") as User); }
            set { this.SetValue("User", value); }
        }

        public string CompanyID
        {
            get { return (string)this.GetValue("CompanyID"); }
            set { this.SetValue("CompanyID", value); }
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
