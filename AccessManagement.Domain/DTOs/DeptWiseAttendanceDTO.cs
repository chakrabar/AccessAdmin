using System.Collections.Generic;

namespace AccessManagement.Domain.DTOs
{
    public class DeptWiseAttendanceDTO
    {
        public string DepartmentName { get; set; }
        public List<AttendanceDTO> Attendance { get; set; }
    }
}
