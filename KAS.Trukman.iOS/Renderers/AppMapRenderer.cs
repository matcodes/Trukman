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
		private MKPolyline _baseRoute = null;
		private MKPolyline _route = null;
	

		private MKPolylineRenderer _baseRouteRenderer = null;
		private MKPolylineRenderer _routeRenderer = null;

		protected override void OnElementChanged (Xamarin.Forms.Platform.iOS.ElementChangedEventArgs<View> args)
		{
			base.OnElementChanged (args);

			var nativeMap = (this.Control as MKMapView);

			if ((nativeMap != null) && (args.OldElement != null)) 
				nativeMap.OverlayRenderer = null;

			if ((nativeMap != null) && (args.NewElement != null)) 
				nativeMap.OverlayRenderer = this.GetOverlayRenderer;
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
			}
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

