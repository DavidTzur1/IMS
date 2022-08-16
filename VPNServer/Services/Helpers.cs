using System.Text;

namespace VPNServer.Services
{
    public class Helpers
    {
        public static string NationalFormat(string phone)
        {
            string str = string.Empty;
            if (phone.Length < 9)
            {
                return phone;
            }
            else if (phone.StartsWith("+972"))
            {
                return "0" + String.Join(String.Empty, phone.Skip(4));
            }

            else if (phone.StartsWith("00972"))
            {
                return "0" + String.Join(String.Empty, phone.Skip(5));
            }
            else if (!phone.StartsWith("0"))
                return "0" + phone;
            else
                return phone;
        }
        public static string InternationalFormat(string phone)
        {
            string str = string.Empty;
            if (phone.Length < 9)
            {
                return phone;
            }
            else if (phone.StartsWith("+") || phone.StartsWith("00"))
            {
                return phone;
            }
            else if (phone.StartsWith("0"))
            {
                return "+972" + String.Join(String.Empty, phone.Skip(1));
            }
            else
            {
                return "+972" + phone;
            }
        }
        public static string GetArpaFromEnum(string tel)
        {
            StringBuilder sb = new StringBuilder();
            string Number = System.Text.RegularExpressions.Regex.Replace(tel, "[^0-9]", "");
            sb.Append("e164.arpa.");
            foreach (char c in Number)
            {
                sb.Insert(0, string.Format("{0}.", c));
            }
            return sb.ToString();
        }
        public static string FindTextBetween(string text, string left, string right)
        {
            // TODO: Validate input arguments
            int beginIndex = text.IndexOf(left); // find occurence of left delimiter
            if (beginIndex == -1)
                return string.Empty; // or throw exception?
            beginIndex += left.Length;
            int endIndex = text.IndexOf(right, beginIndex); // find occurence of right delimiter
            if (endIndex == -1)
                return string.Empty; // or throw exception?
            return text.Substring(beginIndex, endIndex - beginIndex).Trim();
        }
    }
}
