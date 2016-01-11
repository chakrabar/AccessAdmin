
namespace AccessManagement.Domain.DTOs
{
    public class AttendanceDTO
    {
        public string EmployeeName { get; set; }
        public int EmployeeId { get; set; }
        public bool Attended { get; set; }
        public string Date { get; set; }

        public override string ToString()
        {
            return string.Format("Employee [{0}] - {1} on {2}. Name : {3}", EmployeeId, Attended ? "Prsent" : "Absent", Date, EmployeeName);
        }
    }
}
