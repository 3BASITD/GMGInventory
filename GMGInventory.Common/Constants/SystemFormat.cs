namespace GMGInventory.Common.Constants
{
    public static class SystemFormat
    {
        public const string StringDateTime = "MMM dd, yyyy hh:mm:ss tt";
        public const string StringDate = "MMMM dd, yyyy";
        public const string StringDecimalComma = "{0:#,##0.00}";
        public const string StringIntegerComma = "{0:#,##0}";

        public static string Name(string? FirstName, string? LastName, string Nickname)
        {
            return string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName)
                 ? Nickname
                 : (FirstName + " " + LastName);
        }
    }
}
