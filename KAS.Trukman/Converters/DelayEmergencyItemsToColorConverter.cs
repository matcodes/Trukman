using KAS.Trukman.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trucman.Converters
{
    #region DelayEmergencyItemsToColorConverter
    public class DelayEmergencyItemsToColorConverter : IValueConverter
    {
        #region IValueConverter
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = Color.Gray;
            if (parameter != null)
            {
                var selectedValue = (int)value;
                var thisValue = (int)parameter;
                color = (selectedValue == thisValue ? PlatformHelper.DelayEmergencySelectedItemColor : PlatformHelper.DelayEmergencyItemColor);
            }
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
    #endregion
}
