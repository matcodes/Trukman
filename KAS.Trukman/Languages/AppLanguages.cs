using KAS.Trukman.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Languages
{
    #region AppLanguages
    public static class AppLanguages
    {
        private static Dictionary<string, AppLanguage> __languages = new Dictionary<string, AppLanguage>();

        private static string[] __months;

        static AppLanguages()
        {
            CurrentLanguage = new AppLanguage("");

            AddLanguage(new EnglishAppLanguage());

            SetLanguage("English");
        }

        public static void SetLanguage(string languageDisplayName)
        {
            if ((CurrentLanguage == null) || (CurrentLanguage.DisplayName != languageDisplayName))
            {
                AppLanguage language = null;
                if (__languages.TryGetValue(languageDisplayName, out language))
                {
                    CurrentLanguage.Assign(language);

                    __months = new string[] 
                    {
                        CurrentLanguage.January,
                        CurrentLanguage.February,
                        CurrentLanguage.March,
                        CurrentLanguage.April,
                        CurrentLanguage.May,
                        CurrentLanguage.June,
                        CurrentLanguage.July,
                        CurrentLanguage.August,
                        CurrentLanguage.September,
                        CurrentLanguage.October,
                        CurrentLanguage.November,
                        CurrentLanguage.December
                    };

                    LanguageSelectedMessage.Send();
                }
            }
        }

        public static string GetTimeString(DateTime time)
        {
            var prefix = "";
            var hour = time.Hour;
            if (hour > 12)
            {
                hour -= 12;
                prefix = CurrentLanguage.TimePM;
            }
            else
                prefix = CurrentLanguage.TimeAM;

            var result = String.Format("{0} {1}, {2} {3}:{4} {5}", 
                GetMonthByIndex(time.Month), time.Day, time.Year, hour, time.Minute, prefix);
            return result;
        }

        private static string GetMonthByIndex(int month)
        {
            return __months[month - 1];
        }

        private static void AddLanguage(AppLanguage appLanguage)
        {
            __languages.Add(appLanguage.DisplayName, appLanguage);
        }

        public static AppLanguage CurrentLanguage { get; private set; }
    }
    #endregion
}
