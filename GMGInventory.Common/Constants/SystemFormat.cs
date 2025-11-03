namespace GMGInventory.Common.Constants
{
    public static class SystemFormat
    {
        public const string StringDateTime = "MMM dd, yyyy hh:mm:ss tt";
        public const string StringDate = "MMMM dd, yyyy";
        public const string StringDate2 = "MM/dd/yyyy";
        public const string StringDate3 = "yyyy-MM-dd";
        public const string StringDecimalComma = "{0:#,##0.00}";
        public const string StringIntegerComma = "{0:#,##0}";

        public static string Name(string? FirstName, string? LastName, string Nickname)
        {
            return string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName)
                 ? Nickname
                 : (FirstName + " " + LastName);
        }

        public static string GetInnermostExceptionMessage(Exception ex)
        {
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            return ex.Message;
        }

        public static string ToValidFileName(string input, string replacement = "_")
        {
            if (string.IsNullOrWhiteSpace(input))
                return "untitled";

            var invalidChars = Path.GetInvalidFileNameChars();
            var cleaned = string.Join(replacement, input.Split(invalidChars, StringSplitOptions.RemoveEmptyEntries));

            cleaned = cleaned.Trim().TrimEnd('.');

            return string.IsNullOrEmpty(cleaned) ? "untitled" : cleaned;
        }

        public static string CustomerPriority(int prio)
        {
            return prio switch
            {
                5 => "Urgent",
                4 => "High",
                3 => "Normal",
                2 => "Low",
                _ => "Deferred"
            };
        }
    }
}