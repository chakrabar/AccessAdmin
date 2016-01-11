using AccessManagement.External.DTOs;

namespace AccessManagement.CommandStack.DataService
{
    public interface IAccessPointDataService
    {
        int InsertAccessPointPermission(UserAccessDTO accessDetails);
    }
}
