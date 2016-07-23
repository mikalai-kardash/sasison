namespace sasison
{
    public static class StringExtensions
    {
        public static string FixSpaces(this string s)
        {
            return s.Replace(" ", "\\s?");
        }
    }
}