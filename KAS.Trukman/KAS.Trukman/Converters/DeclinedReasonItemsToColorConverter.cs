using KAS.Trukman.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Converters
{
    #region DeclinedReasonItemsToColorConverter
    public class DeclinedReasonItemsToColorConverter : IValueConverter
    {
        #region IValueConverter
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = Color.Gray;
            if (parameter != null)
            {
                var selectedValue = (int)value;
                var thisValue = (int)parameter;
                color = (selectedValue == thisValue ? PlatformHelper.HomeSelectedItemColor : PlatformHelper.HomeItemColor);
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
