using Microsoft.Data.SqlClient;
using RepoDb;

namespace TicksReceiver.Repositories
{
    public class ProductionRepository : DbRepository<SqlConnection>
    {
        public ProductionRepository() :
            base("Server=tcp:mipensqlserver.database.windows.net,1433;Initial Catalog=Definition;Persist Security Info=False;User ID=mipen;Password=Password123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
        { }
    }
}
