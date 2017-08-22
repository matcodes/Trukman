using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms;
using KAS.Trukman.Controls;
using KAS.Trukman.Droid.Renderers;
using Xamarin.Forms.Platform.Android;
using Android.Gms.Maps;
using System.ComponentModel;
using System.Threading.Tasks;
using Android.Gms.Maps.Model;
using KAS.Trukman.Languages;
using KAS.Trukman.Data.Classes;
using Xamarin.Forms.Maps;

[assembly: ExportRenderer(typeof(AppMap), typeof(AppMapRenderer))]
namespace KAS.Trukman.Droid.Renderers
{
    #region AppMapRenderer
    public class AppMapRenderer : MapRenderer, IOnMapReadyCallback, GoogleMap.IInfoWindowAdapter
    {
        #region Static members
        public static readonly string SHIPPER_MARKER = "SHIPPER";
        public static readonly string RECEIVER_MARKER = "RECEIVER";
        public static readonly string CAR_MARKER = "CAR";
        #endregion

        private MapView _mapView = null;
        private GoogleMap _map = null;
        private bool _isMapProcess = false;
        private bool _isFirstDraw = true;

        private PolylineOptions _polylineOptions = null;
        private PolylineOptions _basePolylineOptions = null;
        private MarkerOptions _startMarkerOptions = null;
        private MarkerOptions _endMarkerOptions = null;
        private MarkerOptions _carMarkerOptions = null;

        private Polyline _polyline = null;
        private Polyline _basePolyline = null;
        private Marker _startMarker = null;
        private Marker _endMarker = null;
        private Marker _carMarker = null;

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            _mapView = (this.Control as MapView);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if ((args.PropertyName == "RoutePoints") ||
                (args.PropertyName == "BaseRoutePoints") ||
                (args.PropertyName == "RouteStartPosition") ||
                (args.PropertyName == "RouteEndPosition") ||
                (args.PropertyName == "RouteCarPosition"))
            {
                if (_map == null)
                {
                    if (!_isMapProcess)
                    {
                        _isMapProcess = true;
                        _mapView.GetMapAsync(this);
                    }
                }
                else
                    this.DrawItems();
            }
            else
                base.OnElementPropertyChanged(sender, args);
        }

        #region IOnMapReadyCallback
        public void OnMapReady(GoogleMap googleMap)
        {
            _map = googleMap;
            _map.SetInfoWindowAdapter(this);
            this.DrawItems();
        }
        #endregion

        private void DrawItems()
        {
            Task.Run(() => 
            {
                var element = (this.Element as AppMap);

                if (element.RouteStartPosition == null)
                    _startMarkerOptions = null;
                else if (_startMarkerOptions == null)
                {
                    _startMarkerOptions = new MarkerOptions();
                    _startMarkerOptions.SetTitle(SHIPPER_MARKER);
                    _startMarkerOptions.SetPosition(new LatLng(element.RouteStartPosition.Position.Latitude, element.RouteStartPosition.Position.Longitude));
                    _startMarkerOptions.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.marker_start));
                }

                if (element.RouteEndPosition == null)
                    _endMarkerOptions = null;
                else if (_endMarkerOptions == null)
                {
                    _endMarkerOptions = new MarkerOptions();
                    _endMarkerOptions.SetTitle(RECEIVER_MARKER);
                    _endMarkerOptions.SetPosition(new LatLng(element.RouteEndPosition.Position.Latitude, element.RouteEndPosition.Position.Longitude));
                    _endMarkerOptions.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.marker_finish));
                }

                if (element.RouteCarPosition != null)
                {
                    _carMarkerOptions = new MarkerOptions();
                    _carMarkerOptions.SetTitle(CAR_MARKER);
                    _carMarkerOptions.SetPosition(new LatLng(element.RouteCarPosition.Position.Latitude, element.RouteCarPosition.Position.Longitude));
                    _carMarkerOptions.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.marker_car));
                }
                else
                    _carMarkerOptions = null;

                if (element.BaseRoutePoints != null)
                {
                    var points = new List<LatLng>();
                    foreach (var position in (this.Element as AppMap).BaseRoutePoints)
                        points.Add(new LatLng(position.Latitude, position.Longitude));
                    _basePolylineOptions = new PolylineOptions();
                    _basePolylineOptions.InvokeColor(Android.Graphics.Color.Argb(128, 0, 255, 33));
                    _basePolylineOptions.Geodesic(true);
                    _basePolylineOptions.Add(points.ToArray());
                }

                if (element.RoutePoints != null)
                {
                    var points = new List<LatLng>();
                    foreach (var position in (this.Element as AppMap).RoutePoints)
                        points.Add(new LatLng(position.Latitude, position.Longitude));
                    _polylineOptions = new PolylineOptions();
                    _polylineOptions.InvokeColor(Android.Graphics.Color.Argb(128, 0, 38, 255));
                    _polylineOptions.Geodesic(true);
                    _polylineOptions.Add(points.ToArray());
                }

                this.ShowElements();
            });
        }

        private void ShowElements()
        {
            (this.Context as Activity).RunOnUiThread(() =>
            {
                if (_startMarker != null)
                    _startMarker.Remove();
                if (_endMarker != null)
                    _endMarker.Remove();
                if (_basePolyline != null)
                    _basePolyline.Remove();
                if (_polyline != null)
                    _polyline.Remove();
                if (_carMarker != null)
                    _carMarker.Remove();

                if (_startMarkerOptions != null)
                    _startMarker = _map.AddMarker(_startMarkerOptions);
                if (_endMarkerOptions != null)
                    _endMarker = _map.AddMarker(_endMarkerOptions);
                if (_basePolylineOptions != null)
                    _basePolyline = _map.AddPolyline(_basePolylineOptions);
                if (_polylineOptions != null)
                    _polyline = _map.AddPolyline(_polylineOptions);
                if (_carMarkerOptions != null)
                    _carMarker = _map.AddMarker(_carMarkerOptions);

                LatLng center = null;
                if (_carMarker != null)
                    center = new LatLng(_carMarker.Position.Latitude, _carMarker.Position.Longitude);
                else if (_startMarker != null)
                    center = new LatLng(_startMarker.Position.Latitude, _startMarker.Position.Longitude);
                else if (_endMarker != null)
                    center = new LatLng(_endMarker.Position.Latitude, _endMarker.Position.Longitude);

                if (center != null)
                {
                    if (_isFirstDraw)
                    {
                        _map.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(center, 13));
                        _isFirstDraw = true;
                    }
                    else
                        _map.AnimateCamera(CameraUpdateFactory.NewLatLng(center));
                }
            });
        }

        #region GoogleMap.IInfoWindowAdapter
        public Android.Views.View GetInfoContents(Marker marker)
        {
            Android.Views.View view = null;
            var inflater = (Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService) as Android.Views.LayoutInflater);
            if (inflater != null)
            {
                if (marker.Title == SHIPPER_MARKER)
                    view = this.CreateContractorInfoView(inflater, (this.Element as AppMap).RouteStartPosition.Contractor, AppLanguages.CurrentLanguage.TripShipperTitleLabel);
                else if (marker.Title == RECEIVER_MARKER)
                    view = this.CreateContractorInfoView(inflater, (this.Element as AppMap).RouteEndPosition.Contractor, AppLanguages.CurrentLanguage.TripReceiverTitleLabel);
                else if (marker.Title == CAR_MARKER)
                    view = this.CreateCarInfoView(inflater);
            }
            return view;
        }

        public Android.Views.View GetInfoWindow(Marker marker)
        {
            return null;
        }
        #endregion

        private Android.Views.View CreateContractorInfoView(Android.Views.LayoutInflater inflater, Contractor contractor, string titleText)
        {
            var view = inflater.Inflate(Resource.Layout.contractor_info, null);

            var title = view.FindViewById<TextView>(Resource.Id.contractorTitle);
            var nameLabel = view.FindViewById<TextView>(Resource.Id.nameLabel);
            var nameValue = view.FindViewById<TextView>(Resource.Id.nameValue);
            var phoneLabel = view.FindViewById<TextView>(Resource.Id.phoneLabel);
            var phoneValue = view.FindViewById<TextView>(Resource.Id.phoneValue);
            var faxLabel = view.FindViewById<TextView>(Resource.Id.faxLabel);
            var faxValue = view.FindViewById<TextView>(Resource.Id.faxValue);
            var addressLabel = view.FindViewById<TextView>(Resource.Id.addressLabel);
            var addressValue = view.FindViewById<TextView>(Resource.Id.addressValue);

            title.Text = titleText;
            nameLabel.Text = AppLanguages.CurrentLanguage.ContractorPageNameLabel;
            nameValue.Text = contractor.Name;
            phoneLabel.Text = AppLanguages.CurrentLanguage.ContractorPagePhoneLabel;
            phoneValue.Text = contractor.Phone;
            faxLabel.Text = AppLanguages.CurrentLanguage.ContractorPageFaxLabel;
            faxValue.Text = contractor.Fax;
            addressLabel.Text = AppLanguages.CurrentLanguage.ContractorPageAddressLabel;
            addressValue.Text = contractor.Address;
            
            return view;
        }

        private Android.Views.View CreateCarInfoView(Android.Views.LayoutInflater inflater)
        {
            var view = inflater.Inflate(Resource.Layout.car_info, null);

            var durationValue = view.FindViewById<TextView>(Resource.Id.durationValue);
            var distanceValue = view.FindViewById<TextView>(Resource.Id.distanceValue);

            var carInfo = (this.Element as AppMap).RouteCarPosition;
            if (carInfo != null)
            {
                durationValue.Text = carInfo.GetDurationText();
                distanceValue.Text = carInfo.GetDistanceTextFromMiles();
            }
            else
            {
                durationValue.Text = "";
                distanceValue.Text = "";
            }

            return view;
        }
    }
    #endregion
}