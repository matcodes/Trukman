using System;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms;
using KAS.Trukman.Controls;
using KAS.Trukman.iOS;
using MapKit;
using CoreLocation;
using System.Collections.Generic;
using UIKit;

[assembly: ExportRenderer(typeof(AppMap), typeof(AppMapRenderer))]
namespace KAS.Trukman.iOS
{
	#region AppMapRenderer
	public class AppMapRenderer : MapRenderer
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

		private MKPinAnnotationView _routeStartPinAnnotationView = null;
		private MKPinAnnotationView _routeEndPinAnnotationView = null;
		private MKPinAnnotationView _routeCarPinAnnotationView = null;

		private MKPolylineRenderer _baseRouteRenderer = null;
		private MKPolylineRenderer _routeRenderer = null;

		protected override void OnElementChanged (Xamarin.Forms.Platform.iOS.ElementChangedEventArgs<View> args)
		{
			base.OnElementChanged (args);

			var nativeMap = (this.Control as MKMapView);

			if ((nativeMap != null) && (args.OldElement != null)) {
				nativeMap.OverlayRenderer = null;
				nativeMap.Delegate = null;
			}

			if ((nativeMap != null) && (args.NewElement != null)) {
				nativeMap.Delegate = null;
				nativeMap.OverlayRenderer = this.GetOverlayRenderer;
				nativeMap.GetViewForAnnotation = this.GetViewForAnnotation;
			}
		}

		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs args)
		{
			base.OnElementPropertyChanged (sender, args);

			var map = (this.Element as AppMap);
			var nativeMap = (this.Control as MKMapView);

			if (args.PropertyName == "BaseRoutePoints") {
				if (_baseRoute != null)
					nativeMap.RemoveOverlay (_baseRoute);

				var coords = new List<CLLocationCoordinate2D> ();
				foreach (var position in map.BaseRoutePoints)
					coords.Add (new CLLocationCoordinate2D (position.Latitude, position.Longitude));

				_baseRoute = MKPolyline.FromCoordinates (coords.ToArray ());
				nativeMap.AddOverlay (_baseRoute);
			} else if (args.PropertyName == "RoutePoints") {
				if (_route != null)
					nativeMap.RemoveOverlay (_route);

				var coords = new List<CLLocationCoordinate2D> ();
				foreach (var position in map.RoutePoints)
					coords.Add (new CLLocationCoordinate2D (position.Latitude, position.Longitude));

				_route = MKPolyline.FromCoordinates (coords.ToArray ());
				nativeMap.AddOverlay (_route);
			} else if (args.PropertyName == "RouteStartPosition") {
				if (_routeStartPosition != null)
					nativeMap.RemoveAnnotation (_routeStartPosition);

				_routeStartPosition = new MKPointAnnotation {
					Title = map.RouteStartPosition.Contractor.Name,
					Subtitle = map.RouteStartPosition.Address,
					Coordinate = new CLLocationCoordinate2D (map.RouteStartPosition.Position.Latitude, map.RouteStartPosition.Position.Longitude)
				};

				nativeMap.AddAnnotation (_routeStartPosition);
			} else if (args.PropertyName == "RouteEndPosition") {
				if (_routeEndPosition != null)
					nativeMap.RemoveAnnotation (_routeEndPosition);

				_routeEndPosition = new MKPointAnnotation {
					Title = map.RouteEndPosition.Contractor.Name,
					Subtitle = map.RouteEndPosition.Address,
					Coordinate = new CLLocationCoordinate2D (map.RouteEndPosition.Position.Latitude, map.RouteEndPosition.Position.Longitude)
				};

				nativeMap.AddAnnotation (_routeEndPosition);
			} else if (args.PropertyName == "RouteCarPosition") {
				if (_routeCarPosition != null)
					nativeMap.RemoveAnnotation (_routeCarPosition);

				_routeCarPosition = new MKPointAnnotation {
					Title = map.RouteCarPosition.Duration.ToString(),
					Subtitle = map.RouteCarPosition.Distance.ToString(),
					Coordinate = new CLLocationCoordinate2D (map.RouteCarPosition.Position.Latitude, map.RouteCarPosition.Position.Longitude)
				};
			}
		}

		private MKAnnotationView GetViewForAnnotation (MKMapView mapView, IMKAnnotation annotation)
		{
			MKAnnotationView view = null;
			if (annotation == _routeStartPosition) {
				if (_routeStartPinAnnotationView == null) {
					_routeStartPinAnnotationView = new MKPinAnnotationView (annotation, ROUTE_START_PIN_ID);
					_routeStartPinAnnotationView.Image = this.GetPinImage("marker_start");
					_routeStartPinAnnotationView.CanShowCallout = true;
				}
				view = _routeStartPinAnnotationView;
			} else if (annotation == _routeEndPosition) {
				if (_routeEndPinAnnotationView == null) {
					_routeEndPinAnnotationView = new MKPinAnnotationView (annotation, ROUTE_END_PIN_ID);
					_routeEndPinAnnotationView.Image = this.GetPinImage ("marker_end");
					_routeEndPinAnnotationView.CanShowCallout = true;
				}
				view = _routeEndPinAnnotationView;
			} else if (annotation == _routeCarPosition) {
				if (_routeCarPinAnnotationView == null) {
					_routeCarPinAnnotationView = new MKPinAnnotationView (annotation, ROUTE_CAR_PIN_ID);
					_routeCarPinAnnotationView.Image = this.GetPinImage ("marker_car");
					_routeCarPinAnnotationView.CanShowCallout = true;
				}
				view = _routeCarPinAnnotationView;
			}
			return view;
		}

		private UIImage GetPinImage(string name)
		{
			var image = UIImage.FromBundle("marker_start");
			image = image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
			return image;
		}

		private MKOverlayRenderer GetOverlayRenderer(MKMapView mapView, IMKOverlay overlay)
		{
			MKOverlayRenderer renderer = null;
			if (overlay == _baseRoute) {
				if (_baseRouteRenderer == null) {
					_baseRouteRenderer = new MKPolylineRenderer (overlay as MKPolyline);
					var color = UIColor.FromRGB (0, 255, 33);
					_baseRouteRenderer.FillColor = color;
					_baseRouteRenderer.StrokeColor = color;
					_baseRouteRenderer.LineWidth = 3;
					_baseRouteRenderer.Alpha = 0.5f;
				}
				renderer = _baseRouteRenderer;
			} else if (overlay == _route) {
				if (_route == null) {
					_routeRenderer = new MKPolylineRenderer (overlay as MKPolyline);
					var color = UIColor.FromRGB (0, 38, 255);
					_routeRenderer.FillColor = color;
					_routeRenderer.StrokeColor = color;
					_routeRenderer.LineWidth = 3;
					_routeRenderer.Alpha = 0.5f;
				}
				renderer = _routeRenderer;
			}
			return renderer;
		}
	}
	#endregion
}

