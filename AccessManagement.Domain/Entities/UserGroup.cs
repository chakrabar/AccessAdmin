using System.Collections.Generic;

namespace AccessManagement.Domain.Entities
{
    public class UserGroup : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Employee> Members { get; set; }
    }
}
