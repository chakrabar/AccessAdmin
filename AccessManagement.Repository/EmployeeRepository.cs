using AccessManagement.Domain.Entities;
using AccessManagement.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace AccessManagement.Repository
{
    public class EmployeeRepository : IRepository<Employee>
    {
        public IEnumerable<Employee> Get()
        {
            const string command = @"SELECT e.Name, e.Email, e.Id, d.Name AS DeptName, d.Id AS DeptId, dm.ManagerId AS DeptMgrId
                                    FROM Employees e
                                    JOIN Departments d ON d.Id = e.DepartmentId
                                    JOIN DepartmentManagers dm ON dm.DepartmentId = d.Id";
            var employees = new List<Employee>();
            using (var conn = SqlHelper.GetConnection())
            {
                SqlCommand cmd = CommandFactory.CreateSimpleTextCommand(conn, command);
                
                var reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        var emp = new Employee();
                        emp.Name = reader.GetString(reader.GetOrdinal("Name"));
                        emp.Email = reader.GetString(reader.GetOrdinal("Email"));
                        emp.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        emp.Deprtment = new Department { 
                            Id = reader.GetInt32(reader.GetOrdinal("DeptId")), 
                            Name = reader.GetString(reader.GetOrdinal("DeptName")),
                            Manager = new Employee { Id = reader.GetInt32(reader.GetOrdinal("DeptMgrId")) }
                        };
                        employees.Add(emp);
                    }
                }
            }
            foreach (var emp in employees) //TODO: optimize
            {
                var deptMgr = employees.FirstOrDefault(e => e.Id == emp.Deprtment.Manager.Id);
                if (deptMgr != null)
                    emp.Deprtment.Manager = deptMgr;
            }
            return employees;
        }

        public Employee Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
