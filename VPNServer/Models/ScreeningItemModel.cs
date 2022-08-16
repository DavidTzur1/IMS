namespace VPNServer.Models
{
    public class ScreeningItemModel
    {
        public int ScreeningId { get; set; }
        public string ScreeningName { get; set; } = string.Empty;
        public int CompanyId { get; set; }
        public string Number { get; set; } = string.Empty;
    }
}
