namespace VendingMachineAPI.Models.Options
{
    public class DbConnection
    {
        public const string ConfigSection = "DbConnection";

        public string ConnectionString { get; set; }
    }
}
