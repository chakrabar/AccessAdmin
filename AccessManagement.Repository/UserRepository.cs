using AccessManagement.Infrastructure.Database;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AccessManagement.Repository
{
    public class UserRepository : IUserRepository
    {
        public KeyValuePair<int, string> IsValidUser(string email, string password)
        {
            string qry = @"SELECT Name, Id FROM Employees WHERE LOWER(Email) = LOWER(@email) AND [Password] = @password COLLATE Latin1_General_CS_AS";

            using (var conn = SqlHelper.GetConnection())
            {
                SqlCommand cmd = CommandFactory.CreateSimpleTextCommand(conn, qry);
                SqlParameter unParam = cmd.Parameters.AddWithValue("@email", email);
                SqlParameter psParam = cmd.Parameters.AddWithValue("@password", password);

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new KeyValuePair<int, string>(int.Parse(reader["Id"].ToString()), reader["Name"].ToString());
                }
            }
            return new KeyValuePair<int, string>();
        }

        public KeyValuePair<bool, string> IsManager(int employeeId)
        {
            string qry = @"SELECT d.Name FROM DepartmentManagers dm
                            JOIN Departments d ON d.Id = dm.DepartmentId
                            WHERE dm.ManagerId = @empId";
            //var employees = new List<Employee>();
            using (var conn = SqlHelper.GetConnection())
            {
                SqlCommand cmd = CommandFactory.CreateSimpleTextCommand(conn, qry);
                SqlParameter unParam = cmd.Parameters.AddWithValue("@empId", employeeId);

                var reader = cmd.ExecuteScalar();
                if (reader != null)
                {
                    return new KeyValuePair<bool, string>(true, reader.ToString());
                }
            }
            return new KeyValuePair<bool, string>();
        }
    }
}
