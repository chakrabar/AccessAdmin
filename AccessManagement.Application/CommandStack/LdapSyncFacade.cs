using AccessManagement.CommandStack.DataService;
using AccessManagement.External.Contracts;
using AccessManagement.External.DTOs;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Linq;

namespace AccessManagement.Application.CommandStack
{
    public class LdapSyncFacade : ILdapSyncFacade
    {
        [Dependency]
        public ILdapService LdapService { get; set; }
        [Dependency]
        public IInsertDataService DataService { get; set; }

        public void SyncFromLdap()
        {
            var sites = LdapService.GetSites();
            var sitesAdded = DataService.InsertSitesData(sites);

            var depts = LdapService.GetDepartments();
            var deptsAdded = DataService.InsertDepartmentsData(depts);

            var employees = LdapService.GetEmployees();
            var empsAdded = DataService.InsertEmployeesData(employees);

            var userGroups = LdapService.GetGroups();
            var groupsAdded = DataService.InsertGroupsData(userGroups);

            var userMappings = new List<GroupMappingDTO>();
            userGroups.ToList().ForEach(g =>
            {
                var mappings = g.MemberLogins.Select(m => new GroupMappingDTO { EmployeeLogin = m, GroupId = g.Id });
                userMappings.AddRange(mappings.ToList());
            });
            var mappingsAdded = DataService.InsertGroupMappings(userMappings);

            var deptManagers = LdapService.GetDepartmentManagers();
            var deptMgrsAdded = DataService.InsertDeptManagers(deptManagers);
        }
    }
}
