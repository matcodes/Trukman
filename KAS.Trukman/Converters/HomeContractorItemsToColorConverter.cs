using KAS.Trukman.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Converters
{
    #region ContractorItemsToColorConverter
    public class HomeContractorItemsToColorConverter : IValueConverter
    {
        #region IValueConverter
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var selectedItem = (int)value;
            var item = (int)parameter;
            return (selectedItem == item ? PlatformHelper.HomeSelectedItemColor : PlatformHelper.HomeItemColor);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
    #endregion
}
