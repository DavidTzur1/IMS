using System.ComponentModel.DataAnnotations;

namespace VPNServer.Dto
{
    public class UserForUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public int PABXId { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "CLI must be numeric")]
        public string CLI { get; set; } = string.Empty;
        public bool IsDeactivated { get; set; } = false;
        public bool IsCompanyCallsOnly { get; set; } = false;
        public bool IsVirtualOnNet { get; set; } = false;
        public string PrivateNumber { get; set; } = string.Empty;
        public string CalledNumber { get; set; } = string.Empty;
        public string ChargingNumber { get; set; } = string.Empty;
        public string BarringList { get; set; } = string.Empty;
        public string AllowanceList { get; set; } = string.Empty;
    }
}
