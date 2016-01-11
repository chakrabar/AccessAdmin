using System.Data;
using System.Data.SqlClient;

namespace AccessManagement.Infrastructure.Database
{
    public class CommandFactory
    {
        public static SqlCommand CreateSPCommand(DataTable dt, SqlConnection conn, string spName, string paramName)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = spName;
            SqlParameter param = cmd.Parameters.AddWithValue(paramName, dt);
            return cmd;
        }

        public static SqlCommand CreateSimpleTextCommand(SqlConnection conn, string command)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = command;
            return cmd;
        }
    }
}
