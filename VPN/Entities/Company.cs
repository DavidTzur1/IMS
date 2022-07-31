namespace VPN.Entities
{
    public class Company
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string BillingID { get; set; } = string.Empty;
        public bool IsDeactivated { get; set; } = false;
        public bool IsForceOnNet { get; set; } = false;
        public bool IsCompanyCallsOnly { get; set; } = false;
        public string BarringList { get; set; } = string.Empty;
        public string AllowanceList { get; set; } = string.Empty;
    }
}
