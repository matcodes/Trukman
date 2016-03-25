using System;
using UIKit;
using Foundation;

namespace Trukman.iOS.Helpers
{
	public class SettingsService
	{
		static NSUserDefaults preferences;

		static SettingsService()
		{
			preferences = NSUserDefaults.StandardUserDefaults;
		}

		public static void AddOrUpdateSetting(string key, string value)
		{
			if (string.IsNullOrEmpty(key))
				throw new ArgumentNullException("key");

			preferences.SetValueForKey ((NSString)value, (NSString)key);
			preferences.Synchronize ();
		}

		public static NSObject GetSetting(string key, NSObject defaultValue)
		{
			if (string.IsNullOrEmpty(key))
				throw new ArgumentNullException("key");

			NSObject value = preferences.ValueForKey ((NSString)key);
			if (value == null) {
				value = defaultValue;
			}

			return value;
		}
	}
}

