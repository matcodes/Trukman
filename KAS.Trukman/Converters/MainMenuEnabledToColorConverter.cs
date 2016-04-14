using KAS.Trukman.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Converters
{
    #region MainMenuEnabledToColorConverter
    public class MainMenuEnabledToColorConverter : IValueConverter
    {
        #region IValueConverter
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var enabled = (bool)value;
            return (enabled ? PlatformHelper.MainMenuEnabledColor : PlatformHelper.MainMenuDisabledColor);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
    #endregion
}
