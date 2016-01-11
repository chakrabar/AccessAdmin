using AccessManagement.Application.Processors;
using AccessManagement.Domain.DTOs;
using AccessManagement.Domain.Entities;
using AccessManagement.Domain.Enums;
using AccessManagement.External.Contracts;
using AccessManagement.External.DTOs;
using AccessManagement.Repository;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AccessManagement.Application.ReportGenerators
{
    public class ReportGeneratorFacade : IReportGeneratorFacade
    {
        [Dependency]
        public IAccessPointService AccessPointService { get; set; }
        [Dependency]
        public IRepository<Employee> EmployeeRepository { get; set; }
        [Dependency]
        public IRepository<AccessPoint> AccessPointRepository { get; set; }
        [Dependency]
        public IActivityReportProcessor ActivityReportProcessor { get; set; }
        [Dependency]
        public IAttendanceReportProcessor AttendanceReportProcessor { get; set; }

        public void GenerateCurrentReports()
        {
            var today = DateTime.Now;
            GenerateActivityLogs(today);
        }

        public void GenerateActivityLogs(DateTime date)
        {
            var logStartTime = GetDayStart(date);
            var logsForTheDay = AccessPointService.GetAccessDetailsSince(logStartTime);
            //save logs to database (need access point, empooyee) => Department, Site

            var employees = EmployeeRepository.Get(); //get from database or ldap?
            var accessPoints = AccessPointRepository.Get(); //ger from DB

            var activityLogs = GetActivityLogs(logsForTheDay, employees, accessPoints);

            //dept wise activity
            var deptWiseActivity = GetDeptWiseActivity(activityLogs);
            ActivityReportProcessor.Process(deptWiseActivity);

            //dept wise attendance
            var deptWiseAttendance = GetdeptWiseAttendance(activityLogs, employees);
            AttendanceReportProcessor.Process(deptWiseAttendance);
        }

        List<DeptWiseAttendanceDTO> GetdeptWiseAttendance(IEnumerable<ActivityLogDTO> activityLogs, IEnumerable<Employee> allEmployees)
        {
            var deptWiseAttendance = activityLogs.GroupBy(al => al.Department.Id)
                                .Select(grp => CreateAttendanceDTO(grp, allEmployees))
                                .ToList();
            return deptWiseAttendance;
        }

        private static DeptWiseAttendanceDTO CreateAttendanceDTO(IGrouping<int, ActivityLogDTO> grp, IEnumerable<Employee> allEmployees)
        {
            var deptMembers = allEmployees.Where(e => e.Deprtment.Id == grp.First().Department.Id);
            var dto = new DeptWiseAttendanceDTO
            {
                DepartmentName = grp.First().Department.Name,
                Attendance = grp.GroupBy(gd => gd.Employee.Id)
                                .Select(empGroup => new AttendanceDTO
                                {
                                    EmployeeName = empGroup.First().Employee.Name,
                                    EmployeeId = empGroup.First().Employee.Id,
                                    Attended = true,
                                    Date = empGroup.First().TimeStamp.ToString("yyyy-MM-dd")
                                })
                                .ToList()
            };
            var absents = deptMembers.Where(m => !dto.Attendance.Any(a => a.EmployeeId == m.Id));
            var date = dto.Attendance.First().Date;
            dto.Attendance.AddRange(absents.Select(a => new AttendanceDTO
                                {
                                    EmployeeName = a.Name,
                                    EmployeeId = a.Id,
                                    Attended = false,
                                    Date = date
                                }));
            return dto;
        }

        List<DeptWiseActivityDTO> GetDeptWiseActivity(IEnumerable<ActivityLogDTO> activityLogs)
        {
            var deptWiseLogs = activityLogs.GroupBy(al => al.Department.Id);
            var deptWiseActivityLogs = deptWiseLogs.Select(grp => new DeptWiseActivityDTO
                                {
                                    Department = grp.First().Department.Name,
                                    ManagerName = grp.First().Department.Manager.Name,
                                    ManagerEmail = grp.First().Department.Manager.Email,
                                    ActivityLogs = grp.Select(g => new ActivityReportDTO
                                    {
                                        AccessPoint = g.AccessPoint.Name,
                                        AccesType = g.Type.ToString(),
                                        Employee = g.Employee.Name,
                                        Facility = g.AccessPoint.Facility.Name,
                                        TimeStamp = g.TimeStamp.ToString("yyyy-MM-dd hh:mm:ss")
                                    })
                                    .ToList()
                                })
                                .ToList();
            return deptWiseActivityLogs;
        }

        IEnumerable<ActivityLogDTO> GetActivityLogs(IEnumerable<LogEntryDTO> logs, IEnumerable<Employee> employees, IEnumerable<AccessPoint> accessPoints)
        {
            var activityLogs = new List<ActivityLogDTO>();
            foreach (var log in logs)
            {
                var employee = employees.FirstOrDefault(e => e.Id == log.EmployeeId);
                if (employees != null)
                {
                    activityLogs.Add(new ActivityLogDTO
                    {
                        Employee = employee,
                        Department = employee.Deprtment,
                        Id = log.LogId,
                        TimeStamp = log.TimeStamp,
                        Type = GetAccessType(log.Type),
                        AccessPoint = accessPoints.FirstOrDefault(a => a.Id == log.AccessPointId)
                    });
                }
            }
            return activityLogs;
        }

        AccessType GetAccessType(string access)
        {
            if (string.IsNullOrEmpty(access))
                return AccessType.None;
            if (access.ToLower().Contains("entry") || access.ToLower().Contains("enter"))
                return AccessType.AccessEntry;
            if (access.ToLower().Contains("exit"))
                return AccessType.AccessExit;
            if (access.ToLower().Contains("deny") || access.ToLower().Contains("denied") || access.ToLower().Contains("stop"))
                return AccessType.AccessDenied;
            return AccessType.None;
        }

        DateTime GetDayStart(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        }
    }
}
