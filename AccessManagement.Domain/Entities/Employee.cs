using System;
using System.Collections.Generic;

namespace AccessManagement.Domain.Entities
{
    public class Employee : Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Login { get; set; }
        public string Password { get; set; }
        public string Domain { get; set; }
        public Department Deprtment { get; set; } //required
        public List<UserGroup> Groups { get; set; }
        public List<Permission> Permissions { get; set; }
    }
}
