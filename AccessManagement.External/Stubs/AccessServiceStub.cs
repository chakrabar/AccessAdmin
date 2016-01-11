using AccessManagement.External.Contracts;
using AccessManagement.External.DTOs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace AccessManagement.External.Stubs
{
    public class AccessServiceStub : IAccessPointService
    {
        private readonly string AccessMainRosterPath;
        private readonly string DailyAccessRosterPath;

        public AccessServiceStub()
        {
            AccessMainRosterPath = ConfigurationManager.AppSettings["AccessMainRosterPath"];
            DailyAccessRosterPath = ConfigurationManager.AppSettings["DailyAccessRosterPath"];
        }

        public IEnumerable<AccessPointDTO> GetAccessPoints()
        {
            return File.ReadAllLines(AccessMainRosterPath)
                        .Skip(1)
                        .Select(line => line.Split(','))
                        .Select(parts => new AccessPointDTO { Id = int.Parse(parts[0]), Name = parts[1], SiteId = int.Parse(parts[2]) });
        }

        public IEnumerable<LogEntryDTO> GetAccessDetails()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LogEntryDTO> GetAccessDetailsSince(DateTime fromTime)
        {
            var k = File.ReadAllLines(DailyAccessRosterPath)
                        .Skip(1)
                        .Select(line => line.Split(','));

            var l = k.Select(parts => new LogEntryDTO 
            { 
                LogId = int.Parse(parts[0]), 
                Type = parts[1], 
                AccessPointId = int.Parse(parts[2]), 
                EmployeeId = int.Parse(parts[3]), 
                TimeStamp = DateTime.Now //it's a stub dummy
            })
            .ToList();
             return l;
        }

        public IEnumerable<LogEntryDTO> GetAccessDetailsFor(int accessPointId)
        {
            throw new NotImplementedException();
        }
    }
}
