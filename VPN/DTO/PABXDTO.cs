namespace VPN.DTO
{
    public class PABXDTO
    {
        public string Name { get; set; } = string.Empty;
        public int GroupID { get; set; }
        public bool IsDeactivated { get; set; } = false;
        public bool IsForceOnNet { get; set; } = false;
        public bool IsCompanyCallsOnly { get; set; } = false;
        public string BarringList { get; set; } = string.Empty;
        public string AllowanceList { get; set; } = string.Empty;
    }
}
