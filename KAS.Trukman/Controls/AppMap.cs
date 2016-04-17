using KAS.Trukman.Data.Route;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Runtime.CompilerServices;
using KAS.Trukman.Data.Maps;

namespace KAS.Trukman.Controls
{
    #region AppMap
    public class AppMap : Map
    {
        #region Static members
        public static BindableProperty RoutePointsProperty = BindableProperty.Create("RoutePoints", typeof(Position[]), typeof(AppMap), new Position[] { });
        public static BindableProperty RouteRegionProperty = BindableProperty.Create("RouteRegion", typeof(RouteBounds), typeof(AppMap), null);
        public static BindableProperty RouteStartPositionProperty = BindableProperty.Create("RouteStartPosition", typeof(AddressInfo), typeof(AppMap), null);
        public static BindableProperty RouteEndPositionProperty = BindableProperty.Create("RouteEndPosition", typeof(AddressInfo), typeof(AppMap), null);
        public static BindableProperty RouteCarPositionProperty = BindableProperty.Create("RouteCarPosition", typeof(CarInfo), typeof(AppMap), null);
        #endregion

        public AppMap() 
            : base()
        {
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if ((propertyName == "RouteRegion") && (this.RouteRegion != null))
            {
                var mapSpan = this.GetRegionSpan();
                this.MoveToRegion(mapSpan);
            }
            else if ((propertyName == "RouteCarPosition") && (this.RouteCarPosition != null) && (this.RouteStartPosition != null))
            {
                if (!this.RouteCarPosition.Position.Equals(this.RouteStartPosition.Position))
                {
                    var mapSpan = this.GetRegionSpan(this.RouteCarPosition.Position);
                    this.MoveToRegion(mapSpan);
                }
            }
        }

        private MapSpan GetRegionSpan()
        {
            var latitude = (this.RouteRegion.NorthEast.Latitude + this.RouteRegion.SouthWest.Latitude) / 2;
            var longitude = (this.RouteRegion.NorthEast.Longitude + this.RouteRegion.SouthWest.Longitude) / 2;

            return this.GetRegionSpan(new Position(latitude, longitude));
        }

        private MapSpan GetRegionSpan(Position center)
        {
            var latDeg = Math.Abs(this.RouteRegion.NorthEast.Latitude - this.RouteRegion.SouthWest.Latitude) * 1.3;
            var lonDeg = Math.Abs(this.RouteRegion.NorthEast.Longitude - this.RouteRegion.SouthWest.Longitude) * 1.3;

            return new MapSpan(center, latDeg, lonDeg);
        }

        public Position[] RoutePositions
        {
            get { return (Position[])this.GetValue(RoutePointsProperty); }
            set { this.SetValue(RoutePointsProperty, value); }
        }

        public RouteBounds RouteRegion
        {
            get { return (RouteBounds)this.GetValue(RouteRegionProperty); }
            set { this.SetValue(RouteRegionProperty, value); }
        }

        public AddressInfo RouteStartPosition
        {
            get { return (AddressInfo)this.GetValue(RouteStartPositionProperty); }
            set { this.SetValue(RouteStartPositionProperty, value); }
        }

        public AddressInfo RouteEndPosition
        {
            get { return (AddressInfo)this.GetValue(RouteEndPositionProperty); }
            set { this.SetValue(RouteEndPositionProperty, value); }
        }

        public CarInfo RouteCarPosition
        {
            get { return (CarInfo)this.GetValue(RouteCarPositionProperty); }
            set { this.SetValue(RouteCarPositionProperty, value); }
        }
    }
    #endregion
}
