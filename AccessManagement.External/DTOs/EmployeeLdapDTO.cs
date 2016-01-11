using System;

namespace AccessManagement.External.DTOs
{
    public class EmployeeLdapDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Login { get; set; }
        public string Password { get; set; }
        public string Domain { get; set; }
        public int DepartmentId { get; set; }
    }
}
