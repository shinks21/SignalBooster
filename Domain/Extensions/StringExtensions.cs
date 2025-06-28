namespace Domain.Extensions
{
    public static class StringExtensions
    {
        public static bool ContainsSignalValue(this string textToParse, string valueToFind)
        {
            if (!textToParse.Contains(valueToFind, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;
        }
    }
}
