﻿using KAS.Trukman.Messages;
using KAS.Trukman.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Trukman.Messages;

namespace KAS.Trukman.Views.Pages
{
    #region MainPage
    public class MainPage : MasterDetailPage
    {
        private NavigationPage _navigationPage = null;

        public MainPage() 
            : base()
        {
            this.BindingContext = new MainViewModel();

            var mainMenuPage = new MainMenuPage();
            mainMenuPage.BindingContext = this.ViewModel.MainMenuViewModel;

            var homePage = new HomePage();

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
            this.UnsubscribeMessages();

            base.OnDisappearing();
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

        private void SubscribeMessages()
        {
            ShowMainMenuMessage.Subscribe(this, this.ShowMainMenu);
            HideMainMenuMessage.Subscribe(this, this.HideMainMenu);
            PopPageMessage.Subscribe(this, this.PopPage);
            PopToRootPageMessage.Subscribe(this, this.PopToRootPage);
            ShowTripPageMessage.Subscribe(this, this.ShowTripPage);
            ShowShipperInfoPageMessage.Subscribe(this, this.ShowShipperInfoPage);
            ShowReceiverInfoPageMessage.Subscribe(this, this.ShowReceiverInfoPage);
            ShowAdvancesPageMessage.Subscribe(this, this.ShowAdvancesPage);
            ShowFuelAdvancePageMessage.Subscribe(this, this.ShowFuelAdvancePage);
            ShowLumperPageMessage.Subscribe(this, this.ShowLumperPage);
            ShowDelayEmergencyPageMessage.Subscribe(this, this.ShowDelayEmergencyPage);
			//StartLocationServiceMessage.Subscribe (this, this.StartLocationService);
			//StopLocationServiceMessage.Subscribe (this, this.StopLocationService);
            ShowRoutePageMessage.Subscribe(this, this.ShowRoutePage);
            ShowPointsAndRewardsPageMessage.Subscribe(this, this.ShowPointsAndRewardsPage);
            ShowSettingsPageMessage.Subscribe(this, this.ShowSettingsPage);
            ShowHelpPageMessage.Subscribe(this, this.ShowHelpPage);
        }

        private void UnsubscribeMessages()
        {
            ShowMainMenuMessage.Unsubscribe(this);
            HideMainMenuMessage.Unsubscribe(this);
            PopPageMessage.Unsubscribe(this);
            PopToRootPageMessage.Unsubscribe(this);
            ShowTripPageMessage.Unsubscribe(this);
            ShowShipperInfoPageMessage.Unsubscribe(this);
            ShowReceiverInfoPageMessage.Unsubscribe(this);
            ShowAdvancesPageMessage.Unsubscribe(this);
            ShowFuelAdvancePageMessage.Unsubscribe(this);
            ShowLumperPageMessage.Unsubscribe(this);
            ShowDelayEmergencyPageMessage.Unsubscribe(this);
			StartLocationServiceMessage.Unsubscribe (this);
			StopLocationServiceMessage.Unsubscribe (this);
            ShowRoutePageMessage.Unsubscribe(this);
            ShowPointsAndRewardsPageMessage.Unsubscribe(this);
            ShowSettingsPageMessage.Unsubscribe(this);
            ShowHelpPageMessage.Unsubscribe(this);
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

        private void ShowTripPage(ShowTripPageMessage message)
        {
            this.RemovePage<TripPage>();
            var tripPage = new TripPage();
            tripPage.ViewModel.Initialize(message.Trip);
            this.PushPage(tripPage);
        }

        private void ShowShipperInfoPage(ShowShipperInfoPageMessage message)
        {
            this.RemovePage<ShipperInfoPage>();
            var shipperInfoPage = new ShipperInfoPage();
            shipperInfoPage.ViewModel.Initialize(message.Shipper);
            this.PushPage(shipperInfoPage);
        }

        private void ShowReceiverInfoPage(ShowReceiverInfoPageMessage message)
        {
            this.RemovePage<ReceiverInfoPage>();
            var receiverInfoPage = new ReceiverInfoPage();
            receiverInfoPage.ViewModel.Initialize(message.Receiver);
            this.PushPage(receiverInfoPage);
        }

        private void ShowRoutePage(ShowRoutePageMessage message)
        {
            this.RemovePage<RoutePage>();
            var routePage = new RoutePage();
            routePage.ViewModel.Initialize(message.Trip);
            this.PushPage(routePage);
        }

        private void ShowAdvancesPage(ShowAdvancesPageMessage message)
        {
            this.RemovePage<AdvancesPage>();
            var advancesPage = new AdvancesPage();
			advancesPage.ViewModel.Initialize (message.Trip);
            this.PushPage(advancesPage);
        }

        private void ShowFuelAdvancePage(ShowFuelAdvancePageMessage message)
        {
            this.RemovePage<FuelAdvancePage>();
            var fuelAdvancePage = new FuelAdvancePage();
            fuelAdvancePage.ViewModel.Initialize();
            this.PushPage(fuelAdvancePage);
        }

        private void ShowLumperPage(ShowLumperPageMessage message)
        {
            this.RemovePage<LumperPage>();
            var lumperPage = new LumperPage();
            lumperPage.ViewModel.Initialize();
            this.PushPage(lumperPage);
        }

        private void ShowDelayEmergencyPage(ShowDelayEmergencyPageMessage message)
        {
            this.RemovePage<DelayEmergencyPage>();
            var delayEmergencyPage = new DelayEmergencyPage();
			delayEmergencyPage.ViewModel.Initialize(message.Trip);
            this.PushPage(delayEmergencyPage);
        }

        private void ShowPointsAndRewardsPage(ShowPointsAndRewardsPageMessage message)
        {
            this.RemovePage<PointsAndRewardsPage>();
            var page = new PointsAndRewardsPage();
            page.ViewModel.Initialize();
            this.PushPage(page);
        }

        private void ShowSettingsPage(ShowSettingsPageMessage message)
        {
            this.RemovePage<SettingsPage>();
            var page = new SettingsPage();
            page.ViewModel.Initialize();
            this.PushPage(page);
        }

        private void ShowHelpPage(ShowHelpPageMessage message)
        {
            this.RemovePage<HelpPage>();
            var page = new HelpPage();
            page.ViewModel.Initialize();
            this.PushPage(page);
        }

        private void RemovePage<T>()
        {
            Page existPage = null;
            foreach (var page in _navigationPage.Navigation.NavigationStack)
                if (page is T)
                {
                    existPage = page;
                    break;
                }
            if (existPage != null)
                _navigationPage.Navigation.RemovePage(existPage);
        }

        public MainViewModel ViewModel
        {
            get { return (this.BindingContext as MainViewModel); }
        }
    }
    #endregion
}
