namespace VPNServer.Dto
{
    public class RangeForUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public int PABXId { get; set; }
        public string PublicRange { get; set; } = string.Empty;
        public string PrivateRange { get; set; } = string.Empty;
        public int PublicToPrivateDigitsRemove { get; set; }
        public string PublicToPrivatePrefixAdd { get; set; } = string.Empty;
        public int PrivateToDestDigitsRemove { get; set; }
        public string PrivateToDestPrefixAdd { get; set; } = string.Empty;
        public int PrivateToChargingDigitsRemove { get; set; }
        public string PrivateToChargingPrefixAdd { get; set; } = string.Empty;
        public bool IsVirtualOnNet { get; set; } = false;
    }
}
