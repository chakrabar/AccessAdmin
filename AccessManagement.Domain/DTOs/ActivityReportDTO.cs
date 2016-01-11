
namespace AccessManagement.Domain.DTOs
{
    public class ActivityReportDTO
    {
        public string Employee { get; set; }
        public string AccessPoint { get; set; }
        public string Facility { get; set; }
        public string AccesType { get; set; }
        public string TimeStamp { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - access point [{1}] of facility [{2}] : Employee [{3}], at {4}", AccesType, AccessPoint, Facility, Employee, TimeStamp);
        }
    }
}
