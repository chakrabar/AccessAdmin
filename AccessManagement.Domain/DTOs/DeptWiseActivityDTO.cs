using System.Collections.Generic;

namespace AccessManagement.Domain.DTOs
{
    public class DeptWiseActivityDTO
    {
        public string Department { get; set; }
        public string ManagerName { get; set; }
        public string ManagerEmail { get; set; }
        public List<ActivityReportDTO> ActivityLogs { get; set; }
    }
}
