using AccessManagement.Domain.DTOs;
using AccessManagement.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;

namespace AccessManagement.Application.Processors
{
    public class ActivityReportProcessor : IActivityReportProcessor
    {
        public void Process(IEnumerable<DeptWiseActivityDTO> activities)
        {
            foreach (var dept in activities)
            {
                DispatchDepartmentActivityReport(dept);
            }
        }

        //to be mailed in real
        void DispatchDepartmentActivityReport(DeptWiseActivityDTO activity)
        {
            var filename = string.Format("ActivityLog_{0}_{1}.txt", activity.Department, DateTime.Now.ToString("yyyyMMdd_hhmmss"));
            var logfilepath = Path.Combine(ConfigHelper.GetLogPath(), filename);

            var report = new List<string>();
            report.Add("Department : " + activity.Department);
            report.Add("Date : " + DateTime.Now.ToLongDateString());
            report.Add("Manager : " + activity.ManagerName);
            report.Add("Email : " + activity.ManagerEmail);
            report.Add("==========================================");
            foreach (var log in activity.ActivityLogs)
            {
                report.Add(log.ToString());
            }

            File.WriteAllLines(logfilepath, report);
        }
    }
}
