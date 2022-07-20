namespace VPN.Models
{
    public class UserModel
    {
        public int CompanyID { get; set; } = -1;
        public int GroupID { get; set; }
        public int PABXID { get; set; }
        public string CLI { get; set; } = string.Empty;
        public bool IsDeactivated { get; set; } = false;
        public bool IsCompanyCallsOnly { get; set; } = false;
        public string PrivateNumber { get; set; } = string.Empty;
        public string CalledNumber { get; set; } = string.Empty;
        public string ChargingNumber { get; set; } = string.Empty;
        public bool IsBarred { get; set; } = false;

        private string outgoingCallBarringList = string.Empty;
        public string OutgoingCallBarringList
        {
            get => string.Join(",", outgoingCallBarringList.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
            set => outgoingCallBarringList = value;
        }

        private string outgoingCallAllowanceList = string.Empty;
        public string OutgoingCallAllowanceList
        {
            get => string.Join(",", outgoingCallAllowanceList.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
            set => outgoingCallAllowanceList = value;
        }
    }
}
