using KAS.Trukman.Helpers;
using KAS.Trukman.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Converters
{
    #region LumperStateToImageConverter
    public class LumperStateToImageConverter : IValueConverter
    {
        #region IValueConverter
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var source = PlatformHelper.LumperImageSource;
            var state = (LumperStates)value;
            if (state == LumperStates.Requested)
                source = PlatformHelper.LumperRequestedImageSource;
            else if (state == LumperStates.Received)
                source = PlatformHelper.LumperReceivedImageSource;
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
