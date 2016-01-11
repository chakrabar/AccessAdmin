using AccessManagement.External.DTOs;
using System;
using System.Collections.Generic;

namespace AccessManagement.External.Contracts
{
    public interface IAccessPointService
    {
        IEnumerable<AccessPointDTO> GetAccessPoints();
        IEnumerable<LogEntryDTO> GetAccessDetails();
        IEnumerable<LogEntryDTO> GetAccessDetailsSince(DateTime fromTime);
        IEnumerable<LogEntryDTO> GetAccessDetailsFor(int accessPointId);
    }
}
