using AccessManagement.Domain.DTOs;
using System.Collections.Generic;

namespace AccessManagement.Application.Processors
{
    public interface IActivityReportProcessor
    {
        void Process(IEnumerable<DeptWiseActivityDTO> activities);
    }
}
