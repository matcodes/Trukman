using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace KAS.Trukman.Data.Maps
{
    #region CarInfo
    public class CarInfo
    {
        public CarInfo() 
            : base()
        {
        }

        public string GetDurationText()
        {
            var time = TimeSpan.FromSeconds(this.Duration);
            var minutes = "";
            var hours = "";
            if (time.Hours > 0)
                hours = String.Format("{0} hours", time.Hours);
            if ((time.Minutes > 0) || (String.IsNullOrEmpty(hours)))
                minutes = String.Format("{0} mins", time.Minutes);
            return (hours + " " + minutes).Trim();
        }

        public string GetDistanceTextFromKilometer()
        {
            var kilometers = this.Distance / 1000.0f;
            return String.Format("{0} km", kilometers.ToString("0.###")).Replace('.', ',');
        }

        public string GetDistanceTextFromMiles()
        {
            var miles = this.Distance / 1609.34f;
            return String.Format("{0} mi", miles.ToString("0.###")).Replace('.', ',');
        }

        public Position Position { get; set; }

        public long Distance { get; set; }

        public long Duration { get; set; }
    }
    #endregion
}
