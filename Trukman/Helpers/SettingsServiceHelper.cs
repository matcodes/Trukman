using System;

namespace Trukman
{
	public interface ISettingsService
	{
		void AddOrUpdateSetting<T>(string key, T value);
		T GetSetting<T>(string key, T defaultValue = default(T));
	}
	
	public static class SettingsServiceHelper
	{
		static ISettingsService settingsService;

		static string companySetting = "CompanyName";
		static string rejectedCounterSetting = "RejectedCounter";
		static string lastRejectedTimeSetting = "LastRejectedTime";
		//static string CurrentJobId = "CurrentJobId";
		//static string FuelId = "FuelId";
		//static string Lumper = "Lumper";

		public static int MaxRejectedRequestCount = 3;
		
		public static void Initialize (ISettingsService _settingsService)
		{
			settingsService = _settingsService;
		}

		/// <summary>
		/// Saves the company name on device settings
		/// </summary>
		/// <param name="company">Company.</param>
		public static void SaveCompany(string company)
		{
			if (settingsService != null)
				settingsService.AddOrUpdateSetting (companySetting, company);
			
		}

		/// <summary>
		/// Gets the company name from device settings
		/// </summary>
		/// <returns>The company.</returns>
		public static string GetCompany()
		{
			if (settingsService != null)
				return settingsService.GetSetting<string> (companySetting);
			else
				return null;
		}

		public static void SaveRejectedCounter(int reject)
		{
			if (settingsService != null)
				settingsService.AddOrUpdateSetting<int> (rejectedCounterSetting, reject);
		}

		public static int GetRejectCounter()
		{
			if (settingsService != null)
				return settingsService.GetSetting<int> (rejectedCounterSetting, 0);
			else
				return 0;
		}

		public static void SaveLastRejectTime(DateTime date)
		{
			if (settingsService != null)
				settingsService.AddOrUpdateSetting<DateTime> (lastRejectedTimeSetting, date);	
		}

		public static DateTime GetLastRejectTime()
		{
			if (settingsService != null)
				return settingsService.GetSetting<DateTime> (lastRejectedTimeSetting, DateTime.MinValue);
			else
				return DateTime.MinValue;
		}

		/*public static void AddOrUpdateSetting<T>(string key, T value)
		{
			if (settingsService != null)
				settingsService.AddOrUpdateSetting<T> (key, value);
		}

		public static T GetSetting<T>(string key, T defaultValue = default(T))
		{
			if (settingsService != null)
				return settingsService.GetSetting<T> (key, defaultValue);
			else
				return defaultValue;
		}*/
	}
}

