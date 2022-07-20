using VPN.Services;

namespace VPN.Models
{
    public class VPNServiceModel
    {
        public string CallID { get; set; } = string.Empty;
        public string Method { get; set; } = string.Empty;
        public string CLI { get; set; } = string.Empty;
        public string DN { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string EndReason { get; set; } = string.Empty;
        public ScreeningDataModel ScreeningDataBaring { get; set; } = new ScreeningDataModel();
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



        public override string ToString()
        {
            return $"{Method}|{CallID}|{CLI}|{DN}|{DNTranslation}|{Status}|{EndReason}|{ScreeningDataBaring.ScreeningID}.{ScreeningDataBaring.ScreeningName}.{ScreeningDataBaring.Number}";
        }
    }
}
