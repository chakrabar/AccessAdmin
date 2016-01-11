using AccessManagement.External.DTOs;
using System.Collections.Generic;

namespace AccessManagement.External.Contracts
{
    public interface ILdapService
    {
        IEnumerable<SiteDTO> GetSites();
        IEnumerable<DepartmentDTO> GetDepartments();
        IEnumerable<EmployeeLdapDTO> GetEmployees();
        IEnumerable<UserGroupDTO> GetGroups();
        IEnumerable<DepartmentManagerDTO> GetDepartmentManagers();
    }
}
