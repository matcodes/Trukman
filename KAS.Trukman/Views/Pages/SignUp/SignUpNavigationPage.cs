using KAS.Trukman.Data.Classes;
using KAS.Trukman.Data.Enums;
using KAS.Trukman.Messages;
using KAS.Trukman.ViewModels.Pages.SignUp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KAS.Trukman.Views.Pages.SignUp
{
    #region SignUpNavigationPage
    public class SignUpNavigationPage : NavigationPage
    {
        public SignUpNavigationPage()
            : base(new SignUpMainPage())
        {
            this.BindingContext = new SignUpNavigationViewModel();
        }

        public SignUpNavigationPage(DriverState state, Company company)
            : this()
        {
            if (state == DriverState.Waiting)
                this.ShowSignUpDriverPendingPage(new ShowSignUpDriverPendingPageMessage(company));
            else if (state == DriverState.Declined)
                ShowSignUpDriverDeclinedPage(new ShowSignUpDriverDeclinedPageMessage(company));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.ViewModel.Appering();

            this.Subscribe();
        }

        protected override void OnDisappearing()
        {
            this.Unsubscribe();

            this.ViewModel.Disappering();

            base.OnDisappearing();
        }

        private void PopPage()
        {
            Device.BeginInvokeOnMainThread(async () => {
                await this.PopAsync();
            });
        }

        private void PopToRootPage()
        {
            Device.BeginInvokeOnMainThread(async () => {
                await this.PopToRootAsync();
            });
        }

        private void PushPage(Page page)
        {
            Device.BeginInvokeOnMainThread(async () => {
                await this.PushAsync(page);
            });
        }

        private void Subscribe()
        {
            PopToRootPageMessage.Subscribe(this, this.PopToRootPage);
            PopPageMessage.Subscribe(this, this.PopPage);
            ShowSignUpOwnerMCPageMessage.Subscribe(this, this.ShowSignUpOwnerMCPage);
            ShowSignUpOwnerCompanyPageMessage.Subscribe(this, this.ShowSignUpOwnerCompanyPage);
            ShowSignUpOwnerWelcomePageMessage.Subscribe(this, this.ShowSignUpOwnerWelcomePage);
            ShowSignUpDriverPageMessage.Subscribe(this, this.ShowSignUpDriverPage);
            ShowSignUpDriverPendingPageMessage.Subscribe(this, this.ShowSignUpDriverPendingPage);
            ShowSignUpDriverDeclinedPageMessage.Subscribe(this, this.ShowSignUpDriverDeclinedPage);
            ShowSignUpDriverAuthorizedPageMessage.Subscribe(this, this.ShowSignUpDriverAuthorizedPage);
        }

        private void Unsubscribe()
        {
            ShowSignUpDriverPageMessage.Unsubscribe(this);
            ShowSignUpOwnerWelcomePageMessage.Unsubscribe(this);
            ShowSignUpOwnerCompanyPageMessage.Unsubscribe(this);
            ShowSignUpOwnerMCPageMessage.Unsubscribe(this);
            PopToRootPageMessage.Unsubscribe(this);
            PopPageMessage.Unsubscribe(this);
            ShowSignUpDriverPendingPageMessage.Unsubscribe(this);
            ShowSignUpDriverDeclinedPageMessage.Unsubscribe(this);
            ShowSignUpDriverAuthorizedPageMessage.Unsubscribe(this);
        }

        private void ShowSignUpOwnerMCPage(ShowSignUpOwnerMCPageMessage message)
        {
            var page = new SignUpOwnerMCPage();
            page.ViewModel.Initialize();
            this.PushPage(page);
        }

        private void ShowSignUpOwnerCompanyPage(ShowSignUpOwnerCompanyPageMessage message)
        {
            var page = new SignUpOwnerCompanyPage();
            page.ViewModel.Initialize(message.Info);
            this.PushPage(page);
        }

        private void ShowSignUpOwnerWelcomePage(ShowSignUpOwnerWelcomePageMessage message)
        {
            var page = new SignUpOwnerWelcomePage();
            page.ViewModel.Initialize(message.Company);
            this.PushPage(page);
        }

        private void ShowSignUpDriverPage(ShowSignUpDriverPageMessage message)
        {
            var page = new SignUpDriverPage();
            this.PushPage(page);
        }

        private void ShowSignUpDriverPendingPage(ShowSignUpDriverPendingPageMessage message)
        {
            var page = new SignUpDriverPendingPage();
            page.ViewModel.Initialize(message.Company);
            this.PushPage(page);
        }

        private void ShowSignUpDriverDeclinedPage(ShowSignUpDriverDeclinedPageMessage message)
        {
            var page = new SignUpDriverDeclinedPage();
            page.ViewModel.Initialize(message.Company);
            this.PushPage(page);
        }

        private void ShowSignUpDriverAuthorizedPage(ShowSignUpDriverAuthorizedPageMessage message)
        {
            var page = new SignUpDriverAuthorizedPage();
            page.ViewModel.Initialize(message.Company);
            this.PushPage(page);
        }

        private void PopPage(PopPageMessage message)
        {
            this.PopPage();
        }

        private void PopToRootPage(PopToRootPageMessage message)
        {
            this.PopToRootPage();
        }

        public SignUpNavigationViewModel ViewModel
        {
            get { return (this.BindingContext as SignUpNavigationViewModel); }
        }
    }
    #endregion
}
