using KAS.Trukman.Data.Maps;
using KAS.Trukman.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Converters
{
    #region RouteStepTurnToImageConverter
    public class RouteStepTurnToImageConverter : IValueConverter
    {
        #region IValueConverter
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var routeStepTurn = (RouteStepTurn)value;
            var imageSource = PlatformHelper.TurnNoneImageSource;
            if (routeStepTurn == RouteStepTurn.Left)
                imageSource = PlatformHelper.TurnLeftImageSource;
            else if (routeStepTurn == RouteStepTurn.Right)
                imageSource = PlatformHelper.TurnRightImageSource;
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
