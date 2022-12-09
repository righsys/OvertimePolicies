using OvertimePolicies.Infrastructure.DbContexts;
using Xunit;

namespace OvertimePolicies.Services.Tests.Common
{
    public class QueryTestFixture : IDisposable
    {
        public EFCoreDbContext Context { get; private set; }

        public QueryTestFixture()
        {
            Context = EFCoreContextFactory.CreateEFDbContext();           
        }

        public void Dispose()
        {
            EFCoreContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}