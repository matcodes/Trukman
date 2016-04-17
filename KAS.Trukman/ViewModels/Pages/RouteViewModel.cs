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

namespace KAS.Trukman.ViewModels.Pages
{
    #region RouteViewModel
    public class RouteViewModel : PageViewModel
    {
        private int _currentIndex = -1;

        private System.Timers.Timer _currentPositionTimer = null;

        private RouteResult _routeResult = null;
        private Route _route = null;
        private RouteLeg _leg = null;

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
                this.RemoveSteps();
                if (this.CurrentPosition != null)
                    this.StepInfoText = String.Format("{0}, {1}", this.CurrentPosition.GetDistanceTextFromMiles(), this.CurrentPosition.GetDurationText());
                else
                    this.StepInfoText = "";
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
                _routeResult = await RouteHelper.FindRouteForTrip(this.Trip);
                _route = ((_routeResult != null) && (_routeResult.Routes.Length > 0) ? _routeResult.Routes[0] : null);
                _leg = ((_route != null) && (_route.Legs.Length > 0) ? _route.Legs[0] : null);
                if (_route != null)
                {
                    this.RouteRegion = _route.Bounds;

                    if (_leg != null)
                    {
                        this.StartPosition = new AddressInfo
                        {
                            Address = _leg.StartAddress,
                            Position = new Position(_leg.StartLocation.Latitude, _leg.StartLocation.Longitude),
                            Contractor = this.Trip.Shipper
                        };
                        this.EndPosition = new AddressInfo
                        {
                            Address = _leg.EndAddress,
                            Position = new Position(_leg.EndLocation.Latitude, _leg.EndLocation.Longitude),
                            Contractor = this.Trip.Receiver
                        };

                        List<RouteStepInfo> steps = new List<RouteStepInfo>();

                        for (var index = 0; index < _leg.Steps.Length; index++)
                        {
                            var turn = RouteStepTurn.None;
                            if (_leg.Steps[index].Maneuver == "turn-left")
                                turn = RouteStepTurn.Left;
                            else if (_leg.Steps[index].Maneuver == "turn-right")
                                turn = RouteStepTurn.Right;

                            var routeStep = new RouteStepInfo {
                                StepIndex = index,
                                Text = this.ParseHtmlText(_leg.Steps[index].HtmlInstructions),
                                Distance = _leg.Steps[index].Distance.Text,
                                Turn = turn
                            };

                            steps.Add(routeStep);
                        }

                        this.AddRouteSteps(steps.ToArray());
                    }
                    else
                    {
                        this.StartPosition = null;
                        this.EndPosition = null;
                    }

                    var routePoints = this.DecodeRoutePoints(_route.OverviewPolyline.Points);

                    this.RoutePoints = routePoints;

                    this.GetCurrentPosition();
                }
            });
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
                    this.StopCurrentPositionTimer();

                    this.GetCurrentPosition();
                };
            }
            _currentPositionTimer.Start();
        }

        private void StopCurrentPositionTimer()
        {
            if (_currentPositionTimer != null)
                _currentPositionTimer.Stop();
        }

        private void GetCurrentPosition()
        {
            Task.Run(() => {
                // To do: Get current position
                if (_currentIndex < _leg.Steps.Length - 1)
                    _currentIndex++;

                var step = _leg.Steps[_currentIndex];
                var distance = (long)0;
                var duration = (long)0;
                for (int index = _currentIndex; index < _leg.Steps.Length; index++)
                {
                    distance += _leg.Steps[index].Distance.Value;
                    duration += _leg.Steps[index].Duration.Value;
                }

                var carInfo = new CarInfo {
                    Position = new Position(step.StartLocation.Latitude, step.StartLocation.Longitude),
                    Distance = distance,
                    Duration = duration
                };

                this.CurrentPosition = carInfo;

                this.StartCurrentPositionTimer();
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
