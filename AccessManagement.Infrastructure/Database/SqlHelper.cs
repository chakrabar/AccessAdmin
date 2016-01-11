using System.Configuration;
using System.Data.SqlClient;

namespace AccessManagement.Infrastructure.Database
{
    public class SqlHelper
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["UserAccess"].ConnectionString;
        }

        public static SqlConnection GetConnection()
        {
            var connection = new SqlConnection(GetConnectionString());
            connection.Open();

            return connection;
        }
    }
}
