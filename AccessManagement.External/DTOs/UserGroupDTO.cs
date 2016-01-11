using System.Collections.Generic;

namespace AccessManagement.External.DTOs
{
    public class UserGroupDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> MemberLogins { get; set; } //Logins
    }
}
