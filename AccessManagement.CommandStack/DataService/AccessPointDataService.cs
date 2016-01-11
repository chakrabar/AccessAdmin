using AccessManagement.External.DTOs;
using AccessManagement.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagement.CommandStack.DataService
{
    public class AccessPointDataService : IAccessPointDataService
    {
        public int InsertAccessPointPermission(UserAccessDTO accessDetails)
        {
            const string command = @"INSERT INTO UserPermissions (EmployeeId, AccessPointId, AccessTypeId)
                                        VALUES(@empId, @accessId, @accessTypeId)";

            var effectedRows = 0;
            try
            {
                using (var conn = SqlHelper.GetConnection())
                {
                    SqlCommand cmd = CommandFactory.CreateSimpleTextCommand(conn, command);
                    SqlParameter unParam = cmd.Parameters.AddWithValue("@empId", accessDetails.EmployeeId);
                    SqlParameter psParam = cmd.Parameters.AddWithValue("@accessId", accessDetails.AccessPointId);
                    SqlParameter atParam = cmd.Parameters.AddWithValue("@accessTypeId", accessDetails.AccessLevelId);

                    effectedRows = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                //log later
            }
            return effectedRows;
        }
    }
}
