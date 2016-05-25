using System;
using Xamarin.Forms;
using KAS.Trukman.Languages;

namespace KAS.Trukman
{
	#region DelayEmergenceToTextConverter
	public class DelayEmergenceToTextConverter : IValueConverter
	{
		#region IValueConverter implementation
		public object Convert (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var result = "";
			int index = (int)value;
			if (index == 0)
				result = AppLanguages.CurrentLanguage.DelayFlatTireLabel;
			else if (index == 1)
				result = AppLanguages.CurrentLanguage.DelayFeelingSleepyLabel;
			else if (index == 2)
				result = AppLanguages.CurrentLanguage.DelayRoadWorkAheadLabel;
			return result;
		}

		public object ConvertBack (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException ();
		}
		#endregion
	}
	#endregion
}

