﻿using System;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms;
using KAS.Trukman.Controls;
using KAS.Trukman.iOS;
using MapKit;
using CoreLocation;
using System.Collections.Generic;
using UIKit;
using Foundation;

[assembly: ExportRenderer(typeof(AppMap), typeof(AppMapRenderer))]
namespace KAS.Trukman.iOS
{
	#region AppMapRenderer
	public class AppMapRenderer : MapRenderer, IMKMapViewDelegate
	{
		#region Static members
		public static readonly string ROUTE_START_PIN_ID = "RouteStartPinID";
		public static readonly string ROUTE_END_PIN_ID = "RouteEndPinID";
		public static readonly string ROUTE_CAR_PIN_ID = "RouteStartPinID";
		#endregion

		private MKPolyline _baseRoute = null;
		private MKPolyline _route = null;
	
		private MKPointAnnotation _routeStartPosition = null;
		private MKPointAnnotation _routeEndPosition = null;
		private MKPointAnnotation _routeCarPosition = null;

		private MKMapView _mapView = null;

		protected override void OnElementChanged (Xamarin.Forms.Platform.iOS.ElementChangedEventArgs<View> args)
		{
			base.OnElementChanged (args);

			if (_mapView == null) {
				_mapView = new MKMapView {
					MapType = MKMapType.Standard,
					ZoomEnabled = true,
					ScrollEnabled = true,
					ShowsBuildings = true,
					PitchEnabled = true,
				};

				var mapDelegate = this;

				_mapView.Delegate = mapDelegate;

				SetNativeControl (_mapView);
			}

		}

		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs args)
		{
			base.OnElementPropertyChanged (sender, args);

			var map = (this.Element as AppMap);

			if (args.PropertyName == "BaseRoutePoints") {
				if (_baseRoute != null)
					_mapView.RemoveOverlay (_baseRoute);

				var coords = new List<CLLocationCoordinate2D> ();
				foreach (var position in map.BaseRoutePoints)
					coords.Add (new CLLocationCoordinate2D (position.Latitude, position.Longitude));

				_baseRoute = MKPolyline.FromCoordinates (coords.ToArray ());

				this.InvokeOnMainThread (() => {
					_mapView.AddOverlay (_baseRoute);
				});
			} else if (args.PropertyName == "RoutePoints") {
				if (_route != null)
					_mapView.RemoveOverlay (_route);

				var coords = new List<CLLocationCoordinate2D> ();
				foreach (var position in map.RoutePoints)
					coords.Add (new CLLocationCoordinate2D (position.Latitude, position.Longitude));

				_route = MKPolyline.FromCoordinates (coords.ToArray ());
				_mapView.AddOverlay (_route);
			} else if (args.PropertyName == "RouteStartPosition") {
				if (_routeStartPosition != null)
					_mapView.RemoveAnnotation (_routeStartPosition);

				_routeStartPosition = new MKPointAnnotation {
					Title = map.RouteStartPosition.Contractor.Name,
					Subtitle = map.RouteStartPosition.Address,
					Coordinate = new CLLocationCoordinate2D (map.RouteStartPosition.Position.Latitude, map.RouteStartPosition.Position.Longitude)
				};

				_mapView.AddAnnotation (_routeStartPosition);
			} else if (args.PropertyName == "RouteEndPosition") {
				if (_routeEndPosition != null)
					_mapView.RemoveAnnotation (_routeEndPosition);

				_routeEndPosition = new MKPointAnnotation {
					Title = map.RouteEndPosition.Contractor.Name,
					Subtitle = map.RouteEndPosition.Address,
					Coordinate = new CLLocationCoordinate2D (map.RouteEndPosition.Position.Latitude, map.RouteEndPosition.Position.Longitude)
				};

				_mapView.AddAnnotation (_routeEndPosition);
			} else if (args.PropertyName == "RouteCarPosition") {
				if (_routeCarPosition != null)
					_mapView.RemoveAnnotation (_routeCarPosition);

				_routeCarPosition = new MKPointAnnotation {
					Title = map.RouteCarPosition.GetDurationText(),
					Subtitle = map.RouteCarPosition.GetDistanceTextFromMiles(),
					Coordinate = new CLLocationCoordinate2D (map.RouteCarPosition.Position.Latitude, map.RouteCarPosition.Position.Longitude)
				};

				CLLocationCoordinate2D carPosition = new CLLocationCoordinate2D (map.RouteCarPosition.Position.Latitude, map.RouteCarPosition.Position.Longitude);
				nativeMap.Region = MKCoordinateRegion.FromDistance (carPosition, 1000, 1000);
				nativeMap.AddAnnotation (_routeCarPosition);
			}
		}

		[Export ("mapView:viewForAnnotation:")]
		public virtual MKAnnotationView GetViewForAnnotation (MKMapView mapView, IMKAnnotation annotation)
		{
			MKAnnotationView view = null;
			if (annotation == _routeStartPosition) {
				var annotationView = (MKAnnotationView)mapView.DequeueReusableAnnotation (ROUTE_START_PIN_ID);
				if (annotationView == null)
					annotationView = new MKAnnotationView (annotation, ROUTE_START_PIN_ID);
				annotationView.Image = this.GetPinImage ("marker_start");
				annotationView.CalloutOffset = new CoreGraphics.CGPoint(0, 0);
				view = annotationView;
			} else if (annotation == _routeEndPosition) {
				var annotationView = new MKAnnotationView (annotation, ROUTE_END_PIN_ID);
				annotationView.Image = this.GetPinImage ("marker_finish");
				annotationView.CanShowCallout = true;
				view = annotationView;
			} else if (annotation == _routeCarPosition) {
				var annotationView = new MKAnnotationView (annotation, ROUTE_CAR_PIN_ID);
				annotationView.Image = this.GetPinImage ("marker_car");
				annotationView.CanShowCallout = true;
				view = annotationView;
			}
			return view;
		}

		private UIImage GetPinImage(string name)
		{
			var image = UIImage.FromBundle(name);
			image = image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
			return image;
		}

		[Export ("mapView:rendererForOverlay:")]
		public MKOverlayRenderer OverlayRenderer (MKMapView mapView, IMKOverlay overlay)
		{
			MKOverlayRenderer renderer = null;
			if (overlay == _baseRoute) {
				var polylineRenderer = new MKPolylineRenderer (overlay as MKPolyline);
				var color = UIColor.FromRGB (0, 255, 33);
				polylineRenderer.FillColor = color;
				polylineRenderer.StrokeColor = color;
				polylineRenderer.LineWidth = 3;
				polylineRenderer.Alpha = 0.5f;
				renderer = polylineRenderer;
			} else if (overlay == _route) {
				var polylineRenderer = new MKPolylineRenderer (overlay as MKPolyline);
				var color = UIColor.FromRGB (0, 38, 255);
				polylineRenderer.FillColor = color;
				polylineRenderer.StrokeColor = color;
				polylineRenderer.LineWidth = 3;
				polylineRenderer.Alpha = 0.5f;
				renderer = polylineRenderer;
			}
			return renderer;
		}
	}
	#endregion
}
