namespace PlaylistSpotify.Utils.ExtensionMethods
{
    public static class StringExtended
    {
        public static string ShortString(this string value)
        {
            if (value.Length > 20)
                return string.Format("{0}...", value[..19]);

            return value;
        }
    }
}
