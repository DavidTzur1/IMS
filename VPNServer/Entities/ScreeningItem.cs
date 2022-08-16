namespace VPNServer.Entities
{
    public class ScreeningItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ScreeningId { get; set; }
        public string Number { get; set; } = string.Empty;
    }
}
