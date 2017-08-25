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
        public SignUpNavigationPage() : base(new SignUpMainPage())
        {
            this.BindingContext = new SignUpNavigationViewModel();
        }

        public SignUpNavigationPage(UserState state, Company company) : this()
        {
            if (state == UserState.Waiting)
                this.ShowSignUpUserPendingPage(new ShowSignUpUserPendingPageMessage(company));
            else if (state == UserState.Declined)
                ShowSignUpUserDeclinedPage(new ShowSignUpUserDeclinedPageMessage(company));
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
            ShowSignUpUserPendingPageMessage.Subscribe(this, this.ShowSignUpUserPendingPage);
            ShowSignUpUserDeclinedPageMessage.Subscribe(this, this.ShowSignUpUserDeclinedPage);
            ShowSignUpUserAuthorizedPageMessage.Subscribe(this, this.ShowSignUpUserAuthorizedPage);
            ShowSignUpDispatcherPageMessage.Subscribe(this, this.ShowSignUpDispatcherPage);
        }

        private void Unsubscribe()
        {
            ShowSignUpDriverPageMessage.Unsubscribe(this);
            ShowSignUpOwnerWelcomePageMessage.Unsubscribe(this);
            ShowSignUpOwnerCompanyPageMessage.Unsubscribe(this);
            ShowSignUpOwnerMCPageMessage.Unsubscribe(this);
            PopToRootPageMessage.Unsubscribe(this);
            PopPageMessage.Unsubscribe(this);
            ShowSignUpUserPendingPageMessage.Unsubscribe(this);
            ShowSignUpUserDeclinedPageMessage.Unsubscribe(this);
            ShowSignUpUserAuthorizedPageMessage.Unsubscribe(this);
            ShowSignUpDispatcherPageMessage.Unsubscribe(this);
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

        private void ShowSignUpUserPendingPage(ShowSignUpUserPendingPageMessage message)
        {
            var page = new SignUpUserPendingPage();
            page.ViewModel.Initialize(message.Company);
            this.PushPage(page);
        }

        private void ShowSignUpUserDeclinedPage(ShowSignUpUserDeclinedPageMessage message)
        {
            var page = new SignUpUserDeclinedPage();
            page.ViewModel.Initialize(message.Company);
            this.PushPage(page);
        }

        private void ShowSignUpUserAuthorizedPage(ShowSignUpUserAuthorizedPageMessage message)
        {
            var page = new SignUpUserAuthorizedPage();
            page.ViewModel.Initialize(message.Company);
            this.PushPage(page);
        }

        private void ShowSignUpDispatcherPage(ShowSignUpDispatcherPageMessage message)
        {
            var page = new SignUpDispatcherPage();
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
