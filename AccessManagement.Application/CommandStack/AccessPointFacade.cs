using AccessManagement.CommandStack.DataService;
using AccessManagement.Domain.Enums;
using AccessManagement.External.Contracts;
using AccessManagement.External.DTOs;
using Microsoft.Practices.Unity;
using System;

namespace AccessManagement.Application.CommandStack
{
    public class AccessPointFacade : IAccessPointFacade
    {
        [Dependency]
        public IAccessPointService AccessPointService { get; set; }
        [Dependency]
        public IInsertDataService DataService { get; set; }
        [Dependency]
        public IAccessPointDataService AccessPointDataService { get; set; }

        public void SyncAccessPoints()
        {
            var accessPoints = AccessPointService.GetAccessPoints();
            var accessPointsInserted = DataService.InsertAccessPoints(accessPoints);
        }

        public bool InsertUserAccess(string empId, string accessPointId, string accesslevel)
        {
            try
            {
                AccessLevel level = AccessLevel.None;
                if (accesslevel.ToLower().Contains("access") || accesslevel.ToLower().Contains("allow"))
                    level = AccessLevel.Access;
                else if (accesslevel.ToLower().Contains("manage"))
                    level = AccessLevel.Manage;
                else if (accesslevel.ToLower().Contains("monitor"))
                    level = AccessLevel.Monitor;

                UserAccessDTO dto = new UserAccessDTO { AccessLevelId = (int)level, AccessPointId = int.Parse(accessPointId), EmployeeId = int.Parse(empId) };
                return AccessPointDataService.InsertAccessPointPermission(dto) > 0;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
