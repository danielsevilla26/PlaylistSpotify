namespace PlaylistSpotify.Utils.ExtensionMethods
{
    public static class StringExtended
    {
        public static string ShortString(this string value, int maxLength = 18)
        {
            if (value.Length > maxLength)
                return string.Format("{0}...", value[..(maxLength - 1)]);

            return value;
        }
    }
}
