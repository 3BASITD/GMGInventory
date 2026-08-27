namespace GMGInventory.Common.Constants
{
    public static class SystemFormat
    {
        public const string StringDateTime = "MMM dd, yyyy hh:mm tt";
        public const string StringDate = "MMMM dd, yyyy";
        public const string StringDate2 = "MM/dd/yyyy";
        public const string StringDate3 = "yyyy-MM-dd";
        public const string StringDateTime3 = "yyyy-MM-ddTHH:mm";
        public const string StringDate4 = "MM/dd/yy";
        public const string StringDate5 = "ddd (MM/dd/yy)";
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

        public static string ToValidFileName(string input, string replacement = "_", bool replacespaces = false)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "untitled";

            var invalidChars = Path.GetInvalidFileNameChars();
            var cleaned = string.Join(replacement, input.Split(invalidChars, StringSplitOptions.RemoveEmptyEntries));

            if (replacespaces)
                cleaned = cleaned.Replace(" ", replacement);

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

    public class FormattedDate
    {
        private readonly DateTime _date;

        public FormattedDate(DateTime date)
        {
            _date = date;
        }

        public string Format1 => _date.ToString(SystemFormat.StringDate);
        public string Format2 => _date.ToString(SystemFormat.StringDate2);
        public string Format3 => _date.ToString(SystemFormat.StringDate3);
        public string Format4 => _date.ToString(SystemFormat.StringDate4);
        public string Format5 => _date.ToString(SystemFormat.StringDate5);

        // Optional: default string output
        public override string ToString()
        {
            return _date.ToString(SystemFormat.StringDate);
        }
    }
}