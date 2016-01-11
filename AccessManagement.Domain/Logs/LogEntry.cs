using AccessManagement.Domain.Entities;
using AccessManagement.Domain.Enums;
using System;

namespace AccessManagement.Domain.Logs
{
    public class LogEntry : Entity
    {
        public AccessType Type { get; set; }
        public AccessPoint AccessPoint { get; set; }
        public Employee Employee { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
