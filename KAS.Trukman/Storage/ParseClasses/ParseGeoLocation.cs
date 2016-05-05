using Parse;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Storage.ParseClasses
{
    #region ParseGeoLocation
    [ParseClassName("GeoLocation")]
    public class ParseGeoLocation : ParseObject
    {
        [ParseFieldName("Location")]
        public ParseGeoPoint Location
        {
            get { return this.GetProperty<ParseGeoPoint>(); }
            set { this.SetProperty<ParseGeoPoint>(value); }
        }

        [ParseFieldName("PointCreatedAt")]
        public DateTime PointCreatedAt
        {
            get { return this.GetProperty<DateTime>(); }
            set { this.SetProperty<DateTime>(value); }
        }
    }
    #endregion
}
