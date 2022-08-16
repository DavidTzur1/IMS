namespace VPNServer.Entities
{
    public class Screening
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CompanyId { get; set; }
    }
}
