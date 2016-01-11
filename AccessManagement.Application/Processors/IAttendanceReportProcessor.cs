using AccessManagement.Domain.DTOs;
using System.Collections.Generic;

namespace AccessManagement.Application.Processors
{
    public interface IAttendanceReportProcessor
    {
        void Process(IEnumerable<DeptWiseAttendanceDTO> attendanceLogs);
    }
}
