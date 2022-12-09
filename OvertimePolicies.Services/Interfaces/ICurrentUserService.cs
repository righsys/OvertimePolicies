namespace OvertimePolicies.Services.Interfaces
{
    public interface ICurrentUserService
    {
        string Username { get; }
        bool IsAuthenticated { get; }
    }
}
