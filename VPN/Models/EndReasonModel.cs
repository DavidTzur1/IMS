namespace VPN.Models
{
    public class EndReasonModel
    {
        public static string Success { get; } = "Success";
        public static string UserNotFound { get; } = "UsrNotFound";
        public static string UserDeactivated { get; } = "UserDeactivated";
        public static string OutgoingCallBarring { get; } = "OutgoingCallBarring";
        public static string NotImplemented { get; } = "NotImplemented";
    }
}
