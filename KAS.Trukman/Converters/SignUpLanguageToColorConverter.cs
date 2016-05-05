using KAS.Trukman.Data.Enums;
using KAS.Trukman.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Converters
{
    #region SignUpLanguageToColorConverter
    public class SignUpLanguageToColorConverter : IValueConverter
    {
        #region IValueConverter
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var language = (int)value;
            var currentLanguage = (int)parameter;
            return (language == currentLanguage ? PlatformHelper.SignUpSelectedItemColor : PlatformHelper.SignUpItemColor);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
    #endregion
}
