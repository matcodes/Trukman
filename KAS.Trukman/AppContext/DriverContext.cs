using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using KAS.Trukman.Classes;
using KAS.Trukman.Messages;
using Xamarin.Forms.Maps;
using KAS.Trukman.Storage;
using KAS.Trukman.Helpers;
using System.Threading.Tasks;
using KAS.Trukman.Languages;
using KAS.Trukman.Data.Classes;
using KAS.Trukman.Data.Enums;

namespace KAS.Trukman.AppContext
{
    #region DriverContext
    public class DriverContext : BaseData
    {
        #region Static members
        public static readonly double DISTANCE = 1609.34d;
        public static readonly double SAVE_LOCATION_DISTANCE = 100d;

        //public static readonly string PICKUP_PHOTO_KIND = "Pickup";
        //public static readonly string DELIVERY_PHOTO_KIND = "Delivery";
        #endregion

        private LocalStorage _localStorage = null;

        public DriverContext(LocalStorage localStorage)
            : base()
        {
            _localStorage = localStorage;

            GeoLocationChangedMessage.Subscribe(this, this.GeoLocationChanged);
            DeclineTripChangedMessage.Subscribe(this, this.DeclinedTripChanged);
            DeclineTripChangedBackMessage.Subscribe(this, this.DeclineTripChangedBack);
            DeclineTripSubmitMessage.Subscribe(this, this.DeclineTripSubmit);
            AcceptTripChangedMessage.Subscribe(this, this.AcceptTripChanged);
            CancelledTripChangedMessage.Subscribe(this, this.CancelledTripChanged);
            SendPhotoMessage.Subscribe(this, this.SendPhoto);
            CompletedTripChangedMessage.Subscribe(this, this.CompletedTripChanged);

            this.TripState = _localStorage.GetTripState();
        }

        private bool _inSynchronize = false;

        public void Synchronize()
        {
            Task.Run(async () =>
            {
                if (!_inSynchronize)
                {
                    _inSynchronize = true;
                    try
                    {
                        this.TripID = _localStorage.GetSettings(LocalStorage.TRIP_ID_SETTINGS_KEY);
                        var tripState = this.TripState;
                        this.Trip = _localStorage.SelectTripByID(this.TripID);

                        this.CalcShipperPosition();
                        this.CalcReceiverPosition();

                        if ((tripState == 0) && (this.Trip != null))
                            tripState = 1;
                        else if ((tripState == 1) && (this.Trip != null) && (this.Trip.JobCancelled))
                            tripState = 3;

                        if (this.TripState != tripState)
                            this.TripContextChanged(tripState);

                        if (((Location.Latitude != NewLocation.Latitude) || (Location.Longitude != NewLocation.Longitude)) &&
                            ((NewLocation.Latitude != 0) || (NewLocation.Longitude != 0)))
                        {
                            Location = NewLocation;
                            DriverLocationChangedMessage.Send();
                            this.CalcDistanceToShipper();
                            this.CalcDistanceToReceiver();

                            if ((this.Trip != null) && (this.Trip.DriverAccepted))
                            {
                                if (RouteHelper.Distance(OldLocation, this.Location) >= SAVE_LOCATION_DISTANCE)
                                {
                                    await _localStorage.AddLocation(this.Trip.ID, this.Location);
                                    OldLocation = Location;
                                }
                                else if ((this.Location.Latitude != 0) && (this.Location.Longitude != 0))
                                    await _localStorage.SaveLocation(this.Trip.ID, this.Location);
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                        ShowToastMessage.Send(exception.Message);
                    }
                    finally
                    {
                        _inSynchronize = false;
                    }
                }
            });
        }

        private void GeoLocationChanged(GeoLocationChangedMessage message)
        {
            Task.Run(() =>
            {
                try
                {
                    if ((this.NewLocation.Latitude != message.Latitude) || (this.NewLocation.Longitude != message.Longitude))
                    {
                        //                        var oldLocation = this.Location;
                        this.NewLocation = new Position(message.Latitude, message.Longitude);
                        //                        DriverLocationChangedMessage.Send();
                        //                        this.CalcDistanceToShipper();
                        //                        this.CalcDistanceToReceiver();

                        //                        if ((this.Trip != null) && (this.Trip.DriverAccepted))
                        //                        {
                        //                            if (RouteHelper.Distance(oldLocation, this.Location) >= SAVE_LOCATION_DISTANCE)
                        //                                _localStorage.AddLocation(this.Trip.ID, this.Location);
                        //                            else if ((this.Location.Latitude != 0) && (this.Location.Longitude != 0))
                        //                                _localStorage.SaveLocation(this.Trip.ID, this.Location);
                        //                        }
                    }
                }
                catch (Exception exception)
                {
                    ShowToastMessage.Send(exception.Message);
                }
            });
        }

        private void DeclinedTripChanged(DeclineTripChangedMessage message)
        {
            Task.Run(async () =>
            {
                _inSynchronize = true;
                var tripState = this.TripState;
                try
                {
                    if (await _localStorage.TripIsCancelled(this.TripID))
                        tripState = 3;
                    else
                        tripState = 2;
                    if (this.TripState != tripState)
                        this.TripContextChanged(tripState);
                }
                catch (Exception exception)
                {
                    ShowToastMessage.Send(exception.Message);
                }
                finally
                {
                    _inSynchronize = false;
                }
            });
        }

        private void DeclineTripChangedBack(DeclineTripChangedBackMessage message)
        {
            Task.Run(async () =>
            {
                _inSynchronize = true;
                var tripState = this.TripState;
                try
                {
                    if (await _localStorage.TripIsCancelled(this.TripID))
                        tripState = 3;
                    else
                        tripState = 1;
                    if (this.TripState != tripState)
                        this.TripContextChanged(tripState);
                }
                catch (Exception exception)
                {
                    ShowToastMessage.Send(exception.Message);
                }
                finally
                {
                    _inSynchronize = false;
                }
            });
        }

        private void DeclineTripSubmit(DeclineTripSubmitMessage message)
        {
            Task.Run(async () =>
            {
                _inSynchronize = true;
                var tripState = this.TripState;
                try
                {
                    await _localStorage.TripDeclined(this.TripID, message.DeclineReason, message.ReasonText);
                    this.Trip = null;
                    tripState = 0;
                    if (this.TripState != tripState)
                        this.TripContextChanged(tripState);
                }
                catch (Exception exception)
                {
                    ShowToastMessage.Send(exception.Message);
                }
                finally
                {
                    _inSynchronize = false;
                }
            });
        }

        private void AcceptTripChanged(AcceptTripChangedMessage message)
        {
            Task.Run(async () =>
            {
                _inSynchronize = true;
                var tripState = this.TripState;
                try
                {
                    if (await _localStorage.TripIsCancelled(this.TripID))
                        tripState = 3;
                    else
                    {
                        Trip = await _localStorage.TripAccepted(this.TripID);
                        //await _localStorage.AddPointsAsync(this.TripID, AppLanguages.CurrentLanguage.BaseJobPointsText, Trip.Points);
                        tripState = 4;
                    }
                    if (this.TripState != tripState)
                        this.TripContextChanged(tripState);
                }
                catch (Exception exception)
                {
                    ShowToastMessage.Send(exception.Message);
                }
                finally
                {
                    _inSynchronize = false;
                }
            });
        }

        private void CancelledTripChanged(CancelledTripChangedMessage message)
        {
            Task.Run(() =>
            {
                _inSynchronize = true;
                var tripState = this.TripState;
                try
                {
                    _localStorage.RemoveTrip(this.TripID);
                    this.Trip = null;
                    this.TripID = null;
                    tripState = 0;
                    if (this.TripState != tripState)
                        this.TripContextChanged(tripState);
                }
                catch (Exception exception)
                {
                    ShowToastMessage.Send(exception.Message);
                }
                finally
                {
                    _inSynchronize = false;
                }
            });
        }

        private void CompletedTripChanged(CompletedTripChangedMessage message)
        {
            Task.Run(async () =>
            {
                _inSynchronize = true;
                var tripState = this.TripState;
                try
                {
                    await _localStorage.TripCompleted(this.TripID);
                    this.Trip = null;
                    this.TripID = null;
                    tripState = 0;
                    if (this.TripState != tripState)
                        this.TripContextChanged(tripState);
                }
                catch (Exception exception)
                {
                    ShowToastMessage.Send(exception.Message);
                }
                finally
                {
                    _inSynchronize = false;
                }
            });
        }

        private void SendPhoto(SendPhotoMessage message)
        {
            Task.Run(async () =>
            {
                _inSynchronize = true;
                var tripState = this.TripState;
                var newTripState = this.TripState;
                try
                {
                    var kind = PhotoKind.Other; // "Other";
                    var notificationMessage = "";
                    var pointsText = "";
                    if ((this.TripState == 5) || (this.TripState == 6))
                    {
                        kind = PhotoKind.Pickup; // PICKUP_PHOTO_KIND;
                        tripState = 4;
                        notificationMessage = "Bill of lading was uploaded";
                        pointsText = AppLanguages.CurrentLanguage.PickUpPhotoJobPointsText;
                    }
                    else if ((this.TripState == 7) || (this.TripState == 8))
                    {
                        kind = PhotoKind.Delivery; // DELIVERY_PHOTO_KIND;
                        tripState = 9;
                        notificationMessage = "Delivery proof was uploaded";
                        pointsText = AppLanguages.CurrentLanguage.DeliveryPhotoJobPointsText;
                    }
                    await _localStorage.SendPhoto(this.TripID, message.Data, kind);
                    //await _localStorage.AddPointsAsync(this.TripID, pointsText, 5);

                    await _localStorage.SendNotification(this.Trip, notificationMessage);

                    if (this.TripState != tripState)
                    {
                        this.TripContextChanged(tripState);
                        if (this.TripState == 4)
                            ShowAdvancesPageMessage.Send(this.Trip);
                    }
                }
                catch (Exception exception)
                {
                    ShowToastMessage.Send(exception.Message);
                    StopBusyMessage.Send();
                }
                finally
                {
                    _inSynchronize = false;
                }
            });
        }

        private bool _calcShipperPositionInProgress = false;

        private void CalcShipperPosition()
        {
            if (this.Trip == null)
                this.ShipperPosition = new Position(0, 0);
            else if ((this.ShipperPosition.Latitude == 0) && (this.ShipperPosition.Longitude == 0))
                Task.Run(async () =>
                {
                    if (!_calcShipperPositionInProgress)
                    {
                        _calcShipperPositionInProgress = true;
                        try
                        {
                            var position = await RouteHelper.GetPositionByAddress(this.Trip.Shipper.Address);
                            this.ShipperPosition = position;
                            TripShipperPositionChangedMessage.Send();
                            this.CalcDistanceToShipper();
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception);
                        }
                        finally
                        {
                            _calcShipperPositionInProgress = false;
                        }
                    }
                });
        }

        private bool _calcReceiverPositionInProgress = false;

        private void CalcReceiverPosition()
        {

            if (this.Trip == null)
                this.ReceiverPosition = new Position(0, 0);
            else if ((this.ReceiverPosition.Latitude == 0) && (this.ReceiverPosition.Longitude == 0))
                Task.Run(async () =>
                {
                    if (!_calcReceiverPositionInProgress)
                    {
                        _calcReceiverPositionInProgress = true;
                        try
                        {
                            var position = await RouteHelper.GetPositionByAddress(this.Trip.Receiver.Address);
                            this.ReceiverPosition = position;
                            TripReceiverPositionChangedMessage.Send();
                            this.CalcDistanceToReceiver();
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception);
                        }
                        finally
                        {
                            _calcReceiverPositionInProgress = false;
                        }
                    }
                });
        }

        private void CalcDistanceToShipper()
        {
            Task.Run(async () =>
            {
                try
                {
                    var distance = RouteHelper.Distance(this.Location, this.ShipperPosition);
                    this.DistanceToShipper = distance;
                    await this.CheckArrivedToPickup();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            });
        }

        private void CalcDistanceToReceiver()
        {
            Task.Run(async () =>
            {
                try
                {
                    var distance = RouteHelper.Distance(this.Location, this.ReceiverPosition);
                    this.DistanceToReceiver = distance;
                    await this.CheckArrivedToDelivery();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            });
        }

        private async Task CheckArrivedToPickup()
        {
            if ((this.Trip != null) && (this.Trip.DriverAccepted) &&
                (!this.Trip.IsPickup) && (this.DistanceToShipper < DISTANCE) &&
                (_localStorage.GetPhoto(Trip.ID, PhotoKind.Pickup) == null) /*&&
                this.Trip.Location != default(Position)*/)
            {
                //                _inSynchronize = true;
                try
                {
                    var now = DateTime.Now;
                    var tripState = (now <= this.Trip.PickupDatetime ? 5 : 6);

                    double minutes = 0;
                    if (now > this.Trip.PickupDatetime)
                        minutes = (now - this.Trip.PickupDatetime).TotalMinutes * (-1);
                    else
                        minutes = (this.Trip.PickupDatetime - now).TotalMinutes;
                    if ((minutes >= 0) && (minutes < 1))
                        minutes = 1;
                    else if ((minutes < 0) && (minutes > -1))
                        minutes = -1;

                    var trip = await _localStorage.TripInPickup(this.TripID, (int)minutes);

                    //var pointsText = (minutes >= 0 ? AppLanguages.CurrentLanguage.PickUpOnTimeJobPointsText : AppLanguages.CurrentLanguage.PickUpLateJobPointsText);
                    //var points = (minutes >= 0 ? 50 : -10);
                    //await _localStorage.AddPointsAsync(trip.ID, pointsText, points);
                    //if (minutes >= 15)
                    //    await _localStorage.AddPointsAsync(trip.ID, AppLanguages.CurrentLanguage.PickUpOnTimeEarlyJobPointsText, 5);

                    var message = String.Format(AppLanguages.CurrentLanguage.OwnerArrivedToPickupSystemMessage, trip.JobRef, TrukmanContext.User.FullName);
                    await _localStorage.SendNotification(Trip, message);

                    Trip = trip;

                    if (this.TripState != tripState)
                        this.TripContextChanged(tripState);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    ShowToastMessage.Send(exception.Message);
                }
                finally
                {
                    //                    _inSynchronize = false;
                }
            }
        }

        private async Task CheckArrivedToDelivery()
        {
            if ((this.Trip != null) && (this.Trip.DriverAccepted) &&
                (this.Trip.IsPickup) && (!this.Trip.IsDelivery) &&
                (this.DistanceToReceiver < DISTANCE) &&
                (_localStorage.GetPhoto(Trip.ID, PhotoKind.Pickup) != null) &&
                (_localStorage.GetPhoto(Trip.ID, PhotoKind.Delivery) == null)/* &&
                this.Trip.Location != default(Position)*/)
            {
                //                _inSynchronize = true;
                try
                {
                    var now = DateTime.Now;
                    var tripState = (now <= this.Trip.DeliveryDatetime ? 7 : 8);

                    double minutes = 0;
                    if (now > this.Trip.DeliveryDatetime)
                        minutes = (now - this.Trip.DeliveryDatetime).TotalMinutes * (-1);
                    else
                        minutes = (this.Trip.DeliveryDatetime - now).TotalMinutes;
                    if ((minutes >= 0) && (minutes < 1))
                        minutes = 1;
                    else if ((minutes < 0) && (minutes > -1))
                        minutes = -1;

                    var trip = await _localStorage.TripInDelivery(this.TripID, (int)minutes);

                    //var pointsText = (minutes >= 0 ? AppLanguages.CurrentLanguage.DeliveryOnTimeJobPointsText : AppLanguages.CurrentLanguage.DeliveryLateJobPointsText);
                    //var points = (minutes >= 0 ? 50 : -10);
                    //await _localStorage.AddPointsAsync(trip.ID, pointsText, points);
                    //if (minutes >= 15)
                    //    await _localStorage.AddPointsAsync(trip.ID, AppLanguages.CurrentLanguage.DeliveryOnTimeEarlyJobPointsText, 5);

                    var message = String.Format(AppLanguages.CurrentLanguage.OwnerArrivedToDeliverySystemMessage, trip.JobRef, trip.Driver.FullName);
                    await _localStorage.SendNotification(Trip, message);

                    Trip = trip;

                    if (this.TripState != tripState)
                    {
                        this.TripContextChanged(tripState);
                        if ((this.TripState == 7) || (this.TripState == 8))
                            ShowAdvancesPageMessage.Send(Trip);
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    ShowToastMessage.Send(exception.Message);
                }
                finally
                {
                    //                    _inSynchronize = false;
                }
            }
        }

        private void TripContextChanged(int tripState)
        {
            _localStorage.SetTripState(tripState);
            this.TripState = tripState;
            this.Trip = _localStorage.SelectTripByID(this.TripID);

            var systemMessage = "";
            if (this.TripState == 1)
                systemMessage = AppLanguages.CurrentLanguage.FindNextTripSystemMessage;
            else if (this.TripState == 3)
                systemMessage = AppLanguages.CurrentLanguage.TripCancelledSystemMessage;
            else if ((this.TripState == 5) || (this.TripState == 6))
                systemMessage = AppLanguages.CurrentLanguage.ArrivedToPickupSystemMessage;
            else if ((this.TripState == 7) || (this.TripState == 8))
                systemMessage = AppLanguages.CurrentLanguage.ArrivedToDeliverySystemMessage;

            if (!String.IsNullOrEmpty(systemMessage))
                ShowNotificationMessage.Send(systemMessage);

            DriverTripContextChangedMessage.Send();
        }

        public Position Location { get; private set; }

        public Position NewLocation { get; private set; }

        public Position OldLocation { get; private set; }

        public string TripID { get; private set; }

        public int TripState { get; private set; }

        public Trip Trip { get; private set; }

        public Position ShipperPosition { get; private set; }

        public Position ReceiverPosition { get; private set; }

        public double DistanceToShipper { get; private set; }

        public double DistanceToReceiver { get; private set; }
    }
    #endregion
}