using KAS.Trukman.Helpers;
using KAS.Trukman.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Converters
{
    #region FuelAdvanceStateToImageConverter
    public class FuelAdvanceStateToImageConverter : IValueConverter
    {
        #region IValueConverter
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var source = PlatformHelper.FuelImageSource;
            var state = (FuelAdvanceStates)value;
            if (state == FuelAdvanceStates.Requested)
                source = PlatformHelper.FuelRequestedImageSource;
            else if (state == FuelAdvanceStates.Received)
                source = PlatformHelper.FuelReceivedImageSource;
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
