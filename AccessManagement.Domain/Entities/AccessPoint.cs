
namespace AccessManagement.Domain.Entities
{
    public class AccessPoint : Entity
    {
        public string Name { get; set; }
        public Site Facility { get; set; }
    }
}
