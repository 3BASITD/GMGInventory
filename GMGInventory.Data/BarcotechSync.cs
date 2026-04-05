namespace GMGInventory.Data
{
    public class BarcotechSync
    {
        public BarcotechSync()
        {
        }

        public int Id { get; set; }
        public string ModuleName { get; set; }
        public string PrimaryKey { get; set; }
        public string SyncStatus { get; set; }
        public string? Remarks { get; set; }
        public string? Trace { get; set; }
        public DateTime DatePerformed { get; set; }
    }
}
