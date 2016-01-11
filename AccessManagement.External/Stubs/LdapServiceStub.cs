using AccessManagement.External.Contracts;
using AccessManagement.External.DTOs;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace AccessManagement.External.Stubs
{
    public class LdapServiceStub : ILdapService
    {
        private readonly string EmployeeRosterPath;
        private readonly string DepartmentRosterPath;
        private readonly string SiteRosterPath;
        private readonly string GroupRosterPath;
        private readonly string DeptManagerRosterPath;

        public LdapServiceStub() //why so many things are hard coded? well, its a stub.
        {
            EmployeeRosterPath = ConfigurationManager.AppSettings["EmployeeRosterPath"];
            DepartmentRosterPath = ConfigurationManager.AppSettings["DepartmentRosterPath"];
            SiteRosterPath = ConfigurationManager.AppSettings["SiteRosterPath"];
            GroupRosterPath = ConfigurationManager.AppSettings["GroupRosterPath"];
            DeptManagerRosterPath = ConfigurationManager.AppSettings["DeptManagerRosterPath"];
        }

        public IEnumerable<SiteDTO> GetSites()
        {
            return File.ReadAllLines(SiteRosterPath)
                        .Skip(1)
                        .Select(line => line.Split(','))
                        .Select(parts => new SiteDTO { Id = int.Parse(parts[0]), Name = parts[1] });
        }

        public IEnumerable<DepartmentDTO> GetDepartments()
        {
            return File.ReadAllLines(DepartmentRosterPath)
                        .Skip(1)
                        .Select(line => line.Split(','))
                        .Select(parts => new DepartmentDTO { Id = int.Parse(parts[0]), Name = parts[1] });
        }

        public IEnumerable<EmployeeLdapDTO> GetEmployees()
        {
            return File.ReadAllLines(EmployeeRosterPath)
                        .Skip(1)
                        .Select(line => line.Split(','))
                        .Select(parts => new EmployeeLdapDTO { Name = parts[0], Email = parts[1], Login = int.Parse(parts[2]), Password = parts[3], Domain = parts[4], DepartmentId = int.Parse(parts[5]) });
        }

        public IEnumerable<UserGroupDTO> GetGroups()
        {
            return File.ReadAllLines(GroupRosterPath)
                        .Skip(1)
                        .Select(line => line.Split(','))
                        .Select(parts => new { GroupId = int.Parse(parts[0]), Name = parts[1], MemberLogin = int.Parse(parts[2]) })
                        .GroupBy(groupModel => groupModel.GroupId)
                        .Select(grp => new UserGroupDTO { Id = grp.Key, Name = grp.First().Name, MemberLogins = grp.Select(g => g.MemberLogin).ToList() });
        }


        public IEnumerable<DepartmentManagerDTO> GetDepartmentManagers()
        {
            return File.ReadAllLines(DeptManagerRosterPath)
                        .Skip(1)
                        .Select(line => line.Split(','))
                        .Select(parts => new DepartmentManagerDTO { DepartmentId = int.Parse(parts[0]), ManagerLogin = int.Parse(parts[1]) });
        }
    }
}
