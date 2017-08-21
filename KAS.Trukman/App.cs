using KAS.Trukman.Messages;
using KAS.Trukman.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Trukman.Helpers;
using Trukman;
using KAS.Trukman.AppContext;
using KAS.Trukman.Views.Pages.Owner;
using KAS.Trukman.Views.Pages.SignUp;
using KAS.Trukman.Data.Enums;
using KAS.Trukman.Data.Classes;

namespace KAS.Trukman
{
    #region App
    public class App : Application
    {
        public App()
        {
            this.MainPage = new EmptyPage();
        }

        protected override void OnStart()
        {
            base.OnStart();

            this.Appering();

            this.ShowTopPage();
        }

        private void ShowMainMenu(ShowMainPageMessage message)
        {
            this.MainPage = new MainPage();
        }

        private void PopPage(PopPageMessage message)
        {
            if (this.MainPage is NavigationPage)
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await (this.MainPage as NavigationPage).PopAsync();
                });
        }

        private void PopToRootPage(PopToRootPageMessage message)
        {
            if (this.MainPage is NavigationPage)
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await (this.MainPage as NavigationPage).PopToRootAsync();
                });
        }

        private async void ShowOwnerSignUpWelcomePage(ShowSignUpOwnerWelcomePageMessage message)
        {
            if (this.MainPage is NavigationPage)
            {
                var page = new SignUpOwnerWelcomePage();
                page.ViewModel.Initialize(message.Company);
                await (this.MainPage as NavigationPage).PushAsync(page);
            }
        }

        private void ShowDriverAuthorizationPage(ShowOwnerDriverAuthorizationPageMessage message)
        {
            if (this.MainPage is NavigationPage)
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var page = new DriverAuthorizationPage();
                    page.ViewModel.Initialize(message.CompanyName, message.Driver);
                    await (this.MainPage as NavigationPage).PushAsync(page);
                });
        }

        protected override void OnSleep()
        {
//            this.Disappering();
        }

        protected override void OnResume()
        {
//            this.Appering();

 //           this.ShowTopPage();
        }

        private void Appering()
        {
            TrukmanContext.AppWorking = true;

            ShowTopPageMessage.Subscribe(this, this.ShowTopPage);
            StartOwnerMainPageMessage.Subscribe(this, this.StartOwnerMainPage);
        }

        private void Disappering()
        {
            TrukmanContext.AppWorking = false;

            ShowTopPageMessage.Unsubscribe(this);
            StartOwnerMainPageMessage.Unsubscribe(this);
        }

        private void ShowTopPage()
        {
            var timer = new System.Timers.Timer { Interval = 200 };
            timer.Elapsed += (sender, args) => {
                timer.Stop();
                if (TrukmanContext.Initialized)
                    this.ShowTopPage(new ShowTopPageMessage());
                else
                    this.ShowTopPage();
            };
            timer.Start();
        }

        private void ShowTopPage(ShowTopPageMessage message)
        {
            Device.BeginInvokeOnMainThread(() => {
                try
                {
                    bool isEmpty = (this.MainPage is EmptyPage);

                    if (TrukmanContext.User == null)
                        this.MainPage = new SignUpNavigationPage();
                    else if (TrukmanContext.User.Role == UserRole.Owner)
                        this.MainPage = new OwnerMainPage();
                    else if (TrukmanContext.User.Role == UserRole.Driver)
                    {
                        if (TrukmanContext.Company == null)
                        {
                            this.MainPage = new SignUpNavigationPage();
                        }
                        else
                        {
                            var driverState = (DriverState)TrukmanContext.User.Status;
                            if (driverState == DriverState.Joined)
                                this.MainPage = new MainPage();
                            else
                                this.MainPage = new SignUpNavigationPage(driverState, TrukmanContext.Company as Company);
                        }
                    }

                    if (isEmpty)
                        MainPageInitializedMessage.Send();
                }
                catch (Exception exception)
                {
                    ShowToastMessage.Send(exception.Message);
                    this.MainPage = new SignUpNavigationPage();
                }
            });
        }

        private void StartOwnerMainPage(StartOwnerMainPageMessage message)
        {
            Device.BeginInvokeOnMainThread(() => {
                this.MainPage = new OwnerMainPage();
            });
        }
    }
    #endregion
}
