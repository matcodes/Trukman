namespace KAS.Trukman.Droid.Helpers
{
    public static class StringExtensions
    {
        public static string Pack(this string s, int? maxlen = null)
        {
            if (s != null && maxlen.HasValue && maxlen > 0 && s.Length > maxlen.Value)
            {
                int l = s.Length - maxlen.Value;
                s = s.Remove(maxlen.Value, l);
            }
            return (s == null) ? null : s.Trim().Trim('\uFEFF');
        }

        public static string PackToNull(this string input, int? maxlen = null)
        {
            var s = input.Pack(maxlen);
            return string.IsNullOrWhiteSpace(s) ? null : s;
        }

        /// <summary>
        /// Appends the literal slash mark (/) to the end 
        /// </summary>
        public static string EnsureTrailingSlash(this string input, bool ignoreNull = true)
        {
            var s = input.PackToNull();
            if (s == null)
            {
                if (!ignoreNull) return null;
                s = "";
            }

            if (!s.EndsWith("/"))
                return s + "/";

            return s;
        }

        public static string UrlEncode(this string input)
        {
            if (input == null)
                return null;
            else
                return System.Net.WebUtility.UrlEncode(input);
        }

        public static bool IsNullOrEmpty(this string input)
        {
            return string.IsNullOrEmpty(input);
        }

        public static string format(this string input, params object[] args)
        {
            return string.Format(input ?? "{0}", args);
        }
    }
}