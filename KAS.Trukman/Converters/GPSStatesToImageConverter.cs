using KAS.Trukman.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Converters
{
    #region GPSStatesToImageConverter
    public class GPSStatesToImageConverter : IValueConverter
    {
        #region IValueConvert
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var source = PlatformHelper.GpsOnImageSource;
            var state = (int)value;
            if (state == 1)
                source = PlatformHelper.GpsOffImageSource;
            else if (state == 2)
                source = PlatformHelper.GpsOffWarningImageSource;
            return source;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
    #endregion
}
