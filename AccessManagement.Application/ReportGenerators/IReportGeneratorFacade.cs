using System;

namespace AccessManagement.Application.ReportGenerators
{
    public interface IReportGeneratorFacade
    {
        void GenerateCurrentReports();
        void GenerateActivityLogs(DateTime date);
    }
}
