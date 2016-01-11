using AccessManagement.External.DTOs;
using AccessManagement.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AccessManagement.CommandStack.DataService
{
    public class InsertDataService : IInsertDataService //TODO: remove duplication
    {
        public int InsertSitesData(IEnumerable<SiteDTO> siteData)
        {
            var effectedRows = 0;
            try
            {
                DataTable dt = GetIdNameTable(); //check
                foreach (var site in siteData)
                {
                    DataRow dr = dt.NewRow();
                    dr["Id"] = site.Id;
                    dr["Name"] = site.Name;
                    dt.Rows.Add(dr);
                }

                using (var conn = SqlHelper.GetConnection())
                {
                    SqlCommand cmd = CommandFactory.CreateSPCommand(dt, conn, "dbo.InsertSites", "@sites");
                    effectedRows = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                //log later
            }
            return effectedRows;
        }        

        public int InsertDepartmentsData(IEnumerable<DepartmentDTO> departments)
        {
            var effectedRows = 0;
            try
            {
                DataTable dt = GetIdNameTable(); //check
                foreach (var dept in departments)
                {
                    DataRow dr = dt.NewRow();
                    dr["Id"] = dept.Id;
                    dr["Name"] = dept.Name;
                    dt.Rows.Add(dr);
                }

                using (var conn = SqlHelper.GetConnection())
                {
                    SqlCommand cmd = CommandFactory.CreateSPCommand(dt, conn, "dbo.InsertDepartments", "@departments");
                    effectedRows = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                //log later
            }
            return effectedRows;
        }

        public int InsertEmployeesData(IEnumerable<EmployeeLdapDTO> employees)
        {
            var effectedRows = 0;
            try
            {
                DataTable dt = GetEmployeeDataTable(); //check
                foreach (var emp in employees)
                {
                    DataRow dr = dt.NewRow();
                    dr["Id"] = emp.Login;
                    dr["Name"] = emp.Name;
                    dr["Email"] = emp.Email;
                    //dr["Login"] = emp.Login;
                    dr["Password"] = emp.Password;
                    dr["Domain"] = emp.Domain;
                    dr["DepartmentId"] = emp.DepartmentId;
                    dt.Rows.Add(dr);
                }

                using (var conn = SqlHelper.GetConnection())
                {
                    SqlCommand cmd = CommandFactory.CreateSPCommand(dt, conn, "dbo.InsertEmployees", "@employees");
                    effectedRows = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                //log later
            }
            return effectedRows;
        }

        public int InsertGroupsData(IEnumerable<UserGroupDTO> userGroups)
        {
            var effectedRows = 0;
            try
            {
                DataTable dt = GetIdNameTable(); //check
                foreach (var ug in userGroups)
                {
                    DataRow dr = dt.NewRow();
                    dr["Id"] = ug.Id;
                    dr["Name"] = ug.Name;
                    dt.Rows.Add(dr);
                }

                using (var conn = SqlHelper.GetConnection())
                {
                    SqlCommand cmd = CommandFactory.CreateSPCommand(dt, conn, "dbo.InsertUserGroups", "@userGroups");
                    effectedRows = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                //log later
            }
            return effectedRows;
        }

        public int InsertGroupMappings(IEnumerable<GroupMappingDTO> userGroups)
        {
            var effectedRows = 0;
            try
            {
                DataTable dt = GetMappingTable(); //check
                foreach (var ug in userGroups)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = ug.GroupId;
                    dr[1] = ug.EmployeeLogin;
                    dt.Rows.Add(dr);
                }

                using (var conn = SqlHelper.GetConnection())
                {
                    SqlCommand cmd = CommandFactory.CreateSPCommand(dt, conn, "dbo.InsertGroupMappings", "@mappings");
                    effectedRows = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                //log later
            }
            return effectedRows;
        }

        public int InsertDeptManagers(IEnumerable<DepartmentManagerDTO> userGroups)
        {
            var effectedRows = 0;
            try
            {
                DataTable dt = GetMappingTable(); //check
                foreach (var ug in userGroups)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = ug.DepartmentId;
                    dr[1] = ug.ManagerLogin;
                    dt.Rows.Add(dr);
                }

                using (var conn = SqlHelper.GetConnection())
                {
                    SqlCommand cmd = CommandFactory.CreateSPCommand(dt, conn, "dbo.InsertDepartmentManagers", "@mappings");
                    effectedRows = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                //log later
            }
            return effectedRows;
        }

        public int InsertAccessPoints(IEnumerable<AccessPointDTO> accessPoints)
        {
            var effectedRows = 0;
            try
            {
                DataTable dt = GetSubTable();
                foreach (var ap in accessPoints)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = ap.Id;
                    dr[1] = ap.Name;
                    dr[2] = ap.SiteId;
                    dt.Rows.Add(dr);
                }

                using (var conn = SqlHelper.GetConnection())
                {
                    SqlCommand cmd = CommandFactory.CreateSPCommand(dt, conn, "dbo.InsertAccessPoints", "@accessPoints");
                    effectedRows = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                //log later
            }
            return effectedRows;
        }

        private DataTable GetEmployeeDataTable()
        {
            DataTable dataTable = new DataTable("tbl");
            dataTable.Columns.Add("Id", typeof(int)); //Login
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Email", typeof(string));
            //dataTable.Columns.Add("Login", typeof(int));
            dataTable.Columns.Add("Password", typeof(string));
            dataTable.Columns.Add("Domain", typeof(string));
            dataTable.Columns.Add("DepartmentId", typeof(int));
            return dataTable;
        }

        private DataTable GetIdNameTable()
        {
            DataTable dataTable = new DataTable("SiteType");
            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("Name", typeof(string));
            return dataTable;
        }

        private DataTable GetMappingTable()
        {
            DataTable dataTable = new DataTable("MappingsTable");
            dataTable.Columns.Add("FK1", typeof(int));
            dataTable.Columns.Add("FK2", typeof(int));
            return dataTable;
        }

        private DataTable GetSubTable()
        {
            DataTable dataTable = new DataTable("SecondaryTable");
            dataTable.Columns.Add("FK1", typeof(int));
            dataTable.Columns.Add("FK2", typeof(string));
            dataTable.Columns.Add("FK3", typeof(int));
            return dataTable;
        }
    }
}
