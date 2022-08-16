namespace VPNServer.Dto
{
    public class PABXForCreationDto
    {
        public string Name { get; set; } = string.Empty;
        public int GroupId { get; set; }
        public bool IsDeactivated { get; set; } = false;
        public bool IsForceOnNet { get; set; } = false;
        public bool IsCompanyCallsOnly { get; set; } = false;
        public string BarringList { get; set; } = string.Empty;
        public string AllowanceList { get; set; } = string.Empty;
    }
}
