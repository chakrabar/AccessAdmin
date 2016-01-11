using AccessManagement.Domain.DTOs;
using AccessManagement.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;

namespace AccessManagement.Application.Processors
{
    public class AttendanceReportProcessor : IAttendanceReportProcessor
    {
        public void Process(IEnumerable<DeptWiseAttendanceDTO> attendanceLogs)
        {
            foreach (var dept in attendanceLogs)
            {
                DispatchDepartmentAttendenceReport(dept);
            }
        }

        //to be mailed in real
        void DispatchDepartmentAttendenceReport(DeptWiseAttendanceDTO logs)
        {
            var filename = string.Format("AttendenceReport_{0}_{1}.txt", logs.DepartmentName, DateTime.Now.ToString("yyyyMMdd_hhmmss"));
            var logfilepath = Path.Combine(ConfigHelper.GetHRSharedDrivePath(), filename);

            var report = new List<string>();
            report.Add("Department : " + logs.DepartmentName);
            report.Add("Date : " + DateTime.Now.ToLongDateString());
            report.Add("==========================================");
            foreach (var log in logs.Attendance)
            {
                report.Add(log.ToString());
            }

            File.WriteAllLines(logfilepath, report);
        }
    }
}
