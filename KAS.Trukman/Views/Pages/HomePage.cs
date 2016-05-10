using KAS.Trucman.Converters;
using KAS.Trukman.Controls;
using KAS.Trukman.Converters;
using KAS.Trukman.Helpers;
using KAS.Trukman.Languages;
using KAS.Trukman.ViewModels.Pages;
using KAS.Trukman.Views.Commands;
using KAS.Trukman.Views.Lists;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace KAS.Trukman.Views.Pages
{
    #region HomePage
    public class HomePage : TrukmanPage
    {
        private Map _map = null;
        private Map _contractorMap = null;
//        private Map _arrivedOnTimeMap = null;
//        private Map _arrivedLateMap = null;

        private HomeViewModel _viewModel = null;

        private HomeStateToBoolConverter _homeStateToBoolConverter = new HomeStateToBoolConverter();
        private HomeStateToImageConverter _homeStateToImageConverter = new HomeStateToImageConverter();

        public HomePage() 
            : base()
        {
            _viewModel = new HomeViewModel();
            _viewModel.PropertyChanged += (sender, args) => {
                if (args.PropertyName == "AddressPosition")
                    this.MapLocateAddress(_map, this.ViewModel.AddressPosition, "Origin");
                else if (args.PropertyName == "ContractorPosition")
                    this.MapLocateAddress(_contractorMap, this.ViewModel.ContractorPosition, (this.ViewModel.SelectedContractor == ContractorItems.Origin ?  "Origin" : "Destination"));
                else if (args.PropertyName == "ArrivedPosition")
                {
//                    if (this.ViewModel.State == HomeStates.ArrivedAtPickupOnTime)
//						this.MapLocateAddress(_arrivedOnTimeMap, this.ViewModel.ArrivedPosition, "Arrive");
//                    else if (this.ViewModel.State == HomeStates.ArrivedAtPickupLate)
//						this.MapLocateAddress(_arrivedLateMap, this.ViewModel.ArrivedPosition, "Arrive");
//					else if (this.ViewModel.State == HomeStates.ArrivedAtDestinationOnTime)
//					{
//						this.MapLocateAddress(_arrivedOnTimeMap
//					}
                }
            };

            this.BindingContext = _viewModel;
        }

        protected override void OnAppearing ()
		{
			base.OnAppearing ();
		
			this.SubscribeMessages ();
        }


        protected override void OnDisappearing ()
		{
			this.UnsubscribeMessages();

			base.OnDisappearing();
		}

		private void SubscribeMessages()
		{
			ChangeLocationMessage.Subscribe(this, this.ChangeLocation);
		}

		private void UnsubscribeMessages()
		{   
			ChangeLocationMessage.Unsubscribe (this);			
		}

		private void ChangeLocation(ChangeLocationMessage message)
		{
			this.ViewModel.CurrentPosition = message.Position;			
		}

        protected override bool OnBackButtonPressed()
        {
            var result = base.OnBackButtonPressed();
            if (this.ViewModel != null)
                result = this.ViewModel.HandleBackButton();
            return result;
        }

		private void MapLocateAddress(Map map, Position position, string label)
        {
			Device.BeginInvokeOnMainThread (() => {
				if ((map != null) && (this.ViewModel != null)) {
					map.Pins.Clear ();
					//if (position.Longitude != 0 || position.Latitude != 0)
					map.Pins.Add (new Pin{ Type = PinType.Place, Position = position, Label = label });
                    map.MoveToRegion(new MapSpan(position, 0.5, 0.5));
				}
			});
        }

        protected override View CreateContent()
        {
            var gpsStatesToImageConverter = new GPSStatesToImageConverter();

            var titleBar = new TitleBar
            {
            };
            titleBar.SetBinding(TitleBar.TitleProperty, "Title", BindingMode.OneWay);
            titleBar.SetBinding(TitleBar.LeftIconProperty, new Binding("State", BindingMode.OneWay, _homeStateToImageConverter));
            titleBar.SetBinding(TitleBar.LeftCommandProperty, "ShowMainMenuCommand", BindingMode.OneWay);
            titleBar.SetBinding(TitleBar.RightIconProperty, new Binding("GPSState", BindingMode.OneWay, gpsStatesToImageConverter));
            titleBar.SetBinding(TitleBar.RightCommandProperty, "ShowGPSPreferencesCommand", BindingMode.OneWay);

            var pageCommands = new PageCommandsView {
            };

            var content = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(3, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) }
                }
            };
            content.Children.Add(titleBar, 0, 0);
            content.Children.Add(this.CreateWaitingForTripView(), 0, 1);
            content.Children.Add(this.CreateTripProposedView(), 0, 1);
            content.Children.Add(this.CreateTripDeclinedView(), 0, 1);
            content.Children.Add(this.CreateTripCanceledView(), 0, 1);
            content.Children.Add(this.CreateTripAcceptedView(), 0, 1);
            content.Children.Add(this.CreateArrivedAsPickupOnTimeView(), 0, 1);
            content.Children.Add(this.CreateArrivedAsPickupLateView(), 0, 1);
			content.Children.Add(this.CreateArrivedAtDeliveryOnTimeView(), 0, 1);
			content.Children.Add(this.CreateArrivedAtDeliveryLateView(), 0, 1);
			content.Children.Add(this.CreateTripCompletedView(), 0, 1);
            content.Children.Add(pageCommands, 0, 2);

            var busyIndicator = new ActivityIndicator
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };
            busyIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy", BindingMode.TwoWay);

            var popupBackground = new ContentView {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                BackgroundColor = Color.FromRgba(0, 0, 0, 120)
            };
            popupBackground.SetBinding(ContentView.IsVisibleProperty, "GPSPopupVisible", BindingMode.OneWay);

            var pageContent = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0
            };
            pageContent.Children.Add(content);
            pageContent.Children.Add(busyIndicator);
            pageContent.Children.Add(popupBackground);
            pageContent.Children.Add(this.CreateGPSPopup());

            return pageContent;
        }

        private View CreateWaitingForTripView()
        {
            var mapImage = new Image
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End,
                HeightRequest = 180,
                WidthRequest = 180,
                Source = PlatformHelper.HomeMapImageSource
            };

            var mapContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Content = mapImage
            };

            var waitingTripLabel = new Label {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = PlatformHelper.HomeTextColor,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };
            waitingTripLabel.SetBinding(Label.TextProperty, new Binding("HomeWaitingForTripLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var waitingTripContent = new ContentView {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 0, 20, 0),
                Content = waitingTripLabel
            };

            var grid = new Grid {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                ColumnSpacing = 0,
                RowSpacing = 20,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                }
            };
            grid.Children.Add(mapContent, 0, 0);
            grid.Children.Add(waitingTripContent, 0, 1);

            var content = new ContentView {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(0),
                Content = grid
            };
            content.SetBinding(ContentView.IsVisibleProperty, new Binding("State", BindingMode.OneWay, _homeStateToBoolConverter, HomeStates.WaitingForTrip));

            return content;
        }

        private View CreateTripProposedView()
        {
            var nextTripLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = PlatformHelper.HomeTextColor,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };
            nextTripLabel.SetBinding(Label.TextProperty, new Binding("HomeNextTripLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var nextTripContent = new ContentView {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 20, 20, 10),
                Content = nextTripLabel
            };

            var declineButton = new AppButton
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill,
                AppStyle = AppButtonStyle.Left
            };
            declineButton.SetBinding(AppButton.TextProperty, new Binding("HomeDeclineButtonText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            declineButton.SetBinding(AppButton.CommandProperty, "DeclineCommand");

            var acceptButton = new AppButton
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill,
                AppStyle = AppButtonStyle.Right
            };
            acceptButton.SetBinding(AppButton.TextProperty, new Binding("HomeAcceptButtonText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            acceptButton.SetBinding(AppButton.CommandProperty, "AcceptCommand");

            var buttons = new Grid
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 1,
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                }
            };
            buttons.Children.Add(declineButton, 0, 0);
            buttons.Children.Add(acceptButton, 1, 0);

            var buttonsContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 10, 20, 10),
                Content = buttons
            };

            var originLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = PlatformHelper.HomeTextColor,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };
            originLabel.SetBinding(Label.TextProperty, new Binding("HomeOriginLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var originContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 10, 10, 10),
                Content = originLabel
            };

            var destinationLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = PlatformHelper.HomeTextColor,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };
            destinationLabel.SetBinding(Label.TextProperty, new Binding("HomeDestinationLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var destinationContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(10, 10, 20, 10),
                Content = destinationLabel
            };

            var addressesTitle = new Grid {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0,
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                }
            };
            addressesTitle.Children.Add(originContent, 0, 0);
            addressesTitle.Children.Add(destinationContent, 1, 0);

            var originAddressLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = PlatformHelper.HomeTextColor,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
            };
            originAddressLabel.SetBinding(Label.TextProperty, "TripOrigin", BindingMode.OneWay);

            var originAddressContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 0, 10, 5),
                Content = originAddressLabel
            };

            var destinationAddressLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = PlatformHelper.HomeTextColor,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
            };
            destinationAddressLabel.SetBinding(Label.TextProperty, "TripDestination", BindingMode.OneWay);

            var destinationAddressContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(10, 0, 20, 5),
                Content = destinationAddressLabel
            };

            var tripTimeLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = PlatformHelper.HomeTextColor,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
            };
            tripTimeLabel.SetBinding(Label.TextProperty, "TripTime", BindingMode.OneWay);

            var tripTimeContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(10, 0, 20, 5),
                Content = tripTimeLabel
            };

            var addresses = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0,
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                },
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) }
                }
            };
            addresses.Children.Add(originAddressContent, 0, 0);
            addresses.Children.Add(destinationAddressContent, 1, 0);
            addresses.Children.Add(tripTimeContent, 0, 1);

            var pointsLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = PlatformHelper.HomeTextColor,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };
            pointsLabel.SetBinding(Label.TextProperty, "TripPoints", BindingMode.OneWay);

            var pointsContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 5, 20, 10),
                Content = pointsLabel
            };

            _map = new Map
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill
            };
            this.MapLocateAddress(_map, this.ViewModel.AddressPosition, "Origin");

            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0, 
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                }
            };
            grid.Children.Add(nextTripContent, 0, 0);
            grid.Children.Add(buttonsContent, 0, 1);
            grid.Children.Add(addressesTitle, 0, 2);
            grid.Children.Add(addresses, 0, 3);
            grid.Children.Add(pointsContent, 0, 4);
            grid.Children.Add(_map, 0, 5);

            var content = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(0),
                Content = grid
            };
            content.SetBinding(ContentView.IsVisibleProperty, new Binding("State", BindingMode.OneWay, _homeStateToBoolConverter, HomeStates.TripPropesed));

            return content;
        }

        private View CreateTripDeclinedView()
        {
            var nextTripLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = PlatformHelper.HomeTextColor,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };
            nextTripLabel.SetBinding(Label.TextProperty, new Binding("HomeNextTripLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var nextTripContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 20, 20, 5),
                Content = nextTripLabel
            };

            var tripTime = new Label
            {
                HorizontalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = PlatformHelper.HomeTextColor,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
            };
            tripTime.SetBinding(Label.TextProperty, "TripTime", BindingMode.OneWay);

            var tripTimeContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 0, 20, 10),
                Content = tripTime
            };

            var declined = new AppLabel
            {
                HorizontalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = PlatformHelper.HomeTextColor,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
            };
            declined.SetBinding(Label.TextProperty, new Binding("HomeDeclinedLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var declinedContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 0, 20, 10),
                Content = declined
            };

            var declinedReasonItemsToColorConverter = new DeclinedReasonItemsToColorConverter();

            var firstReasonItemText = new TappedLabel
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                TapCommandParameter = 0
            };
            firstReasonItemText.SetBinding(TappedLabel.TextProperty, new Binding("HomeDeclinedReason_1", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            firstReasonItemText.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedDeclinedReason", BindingMode.OneWay, declinedReasonItemsToColorConverter, 0));
            firstReasonItemText.SetBinding(TappedLabel.TapCommandProperty, "SelectDeclinedReasonCommand", BindingMode.OneWay);

            var secondReasonItemText = new TappedLabel
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                TapCommandParameter = 1
            };
            secondReasonItemText.SetBinding(TappedLabel.TextProperty, new Binding("HomeDeclinedReason_2", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            secondReasonItemText.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedDeclinedReason", BindingMode.OneWay, declinedReasonItemsToColorConverter, 1));
            secondReasonItemText.SetBinding(TappedLabel.TapCommandProperty, "SelectDeclinedReasonCommand", BindingMode.OneWay);

            var otherReasonItemText = new TappedLabel
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                TapCommandParameter = 2
            };
            otherReasonItemText.SetBinding(TappedLabel.TextProperty, new Binding("HomeDeclinedOtherReason", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            otherReasonItemText.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedDeclinedReason", BindingMode.OneWay, declinedReasonItemsToColorConverter, 2));
            otherReasonItemText.SetBinding(TappedLabel.TapCommandProperty, "SelectDeclinedReasonCommand", BindingMode.OneWay);

            var itemsLayout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 10, 20, 10),
                Spacing = 5
            };
            itemsLayout.Children.Add(firstReasonItemText);
            itemsLayout.Children.Add(secondReasonItemText);
            itemsLayout.Children.Add(otherReasonItemText);

            var memoEntry = new AppEntry
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Entry)),
                PlaceholderColor = PlatformHelper.EntryPlaceholderColor
            };
            memoEntry.SetBinding(AppEntry.TextProperty, "OtherReasonText", BindingMode.TwoWay);
            memoEntry.SetBinding(AppEntry.PlaceholderProperty, new Binding("HomeDeclinedOtherReasonPlaceholder", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var memoContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 20, 20, 10),
                Content = memoEntry
            };

            var submitButton = new AppButton
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center
            };
            submitButton.SetBinding(AppButton.TextProperty, new Binding("HomeSubmitButtonText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            submitButton.SetBinding(AppButton.CommandProperty, "DeclinedSubmitCommand");

            var submitContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 10, 20, 10),
                Content = submitButton
            };

            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                }
            };
            grid.Children.Add(nextTripContent, 0, 0);
            grid.Children.Add(tripTimeContent, 0, 1);
            grid.Children.Add(declinedContent, 0, 2);
            grid.Children.Add(itemsLayout, 0, 3);
            grid.Children.Add(memoContent, 0, 4);
            grid.Children.Add(submitContent, 0, 5);

            var content = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(0),
                Content = grid
            };
            content.SetBinding(ContentView.IsVisibleProperty, new Binding("State", BindingMode.OneWay, _homeStateToBoolConverter, HomeStates.TripDeclined));

            return content;
        }

        private View CreateTripCanceledView()
        {
            var mapImage = new Image
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End,
                HeightRequest = 180,
                WidthRequest = 180,
                Source = PlatformHelper.HomeMapImageSource
            };

            var mapContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(0, 0, 0, 10),
                Content = mapImage
            };

            var cancelledTripLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = PlatformHelper.HomeTextColor,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };
            cancelledTripLabel.SetBinding(Label.TextProperty, new Binding("HomeCancelledTripLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var cancelledTripContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 10, 20, 0),
                Content = cancelledTripLabel
            };

            var continueButton = new AppButton
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center
            };
            continueButton.SetBinding(AppButton.TextProperty, new Binding("HomeContinueButtonText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            continueButton.SetBinding(AppButton.CommandProperty, "ContinueCommand");

            var continueContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 10, 20, 10),
                Content = continueButton
            };

            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                ColumnSpacing = 0,
                RowSpacing = 0,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(4, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(2, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                }
            };
            grid.Children.Add(mapContent, 0, 0);
            grid.Children.Add(cancelledTripContent, 0, 1);
            grid.Children.Add(continueContent, 0, 2);

            var content = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(0),
                Content = grid
            };
            content.SetBinding(ContentView.IsVisibleProperty, new Binding("State", BindingMode.OneWay, _homeStateToBoolConverter, HomeStates.TripCanceled));

            return content;
        }

        private View CreateTripAcceptedView()
        {
            var nextTripLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = PlatformHelper.HomeTextColor,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };
            nextTripLabel.SetBinding(Label.TextProperty, new Binding("HomeNextTripLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var contractorItemsToColorConverter = new HomeContractorItemsToColorConverter();

            var nextTripContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 20, 20, 10),
                Content = nextTripLabel
            };

            var originLabel = new TappedLabel
            {
                HorizontalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                TapCommandParameter = 0
            };
            originLabel.SetBinding(TappedLabel.TextProperty, new Binding("HomeOriginLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            originLabel.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedContractor", BindingMode.OneWay, contractorItemsToColorConverter, 0));
            originLabel.SetBinding(TappedLabel.TapCommandProperty, "SelectContractorCommand");

            var originContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 10, 10, 10),
                Content = originLabel
            };

            var destinationLabel = new TappedLabel
            {
                HorizontalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                TapCommandParameter = 1
            };
            destinationLabel.SetBinding(TappedLabel.TextProperty, new Binding("HomeDestinationLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            destinationLabel.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedContractor", BindingMode.OneWay, contractorItemsToColorConverter, 1));
            destinationLabel.SetBinding(TappedLabel.TapCommandProperty, "SelectContractorCommand");

            var destinationContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(10, 10, 20, 10),
                Content = destinationLabel
            };

            var originAddressLabel = new TappedLabel
            {
                HorizontalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TapCommandParameter = 0
            };
            originAddressLabel.SetBinding(TappedLabel.TextProperty, "TripOrigin", BindingMode.OneWay);
            originAddressLabel.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedContractor", BindingMode.OneWay, contractorItemsToColorConverter, 0));
            originAddressLabel.SetBinding(TappedLabel.TapCommandProperty, "SelectContractorCommand");

            var originAddressContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 0, 10, 5),
                Content = originAddressLabel
            };

            var destinationAddressLabel = new TappedLabel
            {
                HorizontalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TapCommandParameter = 1
            };
            destinationAddressLabel.SetBinding(TappedLabel.TextProperty, "TripDestination", BindingMode.OneWay);
            destinationAddressLabel.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedContractor", BindingMode.OneWay, contractorItemsToColorConverter, 1));
            destinationAddressLabel.SetBinding(TappedLabel.TapCommandProperty, "SelectContractorCommand");

            var destinationAddressContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(10, 0, 20, 5),
                Content = destinationAddressLabel
            };

            var tripTimeLabel = new TappedLabel
            {
                HorizontalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TapCommandParameter = 0
            };
            tripTimeLabel.SetBinding(TappedLabel.TextProperty, "TripTime", BindingMode.OneWay);
            tripTimeLabel.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedContractor", BindingMode.OneWay, contractorItemsToColorConverter, 0));
            tripTimeLabel.SetBinding(TappedLabel.TapCommandProperty, "SelectContractorCommand");

            var tripTimeContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(10, 0, 20, 5),
                Content = tripTimeLabel
            };

            var homeTripTimeToColorConverter = new HomeTripTimeToColorConverter();

            var timerLabel = new TappedLabel
            {
                HorizontalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.Start,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };
            timerLabel.SetBinding(TappedLabel.TextProperty, "CurrentTime", BindingMode.OneWay);
            timerLabel.SetBinding(TappedLabel.TextColorProperty, new Binding("IsTimeOver", BindingMode.OneWay, homeTripTimeToColorConverter));
            //timerLabel.SetBinding(TappedLabel.TapCommandProperty, "ArrivedCommand");

            var timerContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 0, 10, 5),
                Content = timerLabel
            };

            var pointsLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.End,
                TextColor = PlatformHelper.HomeTextColor,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };
            pointsLabel.SetBinding(Label.TextProperty, "TripPoints", BindingMode.OneWay);

            var pointsContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 0, 10, 5),
                Content = pointsLabel
            };

            var addresses = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0,
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                },
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) }
                }
            };
            addresses.Children.Add(originContent, 0, 0);
            addresses.Children.Add(destinationContent, 1, 0);
            addresses.Children.Add(originAddressContent, 0, 1);
            addresses.Children.Add(destinationAddressContent, 1, 1);
            addresses.Children.Add(tripTimeContent, 0, 2);
            addresses.Children.Add(timerContent, 0, 3);
            addresses.Children.Add(pointsContent, 1, 3);

            _contractorMap = new Map
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill
            };
            this.MapLocateAddress(_contractorMap, this.ViewModel.ContractorPosition, (this.ViewModel.SelectedContractor == ContractorItems.Origin ? "Origin" : "Destination"));

            var grid = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                }
            };
            grid.Children.Add(nextTripContent, 0, 0);
            grid.Children.Add(addresses, 0, 1);
            grid.Children.Add(_contractorMap, 0, 2);

            var content = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(0),
                Content = grid
            };
            content.SetBinding(ContentView.IsVisibleProperty, new Binding("State", BindingMode.OneWay, _homeStateToBoolConverter, HomeStates.TripAccepted));

            return content;
        }

        private View CreateArrivedAtDeliveryOnTimeView()
        {
            var image = new Image {
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 180,
                HeightRequest = 180,
                Source = PlatformHelper.LikeImageSource
            };

            var imageContent = new ContentView {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(10, 0, 0, 0),
                Content = image
            };

            var arrivedOnTimeLabel = new Label {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TextColor = PlatformHelper.HomeTextColor
            };
            arrivedOnTimeLabel.SetBinding(Label.TextProperty, new Binding("HomeArrivedOnTimeLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var arrivedOnTimeContent = new ContentView {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start,
                Padding = new Thickness(0, 0, 10, 0),
                Content = arrivedOnTimeLabel
            };

            var arrivedBonusPointsLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                TextColor = PlatformHelper.HomeTextColor
            };
            arrivedBonusPointsLabel.SetBinding(Label.TextProperty, new Binding("HomeArrivedOnTimeBonusPointsLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var arrivedBonusPointsContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start,
                Padding = new Thickness(0, 5, 10, 0),
                Content = arrivedBonusPointsLabel
            };

            var arrivedBonusMinsPointsLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TextColor = PlatformHelper.HomeTextColor
            };
            arrivedBonusMinsPointsLabel.SetBinding(Label.TextProperty, new Binding("HomeArrivedOnTimeBonusPointsMinsLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var arrivedBonusMinsPointsContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start,
                Padding = new Thickness(0, 5, 10, 0),
                Content = arrivedBonusMinsPointsLabel
            };
            arrivedBonusMinsPointsContent.SetBinding(ContentView.IsVisibleProperty, "ArrivedBonusMinsVisible", BindingMode.OneWay);

            var pointsLayout = new StackLayout {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Spacing = 0
            };
            pointsLayout.Children.Add(arrivedOnTimeContent);
            pointsLayout.Children.Add(arrivedBonusPointsContent);
            pointsLayout.Children.Add(arrivedBonusMinsPointsContent);

            var pointsGrid = new Grid {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start,
                RowSpacing = 0,
                ColumnSpacing = 0,
                Padding = new Thickness(0, 10, 0, 0),
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                }
            };
            pointsGrid.Children.Add(imageContent, 0, 0);
            pointsGrid.Children.Add(pointsLayout, 1, 0);

            var totalPointsLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                TextColor = PlatformHelper.HomeTextColor
            };
            totalPointsLabel.SetBinding(Label.TextProperty, "TotalPointsText", BindingMode.OneWay);

            var totalPointsContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start,
                Padding = new Thickness(10, 0, 10, 5),
                Content = totalPointsLabel
            };

            var nextStepLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                TextColor = PlatformHelper.HomeTextColor
            };
            nextStepLabel.SetBinding(Label.TextProperty, new Binding("HomeNextStepLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var nextStepContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start,
                Padding = new Thickness(10, 10, 10, 10),
                Content = nextStepLabel
            };

            var arrivedCommands = new CommandsListView
            {
                RowHeight = PlatformHelper.DisplayHeight / 8
            };
            arrivedCommands.SetBinding(MainMenuListView.ItemsSourceProperty, "ArrivedDeliveryMenuItems", BindingMode.OneWay);
            arrivedCommands.SetBinding(MainMenuListView.SelectedItemProperty, "SelectedArrivedMenuItem", BindingMode.TwoWay);
            arrivedCommands.SetBinding(MainMenuListView.ItemClickCommandProperty, "MenuItemClickCommand");

            var grid = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                ColumnSpacing = 0,
                RowSpacing = 0,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                }
            };
            grid.Children.Add(pointsGrid, 0, 0);
            grid.Children.Add(totalPointsContent, 0, 1);
            grid.Children.Add(nextStepContent, 0, 2);
            grid.Children.Add(arrivedCommands, 0, 3);

            var content = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(0),
                Content = grid
            };
			content.SetBinding(ContentView.IsVisibleProperty, new Binding("State", BindingMode.OneWay, _homeStateToBoolConverter, HomeStates.ArrivedAtDestinationOnTime));

            return content;
        }

        private View CreateArrivedAtDeliveryLateView()
        {
            var image = new Image
            {
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 180,
                HeightRequest = 180,
                Source = PlatformHelper.DislikeImageSource
            };

            var imageContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(10, 0, 0, 0),
                Content = image
            };

            var arrivedLateLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TextColor = PlatformHelper.HomeTextColor
            };
            arrivedLateLabel.SetBinding(Label.TextProperty, new Binding("HomeArrivedLateLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var arrivedLateContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start,
                Padding = new Thickness(0, 0, 10, 0),
                Content = arrivedLateLabel
            };

            var arrivedBonusMinsPointsLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TextColor = PlatformHelper.HomeTextColor
            };
            arrivedBonusMinsPointsLabel.SetBinding(Label.TextProperty, new Binding("HomeArrivedLateBonusLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var arrivedBonusMinsPointsContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start,
                Padding = new Thickness(0, 20, 10, 0),
                Content = arrivedBonusMinsPointsLabel
            };

            var pointsLayout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Spacing = 0
            };
            pointsLayout.Children.Add(arrivedLateContent);
            pointsLayout.Children.Add(arrivedBonusMinsPointsContent);

            var pointsGrid = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start,
                RowSpacing = 0,
                ColumnSpacing = 0,
                Padding = new Thickness(0, 10, 0, 0),
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                }
            };
            pointsGrid.Children.Add(imageContent, 0, 0);
            pointsGrid.Children.Add(pointsLayout, 1, 0);

            var totalPointsLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                TextColor = PlatformHelper.HomeTextColor
            };
            totalPointsLabel.SetBinding(Label.TextProperty, "TotalPointsText", BindingMode.OneWay);

            var totalPointsContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start,
                Padding = new Thickness(10, 0, 10, 5),
                Content = totalPointsLabel
            };

            var nextStepLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                TextColor = PlatformHelper.HomeTextColor
            };
            nextStepLabel.SetBinding(Label.TextProperty, new Binding("HomeNextStepLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var nextStepContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start,
                Padding = new Thickness(10, 10, 10, 10),
                Content = nextStepLabel
            };

            var arrivedCommands = new CommandsListView
            {
                RowHeight = PlatformHelper.DisplayHeight / 8
            };
            arrivedCommands.SetBinding(MainMenuListView.ItemsSourceProperty, "ArrivedDeliveryMenuItems", BindingMode.OneWay);
            arrivedCommands.SetBinding(MainMenuListView.SelectedItemProperty, "SelectedArrivedMenuItem", BindingMode.TwoWay);
            arrivedCommands.SetBinding(MainMenuListView.ItemClickCommandProperty, "MenuItemClickCommand");

            var grid = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                ColumnSpacing = 0,
                RowSpacing = 0,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                }
            };
            grid.Children.Add(pointsGrid, 0, 0);
            grid.Children.Add(totalPointsContent, 0, 1);
            grid.Children.Add(nextStepContent, 0, 2);
            grid.Children.Add(arrivedCommands, 0, 3);

            var content = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(0),
                Content = grid
            };
			content.SetBinding(ContentView.IsVisibleProperty, new Binding("State", BindingMode.OneWay, _homeStateToBoolConverter, HomeStates.ArrivedAtDestinationLate));

            return content;
        }

		private View CreateArrivedAsPickupOnTimeView()
		{
			var image = new Image {
				HorizontalOptions = LayoutOptions.End,
				VerticalOptions = LayoutOptions.Center,
				WidthRequest = 180,
				HeightRequest = 180,
				Source = PlatformHelper.LikeImageSource
			};

			var imageContent = new ContentView {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				Padding = new Thickness(10, 0, 0, 0),
				Content = image
			};

			var arrivedOnTimeLabel = new Label {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Start,
				HorizontalTextAlignment = TextAlignment.Center,
				FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
				TextColor = PlatformHelper.HomeTextColor
			};
			arrivedOnTimeLabel.SetBinding(Label.TextProperty, new Binding("HomeArrivedOnTimeLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

			var arrivedOnTimeContent = new ContentView {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Start,
				Padding = new Thickness(0, 0, 10, 0),
				Content = arrivedOnTimeLabel
			};

			var arrivedBonusPointsLabel = new Label
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Start,
				HorizontalTextAlignment = TextAlignment.Center,
				FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
				FontAttributes = FontAttributes.Bold,
				TextColor = PlatformHelper.HomeTextColor
			};
			arrivedBonusPointsLabel.SetBinding(Label.TextProperty, new Binding("HomeArrivedOnTimeBonusPointsLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

			var arrivedBonusPointsContent = new ContentView
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Start,
				Padding = new Thickness(0, 5, 10, 0),
				Content = arrivedBonusPointsLabel
			};

			var arrivedBonusMinsPointsLabel = new Label
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Start,
				HorizontalTextAlignment = TextAlignment.Center,
				FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
				TextColor = PlatformHelper.HomeTextColor
			};
			arrivedBonusMinsPointsLabel.SetBinding(Label.TextProperty, new Binding("HomeArrivedOnTimeBonusPointsMinsLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

			var arrivedBonusMinsPointsContent = new ContentView
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Start,
				Padding = new Thickness(0, 5, 10, 0),
				Content = arrivedBonusMinsPointsLabel
			};
			arrivedBonusMinsPointsContent.SetBinding(ContentView.IsVisibleProperty, "ArrivedBonusMinsVisible", BindingMode.OneWay);

			var pointsLayout = new StackLayout {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Center,
				Spacing = 0
			};
			pointsLayout.Children.Add(arrivedOnTimeContent);
			pointsLayout.Children.Add(arrivedBonusPointsContent);
			pointsLayout.Children.Add(arrivedBonusMinsPointsContent);

			var pointsGrid = new Grid {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Start,
				RowSpacing = 0,
				ColumnSpacing = 0,
				Padding = new Thickness(0, 10, 0, 0),
				ColumnDefinitions = {
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
				}
			};
			pointsGrid.Children.Add(imageContent, 0, 0);
			pointsGrid.Children.Add(pointsLayout, 1, 0);

			var totalPointsLabel = new Label
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Start,
				HorizontalTextAlignment = TextAlignment.Center,
				FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
				FontAttributes = FontAttributes.Bold,
				TextColor = PlatformHelper.HomeTextColor
			};
			totalPointsLabel.SetBinding(Label.TextProperty, "TotalPointsText", BindingMode.OneWay);

			var totalPointsContent = new ContentView {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Start,
				Padding = new Thickness(10, 0, 10, 5),
				Content = totalPointsLabel
			};
            totalPointsLabel.SetBinding(Label.TextProperty, "TotalPointsText", BindingMode.OneWay);

            var nextStepLabel = new Label {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                TextColor = PlatformHelper.HomeTextColor
            };
            nextStepLabel.SetBinding(Label.TextProperty, new Binding("HomeNextStepLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var nextStepContent = new ContentView {
                HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Start,
				Padding = new Thickness(10, 10, 10, 10),
				Content = nextStepLabel
            };

            var arrivedCommands = new CommandsListView {
                RowHeight = PlatformHelper.DisplayHeight / 8
            };
            arrivedCommands.SetBinding(MainMenuListView.ItemsSourceProperty, "ArrivedPickUpMenuItems", BindingMode.OneWay);
            arrivedCommands.SetBinding(MainMenuListView.SelectedItemProperty, "SelectedArrivedMenuItem", BindingMode.TwoWay);
            arrivedCommands.SetBinding(MainMenuListView.ItemClickCommandProperty, "MenuItemClickCommand");

            var grid = new Grid
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				ColumnSpacing = 0,
				RowSpacing = 0,
				RowDefinitions = {
					new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
					new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
					new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
					new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
				}
			};
			grid.Children.Add(pointsGrid, 0, 0);
			grid.Children.Add(totalPointsContent, 0, 1);
            grid.Children.Add(nextStepContent, 0, 2);
            grid.Children.Add(arrivedCommands, 0, 3);

			var content = new ContentView
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				Padding = new Thickness(0),
				Content = grid
			};
			content.SetBinding(ContentView.IsVisibleProperty, new Binding("State", BindingMode.OneWay, _homeStateToBoolConverter, HomeStates.ArrivedAtPickupOnTime));

			return content;
		}

		private View CreateArrivedAsPickupLateView()
		{
			var image = new Image
			{
				HorizontalOptions = LayoutOptions.End,
				VerticalOptions = LayoutOptions.Center,
				WidthRequest = 180,
				HeightRequest = 180,
				Source = PlatformHelper.DislikeImageSource
			};

			var imageContent = new ContentView
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				Padding = new Thickness(10, 0, 0, 0),
				Content = image
			};

			var arrivedLateLabel = new Label
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Start,
				HorizontalTextAlignment = TextAlignment.Center,
				FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
				TextColor = PlatformHelper.HomeTextColor
			};
			arrivedLateLabel.SetBinding(Label.TextProperty, new Binding("HomeArrivedLateLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

			var arrivedLateContent = new ContentView
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Start,
				Padding = new Thickness(0, 0, 10, 0),
				Content = arrivedLateLabel
			};

			var arrivedBonusMinsPointsLabel = new Label
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Start,
				HorizontalTextAlignment = TextAlignment.Center,
				FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
				TextColor = PlatformHelper.HomeTextColor
			};
			arrivedBonusMinsPointsLabel.SetBinding(Label.TextProperty, new Binding("HomeArrivedLateBonusLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

			var arrivedBonusMinsPointsContent = new ContentView
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Start,
				Padding = new Thickness(0, 20, 10, 0),
				Content = arrivedBonusMinsPointsLabel
			};

			var pointsLayout = new StackLayout
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Center,
				Spacing = 0
			};
			pointsLayout.Children.Add(arrivedLateContent);
			pointsLayout.Children.Add(arrivedBonusMinsPointsContent);

			var pointsGrid = new Grid
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Start,
				RowSpacing = 0,
				ColumnSpacing = 0,
				Padding = new Thickness(0, 10, 0, 0),
				ColumnDefinitions = {
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
				}
			};
			pointsGrid.Children.Add(imageContent, 0, 0);
			pointsGrid.Children.Add(pointsLayout, 1, 0);

			var totalPointsLabel = new Label
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Start,
				HorizontalTextAlignment = TextAlignment.Center,
				FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
				FontAttributes = FontAttributes.Bold,
				TextColor = PlatformHelper.HomeTextColor
			};
			totalPointsLabel.SetBinding(Label.TextProperty, "TotalPointsText", BindingMode.OneWay);

			var totalPointsContent = new ContentView
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Start,
				Padding = new Thickness(10, 0, 10, 5),
				Content = totalPointsLabel
			};

            var nextStepLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                TextColor = PlatformHelper.HomeTextColor
            };
            nextStepLabel.SetBinding(Label.TextProperty, new Binding("HomeNextStepLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var nextStepContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start,
                Padding = new Thickness(10, 10, 10, 10),
                Content = nextStepLabel
            };

            var arrivedCommands = new CommandsListView
            {
                RowHeight = PlatformHelper.DisplayHeight / 8
            };
            arrivedCommands.SetBinding(MainMenuListView.ItemsSourceProperty, "ArrivedPickUpMenuItems", BindingMode.OneWay);
            arrivedCommands.SetBinding(MainMenuListView.SelectedItemProperty, "SelectedArrivedMenuItem", BindingMode.TwoWay);
            arrivedCommands.SetBinding(MainMenuListView.ItemClickCommandProperty, "MenuItemClickCommand");

            var grid = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                ColumnSpacing = 0,
                RowSpacing = 0,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                }
            };
            grid.Children.Add(pointsGrid, 0, 0);
            grid.Children.Add(totalPointsContent, 0, 1);
            grid.Children.Add(nextStepContent, 0, 2);
            grid.Children.Add(arrivedCommands, 0, 3);

            var content = new ContentView
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				Padding = new Thickness(0),
				Content = grid
			};
			content.SetBinding(ContentView.IsVisibleProperty, new Binding("State", BindingMode.OneWay, _homeStateToBoolConverter, HomeStates.ArrivedAtPickupLate));

			return content;
		}

		private View CreateTripCompletedView()
		{
			var mapImage = new Image
			{
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.End,
				HeightRequest = 180,
				WidthRequest = 180,
				Source = PlatformHelper.HomeMapImageSource
			};

			var mapContent = new ContentView
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				Content = mapImage
			};

			var completedTripLabel = new Label {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Start,
				HorizontalTextAlignment = TextAlignment.Center,
				TextColor = PlatformHelper.HomeTextColor,
				FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
			};
			completedTripLabel.SetBinding(Label.TextProperty, new Binding("HomeCongratulations", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

			var completedTripContent = new ContentView {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				Padding = new Thickness(20, 10, 20, 0),
				Content = completedTripLabel
			};

			var totalPointsLabel = new Label
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Start,
				HorizontalTextAlignment = TextAlignment.Center,
				FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
				FontAttributes = FontAttributes.Bold,
				TextColor = PlatformHelper.HomeTextColor
			};
			totalPointsLabel.SetBinding(Label.TextProperty, "TotalPointsText", BindingMode.OneWay);

			var totalPointsContent = new ContentView
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Start,
				Padding = new Thickness(10, 0, 10, 5),
				Content = totalPointsLabel
			};

			var rewardsButton = new AppButton
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Center
			};
			rewardsButton.SetBinding(AppButton.TextProperty, new Binding("HomeRewardsButtonText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
			rewardsButton.SetBinding(AppButton.CommandProperty, "RewardsCommand");

			var rewardsContent = new ContentView
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				Padding = new Thickness(20, 10, 20, 10),
				Content = rewardsButton
			};

			var newTripButton = new AppButton {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Center
			};
			newTripButton.SetBinding(AppButton.TextProperty, new Binding("HomeNewTripButtonText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
			newTripButton.SetBinding(AppButton.CommandProperty, "NewTripCommand");

			var newTripContent = new ContentView
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				Padding = new Thickness(20, 10, 20, 10),
				Content = newTripButton
			};

			var grid = new Grid
			{
				VerticalOptions = LayoutOptions.Fill,
				HorizontalOptions = LayoutOptions.Fill,
				ColumnSpacing = 0,
				RowSpacing = 0,
				RowDefinitions = {
					new RowDefinition { Height = new GridLength(4, GridUnitType.Star) },
					new RowDefinition { Height = new GridLength(2, GridUnitType.Star) },
					new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
					new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
					new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) }
				}
			};
			grid.Children.Add(mapContent, 0, 0);
			grid.Children.Add(completedTripContent, 0, 1);
			grid.Children.Add(totalPointsContent, 0, 2);
			grid.Children.Add(rewardsContent, 0, 3);
			grid.Children.Add (newTripContent, 0, 4);

			var content = new ContentView 
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				Padding = new Thickness(0),
				Content = grid
			};
			content.SetBinding(ContentView.IsVisibleProperty, new Binding("State", BindingMode.OneWay, _homeStateToBoolConverter, HomeStates.TripComleted));

			return content;
		}

        private View CreateGPSPopup()
        {
            var appBoxView = new AppBoxView {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill
            };

            var mainLabel = new Label {
                HorizontalOptions = LayoutOptions.Fill,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.Black
            };
            mainLabel.SetBinding(Label.TextProperty, new Binding("HomeGPSPopupMainLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var mainContent = new ContentView {
                HorizontalOptions = LayoutOptions.Fill,
                Padding= new Thickness(10, 20, 10, 20),
                Content = mainLabel
            };

            var smallerLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Fill,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.Black
            };
            smallerLabel.SetBinding(Label.TextProperty, new Binding("HomeGPSPopupSmallerLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var smallerContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(10, 20, 10, 40),
                Content = smallerLabel
            };

            var cancelButton = new AppPopupButton
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill,
                AppStyle = AppButtonStyle.Left
            };
            cancelButton.SetBinding(AppPopupButton.TextProperty, new Binding("HomeGPSPopupCancelButtonText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            cancelButton.SetBinding(AppButton.CommandProperty, "GPSPopupCancelCommand");

            var settingsButton = new AppPopupButton
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill,
                AppStyle = AppButtonStyle.Right
            };
            settingsButton.SetBinding(AppPopupButton.TextProperty, new Binding("HomeGPSPopupSettingsButtonText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            settingsButton.SetBinding(AppPopupButton.CommandProperty, "GPSPopupSettingsCommand");

            var buttons = new Grid
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0,
                Padding = new Thickness(0, 1, 0, 0),
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                }
            };
            buttons.Children.Add(cancelButton, 0, 0);
            buttons.Children.Add(settingsButton, 1, 0);

            var popupContent = new StackLayout {
                Spacing = 0,
                HorizontalOptions = LayoutOptions.Fill
            };
            popupContent.Children.Add(mainContent);
            popupContent.Children.Add(smallerContent);
            popupContent.Children.Add(buttons);

            var content = new Grid {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                RowSpacing = 0,
                ColumnSpacing = 0,
                Padding = new Thickness(40, 0, 40, 0)
            };
            content.SetBinding(Grid.IsVisibleProperty, "GPSPopupVisible", BindingMode.TwoWay);

            content.Children.Add(appBoxView);
            content.Children.Add(popupContent);

            return content;
        }

        public new HomeViewModel ViewModel
        {
            get { return _viewModel; }
        }
    }
    #endregion
}
