using KAS.Trukman.Classes;
using KAS.Trukman.Data.Interfaces;
using KAS.Trukman.Data.Maps;
using KAS.Trukman.Data.Route;
using KAS.Trukman.Helpers;
using KAS.Trukman.Languages;
using KAS.Trukman.Messages;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using KAS.Trukman.AppContext;

namespace KAS.Trukman.ViewModels.Pages
{
    #region RouteViewModel
    public class RouteViewModel : PageViewModel
    {
        private int _currentIndex = -1;

        private System.Timers.Timer _currentPositionTimer = null;

        public RouteViewModel()
            : base()
        {
            this.RouteSteps = new ObservableCollection<RouteStepInfo>();

            this.ShowMainMenuCommand = new VisualCommand(this.ShowMainMenu);
            this.ShowHomePageCommand = new VisualCommand(this.ShowHomePage);
        }

        public override void Initialize(params object[] parameters)
        {
            base.Initialize(parameters);

            this.Trip = ((parameters != null && parameters.Length > 0) ? (parameters[0] as ITrip) : null);
        }

        public override void Appering()
        {
            base.Appering();
        }

        public override void Disappering()
        {
            base.Disappering();

            this.StopCurrentPositionTimer();
        }

        protected override void DoPropertyChanged(string propertyName)
        {
            if (propertyName == "Trip")
                this.CreateRoute();
            else if (propertyName == "CurrentPosition")
            {
                //this.RemoveSteps();
                //if (this.CurrentPosition != null)
                //    this.StepInfoText = String.Format("{0}, {1}", this.CurrentPosition.GetDistanceTextFromMiles(), this.CurrentPosition.GetDurationText());
                //else
                //    this.StepInfoText = "";
            }
            else if ((propertyName == "SelectedRouteStep") && (this.SelectedRouteStep != null))
                this.SelectedRouteStep = null;

            base.DoPropertyChanged(propertyName);
        }

        protected override void Localize()
        {
            this.Title = AppLanguages.CurrentLanguage.RoutePageName;
        }

        protected override void EnabledCommands()
        {
            base.EnabledCommands();
        }

        protected override void DisableCommands()
        {
            base.DisableCommands();
        }

        private void ShowMainMenu(object parameter)
        {
            ShowMainMenuMessage.Send();
        }

        private void ShowHomePage(object parameter)
        {
            PopToRootPageMessage.Send();
        }

        private void CreateRoute()
        {
            Task.Run(async () =>
            {
                this.IsBusy = true;
                try
                {
                    var routeResult = await RouteHelper.FindRouteForTrip(this.Trip);
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
                                Contractor = this.Trip.Shipper
                            };
                            this.EndPosition = new AddressInfo
                            {
                                Address = leg.EndAddress,
                                Position = new Position(leg.EndLocation.Latitude, leg.EndLocation.Longitude),
                                Contractor = this.Trip.Receiver
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
            });
        }

        private void RecreateRoute()
        {
            var timer = new System.Timers.Timer { Interval = 200 };
            timer.Elapsed += (sender, args) => 
            {
                timer.Stop();
                this.CreateRoute();
            };
            timer.Start();
        }

        private string ParseHtmlText(string htmlText)
        {
            var result = "";
            var strings = htmlText.Split('<', '>');
            for (int index = 0; index < strings.Length; index += 2)
                result += strings[index];
            return result;
        }

        private void AddRouteSteps(RouteStepInfo[] steps)
        {
            Device.BeginInvokeOnMainThread(() => {
                this.RouteSteps.Clear();
                foreach (var step in steps)
                    this.RouteSteps.Add(step);
            });
        }

        private void RemoveSteps()
        {
            var steps = this.RouteSteps.TakeWhile(rs => rs.StepIndex < _currentIndex).ToArray();
            Device.BeginInvokeOnMainThread(() => {
                foreach (var step in steps)
                    this.RouteSteps.Remove(step);
            });
        }

        private void StartCurrentPositionTimer()
        {
            if (_currentPositionTimer == null)
            {
                _currentPositionTimer = new System.Timers.Timer { Interval = 10000 };
                _currentPositionTimer.Elapsed += (sender, args) => {
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

        private void SetCurrentPosition()
        {
            Task.Run(async () =>
            {
                this.StopCurrentPositionTimer();
                try
                {
                    if (this.Trip != null)
                    {
                        var position = TrukmanContext.Driver.Location;

                        if ((position.Latitude == 0) && (position.Longitude == 0) && (this.StartPosition != null))
                            position = this.StartPosition.Position;

                        var positionAddress = await RouteHelper.GetAddressByPosition(position);

                        var contractorAddress = (this.Trip.IsPickup ? this.Trip.Receiver.Address : this.Trip.Shipper.Address);

                        var routeResult = await RouteHelper.FindRouteForTrip(positionAddress, contractorAddress);
                        var route = ((routeResult != null) && (routeResult.Routes.Length > 0) ? routeResult.Routes[0] : null);
                        var leg = ((route != null) && (route.Legs.Length > 0) ? route.Legs[0] : null);

                        if (route != null)
                        {
                            List<RouteStepInfo> steps = new List<RouteStepInfo>();

                            for (var index = 0; index < leg.Steps.Length; index++)
                            {
                                var turn = RouteStepTurn.None;
                                if (leg.Steps[index].Maneuver == "turn-left")
                                    turn = RouteStepTurn.Left;
                                else if (leg.Steps[index].Maneuver == "turn-right")
                                    turn = RouteStepTurn.Right;

                                var routeStep = new RouteStepInfo
                                {
                                    StepIndex = index,
                                    Text = this.ParseHtmlText(leg.Steps[index].HtmlInstructions),
                                    Distance = leg.Steps[index].Distance.Text,
                                    Turn = turn
                                };

                                steps.Add(routeStep);
                            }

                            this.AddRouteSteps(steps.ToArray());

                            var routePoints = this.DecodeRoutePoints(route.OverviewPolyline.Points);
                            this.RoutePoints = routePoints;
                        }

                        this.CurrentPosition = new CarInfo
                        {
                            Distance = leg.Distance.Value,
                            Duration = leg.Duration.Value,
                            Position = position
                        };
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
            });
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

        public ITrip Trip
        {
            get { return (this.GetValue("Trip") as ITrip); }
            set { this.SetValue("Trip", value); }
        }

        public Position[] BaseRoutePoints
        {
            get { return (Position[])this.GetValue("BaseRoutePoints", new Position[] { }); }
            set { this.SetValue("BaseRoutePoints", value); }
        }

        public Position[] RoutePoints
        {
            get { return (Position[])this.GetValue("RoutePoints", new Position[] { }); }
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

        public string StepInfoText
        {
            get { return (string)this.GetValue("StepInfoText"); }
            set { this.SetValue("StepInfoText", value); }
        }

        public RouteStepInfo SelectedRouteStep
        {
            get { return (this.GetValue("SelectedRouteStep") as RouteStepInfo); }
            set { this.SetValue("SelectedRouteStep", value); }
        }

        public ObservableCollection<RouteStepInfo> RouteSteps { get; private set; }

        public VisualCommand ShowMainMenuCommand { get; private set; }

        public VisualCommand ShowHomePageCommand { get; private set; }
    }
    #endregion
}
