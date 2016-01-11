using System;
using System.Collections.Generic;

namespace AccessManagement.Domain.Entities
{
    public class Department : Entity
    {
        public string Name { get; set; }
        public Employee Manager { get; set; }
    }
}
