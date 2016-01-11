using System;

namespace AccessManagement.Application.CommandStack
{
    public interface IAccessPointFacade
    {
        void SyncAccessPoints();
        bool InsertUserAccess(string empId, string accessPointId, string accesslevel);
    }
}
