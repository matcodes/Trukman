using KAS.Trukman.Classes;
using KAS.Trukman.Data.Maps;
using KAS.Trukman.Data.Route;
using KAS.Trukman.AppContext;
using KAS.Trukman.Helpers;
using KAS.Trukman.Languages;
using KAS.Trukman.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms.Maps;
using Xamarin.Forms;
using KAS.Trukman.Data.Classes;
using KAS.Trukman.Extensions;

namespace KAS.Trukman.ViewModels.Pages.Owner
{
    #region OwnerFleetViewModel
    public class OwnerFleetViewModel : PageViewModel
    {
        private Timer _currentPositionTimer = null;

        private Position _lastCarPosition = new Position(0, 0);

        public OwnerFleetViewModel() : base()
        {
            this.Trips = new ObservableCollection<Trip>();

            this.ShowMainMenuCommand = new VisualCommand(this.ShowMainMenu);
            this.ShowHomePageCommand = new VisualCommand(this.ShowHomePage);

            this.SelectTripCommand = new VisualCommand(this.SelectTrip);

            this.RefreshCommand = new VisualCommand(this.Refresh);
        }

        public override void Initialize(params object[] parameters)
        {
            base.Initialize(parameters);

            this.SelectActiveTrips();
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

            this.Title = AppLanguages.CurrentLanguage.OwnerFleetPageName;
        }

        protected override void DoPropertyChanged(string propertyName)
        {
            if ((propertyName == "SelectedTrip") && (this.SelectedTrip != null))
                this.CreateBaseRoute();

            base.DoPropertyChanged(propertyName);
        }

        private void SelectActiveTrips()
        {
            Task.Run(async () =>
            {
                this.IsBusy = true;
                try
                {
                    var trips = await TrukmanContext.SelectActiveTrips();
                    this.ShowTrips(trips);
                }
                catch (Exception exception)
                {
                    ShowToastMessage.Send(exception.Message);
                }
                finally
                {
                    this.IsBusy = false;
                    this.IsRefreshing = false;
                }
            }).LogExceptions("OwnerFleetViewModel SelectActiveTrips");
        }

        private void ShowTrips(Trip[] trips)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                this.Trips.Clear();
                this.SelectedTrip = null;

                if (trips != null)
                    foreach (var trip in trips)
                    {
                        this.Trips.Add(trip);
                        if (this.SelectedTrip == null)
                            this.SelectedTrip = trip;
                    }
            });
        }

        private void CreateBaseRoute()
        {
            Task.Run(async () =>
            {
                this.IsBusy = true;
                try
                {
                    var routeResult = await RouteHelper.FindRouteForTrip(this.SelectedTrip);
                    var route = ((routeResult != null) && (routeResult.Routes.Length > 0) ? routeResult.Routes[0] : null);
                    var leg = ((route != null) && (route.Legs.Length > 0) ? route.Legs[0] : null);
                    if (route != null)
                    {
                        this.RouteRegion = route.Bounds;

                        if (leg != null)
                        {
                            this.StartPosition = new AddressInfo
                            {
                                Address = leg.StartAddress,
                                Position = new Position(leg.StartLocation.Latitude, leg.StartLocation.Longitude),
                                Contractor = this.SelectedTrip.Shipper
                            };
                            this.EndPosition = new AddressInfo
                            {
                                Address = leg.EndAddress,
                                Position = new Position(leg.EndLocation.Latitude, leg.EndLocation.Longitude),
                                Contractor = this.SelectedTrip.Receiver
                            };
                        }
                        else
                        {
                            this.StartPosition = null;
                            this.EndPosition = null;
                        }

                        var routePoints = this.DecodeRoutePoints(route.OverviewPolyline.Points);

                        this.BaseRoutePoints = routePoints;
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    ShowToastMessage.Send(exception.Message);
                    this.RecreateRoute();
                }
                finally
                {
                    this.IsBusy = false;
                    this.SetCurrentPosition();
                }
            }).LogExceptions("OwnerFleetViewModel CreateBaseRoute");
        }

        private void RecreateRoute()
        {
            var timer = new System.Timers.Timer { Interval = 200 };
            timer.Elapsed += (sender, args) =>
            {
                timer.Stop();
                this.CreateBaseRoute();
            };
            timer.Start();
        }

        private void SetCurrentPosition()
        {
            Task.Run(async () =>
            {
                this.StopCurrentPositionTimer();
                try
                {
                    if (this.SelectedTrip != null)
                    {
                        var trip = await TrukmanContext.SelectTripByID(this.SelectedTrip.ID);
                        if (trip != null)
                        {
                            var position = trip.Location;

                            if ((position.Latitude == 0) && (position.Longitude == 0) && (this.StartPosition != null))
                                position = this.StartPosition.Position;

                            if ((position.Latitude != _lastCarPosition.Latitude) || (position.Longitude != _lastCarPosition.Longitude))
                            {
                                _lastCarPosition = position;

                                var positionAddress = await RouteHelper.GetAddressByPosition(position);

                                var contractorAddress = (trip.IsPickup ? trip.Receiver.Address : trip.Shipper.Address);

                                var routeResult = await RouteHelper.FindRouteForTrip(positionAddress, contractorAddress);
                                var route = ((routeResult != null) && (routeResult.Routes.Length > 0) ? routeResult.Routes[0] : null);
                                var leg = ((route != null) && (route.Legs.Length > 0) ? route.Legs[0] : null);

                                if (route != null)
                                {
                                    var routePoints = this.DecodeRoutePoints(route.OverviewPolyline.Points);
                                    this.RoutePoints = routePoints;
                                }
                                else
                                {
                                    string driver = (trip.Driver != null ? trip.Driver.FullName : "");
                                    ShowToastMessage.Send("Not found route from location of " + driver);
                                }

                                if (leg != null)
                                {
                                    this.CurrentPosition = new CarInfo
                                    {
                                        Distance = leg.Distance.Value,
                                        Duration = leg.Duration.Value,
                                        Position = position
                                    };
                                }
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    ShowToastMessage.Send(exception.Message);
                }
                finally
                {
                    this.StartCurrentPositionTimer();
                }
            }).LogExceptions("OwnerFleetViewModel SetCurrentPosition");
        }

        private void StartCurrentPositionTimer()
        {
            if (_currentPositionTimer == null)
            {
                _currentPositionTimer = new Timer { Interval = 15000 };
                _currentPositionTimer.Elapsed += (sender, args) =>
                {
                    this.SetCurrentPosition();
                };
            }
            _currentPositionTimer.Start();
        }

        private void StopCurrentPositionTimer()
        {
            if (_currentPositionTimer != null)
                _currentPositionTimer.Stop();
        }

        private Position[] DecodeRoutePoints(string overviewPolyline)
        {
            var points = new List<Position>();
            char[] polylinechars = overviewPolyline.ToCharArray();
            int index = 0;

            int currentLat = 0;
            int currentLng = 0;
            int next5bits;
            int sum;
            int shifter;

            try
            {
                while (index < polylinechars.Length)
                {
                    // calculate next latitude
                    sum = 0;
                    shifter = 0;
                    do
                    {
                        next5bits = (int)polylinechars[index++] - 63;
                        sum |= (next5bits & 31) << shifter;
                        shifter += 5;
                    } while (next5bits >= 32 && index < polylinechars.Length);

                    if (index >= polylinechars.Length)
                        break;

                    currentLat += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

                    //calculate next longitude
                    sum = 0;
                    shifter = 0;
                    do
                    {
                        next5bits = (int)polylinechars[index++] - 63;
                        sum |= (next5bits & 31) << shifter;
                        shifter += 5;
                    } while (next5bits >= 32 && index < polylinechars.Length);

                    if (index >= polylinechars.Length && next5bits >= 32)
                        break;

                    currentLng += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);
                    Position p = new Position(Convert.ToDouble(currentLat) / 100000.0, Convert.ToDouble(currentLng) / 100000.0);
                    points.Add(p);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            return points.ToArray();
        }

        private void ShowMainMenu(object parameter)
        {
            ShowMainMenuMessage.Send();
        }

        private void ShowHomePage(object parameter)
        {
            PopToRootPageMessage.Send();
        }

        private void SelectTrip(object parameter)
        {

        }

        private void Refresh(object parameter)
        {
            this.SelectActiveTrips();
        }

        public ObservableCollection<Trip> Trips { get; private set; }

        public Trip SelectedTrip
        {
            get { return (this.GetValue("SelectedTrip") as Trip); }
            set { this.SetValue("SelectedTrip", value); }
        }

        public Position[] BaseRoutePoints
        {
            get { return (Position[])this.GetValue("BaseRoutePoints"); }
            set { this.SetValue("BaseRoutePoints", value); }
        }

        public Position[] RoutePoints
        {
            get { return (Position[])this.GetValue("RoutePoints"); }
            set { this.SetValue("RoutePoints", value); }
        }

        public RouteBounds RouteRegion
        {
            get { return (this.GetValue("RouteRegion") as RouteBounds); }
            set { this.SetValue("RouteRegion", value); }
        }

        public AddressInfo StartPosition
        {
            get { return (this.GetValue("StartPosition") as AddressInfo); }
            set { this.SetValue("StartPosition", value); }
        }

        public AddressInfo EndPosition
        {
            get { return (this.GetValue("EndPosition") as AddressInfo); }
            set { this.SetValue("EndPosition", value); }
        }

        public CarInfo CurrentPosition
        {
            get { return (this.GetValue("CurrentPosition") as CarInfo); }
            set { this.SetValue("CurrentPosition", value); }
        }

        public bool IsRefreshing
        {
            get { return (bool)this.GetValue("IsRefreshing", false); }
            set { this.SetValue("IsRefreshing", value); }
        }

        public VisualCommand SelectTripCommand { get; private set; }

        public VisualCommand ShowMainMenuCommand { get; private set; }

        public VisualCommand ShowHomePageCommand { get; private set; }

        public VisualCommand RefreshCommand { get; private set; }
    }
    #endregion
}
