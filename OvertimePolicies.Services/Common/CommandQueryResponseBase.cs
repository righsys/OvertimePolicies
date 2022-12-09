namespace OvertimePolicies.Services.Common
{
    public class CommandQueryResponseBase
    {
        public bool Success { get; set; }
        public string CustomErrorMessage { get; set; } = String.Empty;
        public string ExceptionMessage { get; set; } = String.Empty;
    }
}