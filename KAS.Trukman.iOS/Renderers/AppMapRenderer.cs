using System;
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

		protected override void OnElementChanged (Xamarin.Forms.Platform.iOS.ElementChangedEventArgs<View> args)
		{
			base.OnElementChanged (args);

			var nativeMap = (this.Control as MKMapView);

			if ((nativeMap != null) && (args.OldElement != null)) 
				nativeMap.OverlayRenderer = null;

			if ((nativeMap != null) && (args.NewElement != null)) {
				nativeMap.WeakDelegate = this;
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
					Title = map.RouteCarPosition.GetDurationText(),
					Subtitle = map.RouteCarPosition.GetDistanceTextFromMiles(),
					Coordinate = new CLLocationCoordinate2D (map.RouteCarPosition.Position.Latitude, map.RouteCarPosition.Position.Longitude)
				};

				nativeMap.AddAnnotation (_routeCarPosition);
			}
		}

		[Export ("mapView:viewForAnnotation:")]
		public virtual MKAnnotationView GetViewForAnnotation (MKMapView mapView, IMKAnnotation annotation)
		{
			MKAnnotationView view = null;
			if (annotation == _routeStartPosition) {
				var annotationView = (MKPinAnnotationView)mapView.DequeueReusableAnnotation (ROUTE_START_PIN_ID);
				if (annotationView == null)
					annotationView = new MKPinAnnotationView (annotation, ROUTE_START_PIN_ID);
				annotationView.Image = this.GetPinImage ("marker_start");
				annotationView.CanShowCallout = true;
				view = annotationView;
			} else if (annotation == _routeEndPosition) {
				var annotationView = new MKPinAnnotationView (annotation, ROUTE_END_PIN_ID);
				annotationView.Image = this.GetPinImage ("marker_end");
				annotationView.CanShowCallout = true;
				view = annotationView;
			} else if (annotation == _routeCarPosition) {
				var annotationView = new MKPinAnnotationView (annotation, ROUTE_CAR_PIN_ID);
				annotationView.Image = this.GetPinImage ("marker_car");
				annotationView.CanShowCallout = true;
				view = annotationView;
			}
			return view;
		}

		private UIImage GetPinImage(string name)
		{
			var image = UIImage.FromBundle("marker_start");
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

