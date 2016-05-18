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
using CoreGraphics;
using KAS.Trukman.Languages;

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

		protected override void OnElementChanged (Xamarin.Forms.Platform.iOS.ElementChangedEventArgs<View> args)
		{
			base.OnElementChanged (args);

			var nativeMap = (this.Control as MKMapView);

			if ((nativeMap != null) && (args.OldElement != null)) {
				nativeMap.OverlayRenderer = null;
				nativeMap.GetViewForAnnotation = null;
				nativeMap.DidSelectAnnotationView -= this.DidSelectAnnotationView;
				nativeMap.DidDeselectAnnotationView -= this.DidDeselectAnnotationView;
			}

			if ((nativeMap != null) && (args.NewElement != null)) {
				nativeMap.OverlayRenderer = this.OverlayRenderer;
				nativeMap.GetViewForAnnotation = this.GetViewForAnnotation;
				nativeMap.DidSelectAnnotationView += this.DidSelectAnnotationView;
				nativeMap.DidDeselectAnnotationView += this.DidDeselectAnnotationView;
			}
		}

		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs args)
		{
			base.OnElementPropertyChanged (sender, args);
			this.InvokeOnMainThread (() => {
				
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
					CLLocationCoordinate2D carPosition = new CLLocationCoordinate2D (map.RouteCarPosition.Position.Latitude, map.RouteCarPosition.Position.Longitude);
					var region = new MKCoordinateRegion(carPosition, nativeMap.Region.Span);
					if (_routeCarPosition != null)
						nativeMap.RemoveAnnotation (_routeCarPosition);
					else 
						region = MKCoordinateRegion.FromDistance (carPosition, 1000, 1000);

					nativeMap.Region =  region;

					_routeCarPosition = new MKPointAnnotation {
						Title = map.RouteCarPosition.GetDurationText (),
						Subtitle = map.RouteCarPosition.GetDistanceTextFromMiles (),
						Coordinate = new CLLocationCoordinate2D (map.RouteCarPosition.Position.Latitude, map.RouteCarPosition.Position.Longitude)
					};

					nativeMap.AddAnnotation (_routeCarPosition);
				}
			});
		}

		[Export ("mapView:viewForAnnotation:")]
		public virtual MKAnnotationView GetViewForAnnotation (MKMapView mapView, IMKAnnotation annotation)
		{
			MKAnnotationView view = null;
			if (annotation == _routeStartPosition) {
				var annotationView = (MKAnnotationView)mapView.DequeueReusableAnnotation (ROUTE_START_PIN_ID);
				if (annotationView == null)
					annotationView = new MKAnnotationView (annotation, ROUTE_START_PIN_ID);
				var image = this.GetPinImage ("marker_start");
				annotationView.Image = image;
				annotationView.CalloutOffset = new CoreGraphics.CGPoint(0, image.Size.Height / 2 * -1);
				view = annotationView;
			} else if (annotation == _routeEndPosition) {
				var annotationView = new MKAnnotationView (annotation, ROUTE_END_PIN_ID);
				var image = this.GetPinImage ("marker_finish");
				annotationView.Image = image;
				annotationView.CalloutOffset = new CoreGraphics.CGPoint(0, image.Size.Height / 2 * -1);
				view = annotationView;
			} else if (annotation == _routeCarPosition) {
				var annotationView = (MKAnnotationView)mapView.DequeueReusableAnnotation (ROUTE_CAR_PIN_ID);
				if (annotationView == null)
					annotationView = new MKAnnotationView (annotation, ROUTE_CAR_PIN_ID);
				var image = this.GetPinImage ("marker_car");
				annotationView.Image = image;
				annotationView.CalloutOffset = new CoreGraphics.CGPoint(0, image.Size.Height / 2 * -1);
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

		private CarInfoViewController _carInfoView = null;
		private ContractorInfoViewController _shipperInfoView = null;
		private ContractorInfoViewController _receiverInfoView = null;

		private void DidSelectAnnotationView(object sender, MKAnnotationViewEventArgs args)
		{
			var map = (this.Element as AppMap);
			if ((args.View.Annotation == _routeCarPosition) && (_carInfoView == null)) {
				_carInfoView = new CarInfoViewController ();
				_carInfoView.CarInfo = map.RouteCarPosition;
				args.View.AddSubview (_carInfoView.View);

				UIView.Animate (0.4, () => {
					_carInfoView.View.Frame = new CGRect (-105, -62, 211, 58); 
				});
			} else if ((args.View.Annotation == _routeStartPosition) && (_shipperInfoView == null)) {
				_shipperInfoView = new ContractorInfoViewController ();
				_shipperInfoView.ContractorTypeText = AppLanguages.CurrentLanguage.TripShipperTitleLabel;
				_shipperInfoView.ContractorInfo = map.RouteStartPosition;
				args.View.AddSubview (_shipperInfoView.View);

				UIView.Animate (0.4, () => {
					_shipperInfoView.View.Frame = new CGRect (-130, -110, 310, 107); 
				});
			} else if ((args.View.Annotation == _routeEndPosition) && (_receiverInfoView == null)) {
				_receiverInfoView = new ContractorInfoViewController ();
				_receiverInfoView.ContractorTypeText = AppLanguages.CurrentLanguage.TripShipperTitleLabel;
				_receiverInfoView.ContractorInfo = map.RouteEndPosition;
				args.View.AddSubview (_receiverInfoView.View);

				UIView.Animate (0.4, () => {
					_receiverInfoView.View.Frame = new CGRect (-130, -110, 310, 107); 
				});
			}
		}

		private void DidDeselectAnnotationView(object sender, MKAnnotationViewEventArgs args)
		{
			// remove the image view when the conference annotation is deselected
			if ((args.View.Annotation == _routeCarPosition) && (_carInfoView != null)) {
				_carInfoView.View.RemoveFromSuperview ();
				_carInfoView.Dispose ();
				_carInfoView = null;
			} else if ((args.View.Annotation == _routeStartPosition) && (_shipperInfoView != null)) {
				_shipperInfoView.View.RemoveFromSuperview ();
				_shipperInfoView.Dispose ();
				_shipperInfoView = null;
			} else if ((args.View.Annotation == _routeEndPosition) && (_receiverInfoView != null)) {
				_receiverInfoView.View.RemoveFromSuperview ();
				_receiverInfoView.Dispose ();
				_receiverInfoView = null;
			}
		}
	}
	#endregion
}

