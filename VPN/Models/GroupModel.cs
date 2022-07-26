namespace VPN.Models
{
    public class GroupModel
    {
        public int CompanyID { get; set; }
        public int GroupID { get; set; }
        public string GroupName { get; set; } = string.Empty;
        public bool IsDeactivated { get; set; } = false;
        public bool IsForceOnNet { get; set; } = false;
        public bool IsCompanyCallsOnly { get; set; } = false;
        public string OutgoingCallBarringList { get; set; } = string.Empty;
        public string OutgoingCallAllowanceList { get; set; } = string.Empty;
    }
}
