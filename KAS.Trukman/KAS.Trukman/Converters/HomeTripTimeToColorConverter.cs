using KAS.Trukman.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Converters
{
    #region HomeTripTimeToColorConverter
    public class HomeTripTimeToColorConverter : IValueConverter
    {
        #region IValueConverter
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isTimeOver = (bool)value;
            return (isTimeOver ? PlatformHelper.HomeTimeOverColor : PlatformHelper.HomeTimeColor);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
    #endregion
}
