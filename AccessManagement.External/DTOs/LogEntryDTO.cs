using System;

namespace AccessManagement.External.DTOs
{
    public class LogEntryDTO
    {
        public int LogId { get; set; }
        public string Type { get; set; }
        public int AccessPointId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
