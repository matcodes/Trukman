using System;

using Xamarin.Forms;
using System.Globalization;
using Trukman.ViewModels.Pages;

namespace Trukman
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
