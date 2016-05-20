using KAS.Trukman.Messages;
using KAS.Trukman.ViewModels.Pages;
using KAS.Trukman.ViewModels.Pages.Owner;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Views.Pages.Owner
{
    #region OwnerMainPage
    public class OwnerMainPage : MasterDetailPage
    {
        private NavigationPage _navigationPage = null;

        public OwnerMainPage()
            : base()
        {
            this.BindingContext = new OwnerMainViewModel();

            var mainMenuPage = new OwnerMainMenuPage();
            mainMenuPage.BindingContext = this.ViewModel.MainMenuViewModel;

            var homePage = new OwnerHomePage();

            _navigationPage = new NavigationPage(homePage);

            this.Master = mainMenuPage;
            this.Detail = _navigationPage;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.SubscribeMessages();
        }

        protected override void OnDisappearing()
        {
            this.UnsubscribeMessage();

            base.OnDisappearing();
        }

        private void SubscribeMessages()
        {
            PopPageMessage.Subscribe(this, this.PopPage);
            PopToRootPageMessage.Subscribe(this, this.PopToRootPage);
            ShowMainMenuMessage.Subscribe(this, this.ShowMainMenu);
            HideMainMenuMessage.Subscribe(this, this.HideMainMenu);
            ShowOwnerFleetPageMessage.Subscribe(this, this.ShowOwnerFleetPage);
			ShowOwnerDeliveryUpdateMessage.Subscribe (this, this.ShowOwnerJobList);
            ShowOwnerDriverAuthorizationPageMessage.Subscribe(this, this.ShowOwnerDriverAuthorizationPage);
            ShowOwnerFuelAdvancePageMessage.Subscribe(this, this.ShowOwnerFuelAdvancePage);
            ShowOwnerLumperPageMessage.Subscribe(this, this.ShowOwnerLumperPage);
            ShowOwnerBrockerListMessage.Subscribe(this, this.ShowOwnerBrockerListPage);
        }

        private void UnsubscribeMessage()
        {
            PopPageMessage.Unsubscribe(this);
            PopToRootPageMessage.Unsubscribe(this);
            ShowMainMenuMessage.Unsubscribe(this);
            HideMainMenuMessage.Unsubscribe(this);
            ShowOwnerFleetPageMessage.Unsubscribe(this);
			ShowOwnerDeliveryUpdateMessage.Unsubscribe (this);
            ShowOwnerDriverAuthorizationPageMessage.Unsubscribe(this);
            ShowOwnerFuelAdvancePageMessage.Unsubscribe(this);
            ShowOwnerLumperPageMessage.Unsubscribe(this);
            ShowOwnerBrockerListMessage.Unsubscribe(this);
        }

        private void PushPage(Page page)
        {
            Device.BeginInvokeOnMainThread(async () => {
                await _navigationPage.PushAsync(page);
            });
        }

        private void PopPage()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await _navigationPage.PopAsync(true);
            });
        }

        private void PopToRootPage()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await _navigationPage.PopToRootAsync(true);
            });
        }

        private void PopPage(PopPageMessage message)
        {
            this.PopPage();
        }

        private void PopToRootPage(PopToRootPageMessage message)
        {
            this.PopToRootPage();
        }

        private void ShowMainMenu(ShowMainMenuMessage message)
        {
            this.IsPresented = true;
        }

        private void HideMainMenu(HideMainMenuMessage message)
        {
            this.IsPresented = false;
        }

        private void ShowOwnerFleetPage(ShowOwnerFleetPageMessage message)
        {
            var page = new OwnerFleetPage();
            page.ViewModel.Initialize();
            this.PushPage(page);
        }

        private void ShowOwnerDriverAuthorizationPage(ShowOwnerDriverAuthorizationPageMessage message)
        {
            var page = new DriverAuthorizationPage();
            page.ViewModel.Initialize(message.CompanyName, message.Driver);
            this.PushPage(page);
        }

		private void ShowOwnerJobList(ShowOwnerDeliveryUpdateMessage message)
		{
			var page = new OwnerCurrentJobListPage ();
			page.ViewModel.Initialize ();
			this.PushPage (page);
		}

        private void ShowOwnerFuelAdvancePage(ShowOwnerFuelAdvancePageMessage message)
        {
            var page = new OwnerFuelAdvancePage();
            page.ViewModel.Initialize();
            this.PushPage(page);
        }

        private void ShowOwnerLumperPage(ShowOwnerLumperPageMessage message)
        {
            var page = new OwnerLumperPage();
            page.ViewModel.Initialize();
            this.PushPage(page);
        }

        private void ShowOwnerBrockerListPage(ShowOwnerBrockerListMessage message)
        {
            var page = new OwnerBrokerListPage();
            page.ViewModel.Initialize();
            this.PushPage(page);
        }

        public OwnerMainViewModel ViewModel
        {
            get { return (this.BindingContext as OwnerMainViewModel); }
        }
    }
    #endregion
}
