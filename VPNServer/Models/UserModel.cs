namespace VPNServer.Models
{
    public class UserModel
    {
        public int CompanyId { get; set; } = -1;
        public string CompanyName { get; set; } = string.Empty;
        public int GroupId { get; set; }
        public int PABXId { get; set; }
        public string CLI { get; set; } = string.Empty;
        public bool IsDeactivated { get; set; } = false;
        public bool IsCompanyCallsOnly { get; set; } = false;
        public bool IsForceOnNet { get; set; } = false;
        public bool IsVirtualOnNet { get; set; } = false;
        public string PrivateNumber { get; set; } = string.Empty;
        public string CalledNumber { get; set; } = string.Empty;
        public string ChargingNumber { get; set; } = string.Empty;
        public bool IsBarred { get; set; } = false;

        private string barringList = string.Empty;
        public string BarringList
        {
            get => string.Join(",", barringList.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
            set => barringList = value;
        }

        private string allowanceList = string.Empty;
        public string AllowanceList
        {
            get => string.Join(",", allowanceList.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
            set => allowanceList = value;
        }
    }
}
