using AccessManagement.Domain.Enums;

namespace AccessManagement.Domain.Entities
{
    public class Permission
    {
        public AccessPoint Entry { get; set; }
        public AccessLevel Access { get; set; }
    }
}
