using KAS.Trukman.Helpers;
using KAS.Trukman.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Converters
{
    #region HomeStateToImageConverter
    public class HomeStateToImageConverter : IValueConverter
    {
        #region IValueConverter
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var state = (HomeStates)value;
            var imageSource = (state == HomeStates.TripDeclined ? PlatformHelper.LeftImageSource : PlatformHelper.MenuImageSource);
            return imageSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
    #endregion
}
