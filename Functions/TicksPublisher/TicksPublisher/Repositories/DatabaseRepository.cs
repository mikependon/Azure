using Microsoft.Data.SqlClient;
using RepoDb;

namespace TicksPublisher.Repositories
{
    public class DatabaseRepository : DbRepository<SqlConnection>
    {
        public DatabaseRepository() :
            base("Server=tcp:mipensqlserver.database.windows.net,1433;Initial Catalog=TestDB;Persist Security Info=False;User ID=mipen;Password=Password123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
        { }
    }
}
