namespace GMGInventory.Common.Constants
{
    public static class SystemFormat
    {
        public const string StringDateTime = "MMM dd, yyyy hh:mm:ss tt";
        public const string StringDate = "MMMM dd, yyyy";
        public const string StringDecimalComma = "{0:#,##0.00}";
        //public const string SqlConnectionString = "Server=.;Database=GMGInventory;Trusted_Connection=True;MultipleActiveResultSets=true;";
        public const string SqlConnectionString = "Server=192.168.0.36;Database=GMGInventory;User Id=sa;Password=Monitor321;";

        public static string Name(string? FirstName, string? LastName, string Nickname)
        {
            return string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName)
                 ? Nickname
                 : (FirstName + " " + LastName);
        }
    }
}
