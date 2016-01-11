using AccessManagement.Domain.Entities;
using AccessManagement.Domain.Logs;

namespace AccessManagement.Domain.DTOs
{
    public class ActivityLogDTO : LogEntry
    {
        public Department Department { get; set; }
    }
}
