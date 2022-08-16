namespace VPNServer.Models
{
    public class RangeModel
    {
        public int Id { get; set; }
        public int CompanyId{ get; set; } = -1;
        public int GroupId { get; set; }
        public int PABXId { get; set; }
        public string PublicRange { get; set; } = string.Empty;
        public string PrivateRange { get; set; } = string.Empty;
        public bool IsForceOnNet { get; set; } = false;
        public int PublicToPrivateDigitsRemove { get; set; }
        public string PublicToPrivatePrefixAdd { get; set; } = string.Empty;
        public int PrivateToDestDigitsRemove { get; set; }
        public string PrivateToDestPrefixAdd { get; set; } = string.Empty;
        public int PrivateToChargingDigitsRemove { get; set; }
        public string PrivateToChargingPrefixAdd { get; set; } = string.Empty;
    }
}
