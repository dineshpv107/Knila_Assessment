using Dapper;
using Microsoft.Data.SqlClient;

namespace Knila.Migrator
{
    public class Migrator
    {
        public Migrator()
        {
                
        }

        public async Task MigratorRun(string connectionString) 
        {
            //SQL Client Nuget packages
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var sql = "CREATE TABLE Users (Id INT PRIMARY KEY IDENTITY(1,1), FirstName NVARCHAR(100), LastName NVARCHAR(100), Email NVARCHAR(150), PhoneNumber NVARCHAR(20), Address NVARCHAR(200), City NVARCHAR(100), State NVARCHAR(100), Country NVARCHAR(100), PostalCode NVARCHAR(20));";
                    var users = await connection.ExecuteAsync(sql);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
