using Microsoft.Data.SqlClient;
using RepoDb;

namespace TicksPublisher.Repositories
{
    public class DefinitionRepository : DbRepository<SqlConnection>
    {
        public DefinitionRepository() :
            base("Server=tcp:mipensqlserver.database.windows.net,1433;Initial Catalog=Definition;Persist Security Info=False;User ID=mipen;Password=Password123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
        { }
    }
}
