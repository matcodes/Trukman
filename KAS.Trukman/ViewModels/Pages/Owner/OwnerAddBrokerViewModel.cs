using KAS.Trukman.AppContext;
using KAS.Trukman.Classes;
using KAS.Trukman.Data.Infos;
using KAS.Trukman.Languages;
using KAS.Trukman.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KAS.Trukman.ViewModels.Pages.Owner
{
    #region OwnerAddBrokerViewModel
    public class OwnerAddBrokerViewModel : PageViewModel
    {
        public OwnerAddBrokerViewModel() : base()
        {
            this.SubmitCommand = new VisualCommand(this.Submit);
            this.ShowHomePageCommand = new VisualCommand(this.ShowHomePage);
            this.ShowMainMenuCommand = new VisualCommand(this.ShowMainMenu);
        }

        public override void Initialize(params object[] parameters)
        {
            base.Initialize(parameters);
        }

        protected override void Localize()
        {
            base.Localize();

            this.Title = AppLanguages.CurrentLanguage.OwnerAddBrokerPageName;
        }

        private void ShowMainMenu(object parameter)
        {
            ShowMainMenuMessage.Send();
        }

        private void ShowHomePage(object parameter)
        {
            PopToRootPageMessage.Send();
        }

        private void Submit(object parameter)
        {
            Task.Run(async () =>
            {
                try
                {
                    this.IsBusy = true;
                    var brokerInfo = new BrokerInfo
                    {
                        Address = this.Address,
                        ContactName = this.ContactName,
                        ContactTitle = this.ContactTitle,
                        DocketNumber = this.DocketNumber,
                        Email = this.Email,
                        Name = this.Name,
                        Phone = this.Phone,
                        State = this.State,
                        ZIP = this.ZIP
                    };

                    await TrukmanContext.SaveBrokerAsync(brokerInfo);

                    PopPageMessage.Send();
                }
                finally
                {
                    this.IsBusy = false;
                }
            });
        }

        public string Name
        {
            get { return (string)this.GetValue("Name"); }
            set { this.SetValue("Name", value); }
        }

        public string Email
        {
            get { return (string)this.GetValue("Email"); }
            set { this.SetValue("Email", value); }
        }

        public string Address
        {
            get { return (string)this.GetValue("Address"); }
            set { this.SetValue("Address", value); }
        }

        public string State
        {
            get { return (string)this.GetValue("State"); }
            set { this.SetValue("State", value); }
        }

        public string ZIP
        {
            get { return (string)this.GetValue("ZIP"); }
            set { this.SetValue("ZIP", value); }
        }

        public string Phone
        {
            get { return (string)this.GetValue("Phone"); }
            set { this.SetValue("Phone", value); }
        }

        public string ContactTitle
        {
            get { return (string)this.GetValue("ContactTitle"); }
            set { this.SetValue("ContactTitle", value); }
        }

        public string ContactName
        {
            get { return (string)this.GetValue("ContactName"); }
            set { this.SetValue("ContactName", value); }
        }

        public string DocketNumber
        {
            get { return (string)this.GetValue("DocketNumber"); }
            set { this.SetValue("DocketNumber", value); }
        }

        public VisualCommand ShowMainMenuCommand { get; private set; }

        public VisualCommand ShowHomePageCommand { get; private set; }

        public VisualCommand SubmitCommand { get; private set; }
    }
    #endregion
}
