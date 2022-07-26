namespace VPN.Models
{
    public class CompanyModel
    {
        public int CompanyID { get; set; } 
        public string CompanyName { get; set; } = string.Empty;
        public int BillingID { get; set; }
        public bool IsDeactivated { get; set; } = false;
        public bool IsForceOnNet { get; set; } = false;
        public bool IsCompanyCallsOnly { get; set; } = false;
        public string OutgoingCallBarringList { get; set; } = string.Empty;
        public string OutgoingCallAllowanceList { get; set; } = string.Empty;
    }
}
