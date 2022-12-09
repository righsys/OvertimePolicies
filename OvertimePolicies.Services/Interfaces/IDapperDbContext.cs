using System.Data;

namespace OvertimePolicies.Services.Interfaces
{
    public interface IDapperDbContext
    {
        IDbConnection CreateConnection();
    }
}
