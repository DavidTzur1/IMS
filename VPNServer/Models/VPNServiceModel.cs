using VPNServer.Services;

namespace VPNServer.Models
{
    public class VPNServiceModel
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string CallId { get; set; } = string.Empty;
        public string Method { get; set; } = string.Empty;
        public string CLI { get; set; } = string.Empty;
        public string DN { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        public string DNTranslation { get; set; } = string.Empty;
        public string NationalCLI
        {
            get => Helpers.NationalFormat(CLI);
        }

        public string InternationalCLI
        {
            get => Helpers.InternationalFormat(CLI);
        }

        public string NationalDN
        {
            get => Helpers.NationalFormat(DN);
        }

        public string InternationalDN
        {
            get => Helpers.InternationalFormat(DN);
        }

        public string NationalDNTranslation
        {
            get => Helpers.NationalFormat(DNTranslation);
        }

        public string InternationalDNTranslation
        {
            get => Helpers.InternationalFormat(DNTranslation);
        }

        public string Info { get; set; } = string.Empty;



        public override string ToString()
        {
            return $"{CompanyId}|{CompanyName}|{Method}|{CallId}|{CLI}|{DN}|{DNTranslation}|{Status}|{Info}";
        }
    }
}
