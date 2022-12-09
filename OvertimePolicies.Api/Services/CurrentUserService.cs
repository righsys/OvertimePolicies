using OvertimePolicies.Services.Interfaces;

namespace OvertimePolicies.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            Username = "GustUser";// httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
            IsAuthenticated = Username != null;
        }
        public string Username { get; }
        public bool IsAuthenticated { get; }
    }
}
