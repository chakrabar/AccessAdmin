using AccessManagement.External.DTOs;
using System.Collections.Generic;

namespace AccessManagement.CommandStack.DataService
{
    public interface IInsertDataService
    {
        int InsertSitesData(IEnumerable<SiteDTO> siteData);
        int InsertDepartmentsData(IEnumerable<DepartmentDTO> departments);
        int InsertEmployeesData(IEnumerable<EmployeeLdapDTO> employees);
        int InsertGroupsData(IEnumerable<UserGroupDTO> userGroups);
        int InsertGroupMappings(IEnumerable<GroupMappingDTO> userGroups);
        int InsertDeptManagers(IEnumerable<DepartmentManagerDTO> userGroups);
        int InsertAccessPoints(IEnumerable<AccessPointDTO> accessPoints);
    }
}
