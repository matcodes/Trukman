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
using KAS.Trukman.Data.Interfaces;
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
        static IServerManager serverManager;
        public static IServerManager ServerManager
        {
            get
            {
                if (serverManager == null)
                    serverManager = DependencyService.Get<IServerManager>();
                return serverManager;
            }
        }

        static ILocationService locManager;
        public static ILocationService LocManager
        {
            get
            {
                if (locManager == null)
                    locManager = DependencyService.Get<ILocationService>();
                return locManager;
            }
        }


        static ILocationServicePlatformStarter locationServiceStarter;
        public static ILocationServicePlatformStarter LocationServiceStarter
        {
            get
            {
                if (locationServiceStarter == null)
                    locationServiceStarter = DependencyService.Get<ILocationServicePlatformStarter>();
                return locationServiceStarter;
            }
        }

        public App()
        {
            this.MainPage = new EmptyPage();

            CreateStyles();
        }

        void CreateStyles()
        {
            var entryRadiusStyle = new Style(typeof(TrukmanEditor)) {
                Setters = {
                    new Setter{ Property = TrukmanEditor.TextColorProperty, Value = Color.FromHex ("8D8D8D") },
                    new Setter{ Property = TrukmanEditor.PlaceholderColorProperty, Value = Color.FromHex ("8D8D8D") },
                    new Setter{ Property = TrukmanEditor.BackgroundColorProperty, Value = Color.Transparent },
                }
            };
            var buttonForEntryRadiusStyle = new Style(typeof(TrukmanButton)) {
                Setters = {
                    new Setter{ Property = TrukmanButton.TextColorProperty, Value = Color.Black },
                    new Setter{ Property = TrukmanButton.BackgroundColorProperty, Value = Color.White },
                    new Setter{ Property = TrukmanButton.BorderRadiusProperty, Value = 22 },
                }
            };

            var disabledEntryStyle = new Style(typeof(TrukmanEditor)) {
                Setters = {
                    new Setter{ Property = TrukmanEditor.TextColorProperty, Value = Color.Black },
                    new Setter{ Property = TrukmanEditor.BackgroundColorProperty, Value = Color.Transparent },
                }
            };

            var disabledButtonForEntryRadiusStyle = new Style(typeof(TrukmanButton)) {
                Setters = {
                    new Setter{ Property = TrukmanButton.TextColorProperty, Value = Color.FromHex ("7A7474") },
                    new Setter{ Property = TrukmanButton.BackgroundColorProperty, Value = Color.FromHex ("EAD2D2") },
                    new Setter{ Property = TrukmanButton.BorderRadiusProperty, Value = 22 }
                }
            };

            var buttonTransparentEntry = new Style(typeof(TrukmanButton)) {
                Setters = {
                    new Setter{ Property = TrukmanButton.BackgroundColorProperty, Value = Color.Transparent },
                    new Setter{ Property = TrukmanButton.BorderRadiusProperty, Value = 22 },
                    new Setter{ Property = TrukmanButton.BorderColorProperty, Value = Color.White },
                    new Setter{ Property = TrukmanButton.BorderWidthProperty, Value = 1.5 }
                }
            };

            Resources = new ResourceDictionary();
            Resources.Add("entryRadiusStyle", entryRadiusStyle);
            Resources.Add("buttonForEntryRadiusStyle", buttonForEntryRadiusStyle);
            Resources.Add("disabledEntryStyle", disabledEntryStyle);
            Resources.Add("disabledButtonForEntryRadiusStyle", disabledButtonForEntryRadiusStyle);
            Resources.Add("buttonTransparentEntry", buttonTransparentEntry);
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
                var page = new OwnerSignUpWelcomePage();
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
            this.Disappering();
        }

        protected override void OnResume()
        {
            this.Appering();

 //           this.ShowTopPage();
        }

        private void Appering()
        {
            TrukmanContext.AppWorking = true;

//            ShowSignUpOwnerWelcomePageMessage.Subscribe(this, this.ShowOwnerSignUpWelcomePage);
//            ShowMainPageMessage.Subscribe(this, this.ShowMainMenu);
            ShowTopPageMessage.Subscribe(this, this.ShowTopPage);
//            PopPageMessage.Subscribe(this, this.PopPage);
//            PopToRootPageMessage.Subscribe(this, this.PopToRootPage);
//            ShowOwnerDriverAuthorizationPageMessage.Subscribe(this, this.ShowDriverAuthorizationPage);
            StartOwnerMainPageMessage.Subscribe(this, this.StartOwnerMainPage);
        }

        private void Disappering()
        {
            TrukmanContext.AppWorking = false;

//            ShowMainPageMessage.Unsubscribe(this);
            ShowTopPageMessage.Unsubscribe(this);
//           PopPageMessage.Unsubscribe(this);
//            PopToRootPageMessage.Unsubscribe(this);
//            ShowSignUpOwnerWelcomePageMessage.Unsubscribe(this);
//            ShowOwnerDriverAuthorizationPageMessage.Unsubscribe(this);
            StartOwnerMainPageMessage.Unsubscribe(this);
        }

        //private void ShowTopPage(ShowTopPageMessage message)
        //{
        //    Device.BeginInvokeOnMainThread(() => {
        //        if (TrukmanContext.User == null)
        //            this.MainPage = new NavigationPage(new SignUpTypePage());
        //        else if (TrukmanContext.User.Role == UserRole.UserRoleDriver)
        //            this.MainPage = new MainPage();
        //        else if (TrukmanContext.User.Role == UserRole.UserRoleOwner)
        //        {
        //        }
        //        else if (TrukmanContext.User.Role == UserRole.UserRoleDispatch)
        //        {
        //        }
        //    });
        //}

        //private void ShowTopPage(ShowTopPageMessage message)
        //{
        //    Device.BeginInvokeOnMainThread(async () =>
        //    {
        //        //var _navigationPage = new NavigationPage ();
        //        //SettingsServiceHelper.SaveRejectedCounter(0);
        //        string companyName = SettingsServiceHelper.GetCompany();
        //        if (App.ServerManager.IsAuthorizedUser() && App.serverManager.GetCurrentUserRole() == UserRole.UserRoleOwner)
        //        {
        //            //                    var page = new OwnerSignUpWelcomePage();
        //            //                    page.ViewModel.Initialize(companyName);
        //            //                    this.MainPage = new NavigationPage(page);
        //            this.MainPage = new OwnerMainPage();
        //        }
        //        else {

        //            if (!App.ServerManager.IsAuthorizedUser() || string.IsNullOrEmpty(companyName))
        //            {
        //                this.MainPage = new NavigationPage(new SignUpTypePage());
        //            }
        //            else {
        //                var status = await App.serverManager.GetAuthorizationStatus(companyName);
        //                if (status == AuthorizationRequestStatus.Authorized)
        //                    this.MainPage = new MainPage();
        //                else if (status == AuthorizationRequestStatus.Pending)
        //                    this.MainPage = new NavigationPage(new PendingAuthorizationPage());
        //                else if (status == AuthorizationRequestStatus.Declined)
        //                    this.MainPage = new NavigationPage(new SignUpTypePage());
        //            }
        //        }
        //    });
        //}

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
                    if (TrukmanContext.User == null)
                        this.MainPage = new SignUpNavigationPage();
                    else if (TrukmanContext.User.Role == UserRole.UserRoleOwner)
                        this.MainPage = new OwnerMainPage();
                    else if (TrukmanContext.User.Role == UserRole.UserRoleDriver)
                    {
                        var driverState = (DriverState)TrukmanContext.User.Status;
                        if (driverState == DriverState.Joined)
                            this.MainPage = new MainPage();
                        else
                            this.MainPage = new SignUpNavigationPage(driverState, TrukmanContext.Company as Company);
                    }
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
