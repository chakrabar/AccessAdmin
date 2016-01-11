using AccessManagement.Application.CommandStack;
using AccessManagement.Application.Processors;
using AccessManagement.Application.ReportGenerators;
using AccessManagement.CommandStack.DataService;
using AccessManagement.Domain.Entities;
using AccessManagement.External.Contracts;
using AccessManagement.External.Stubs;
using AccessManagement.Repository;
using Microsoft.Practices.Unity;
using System;

namespace AccessManagement.Console
{
    class Initializer
    {
        internal static IUnityContainer Container;

        internal static void Bootstrap()
        {
            try
            {
                Container = new UnityContainer();
                ConfigureInjector();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Application failed to initialize!");
            }
        }

        static void ConfigureInjector()
        {
            Container.RegisterType<ILdapService, LdapServiceStub>();
            Container.RegisterType<IInsertDataService, InsertDataService>();
            //Container.RegisterType<ISqlHelper, SqlHelper>();
            Container.RegisterType<ILdapSyncFacade, LdapSyncFacade>();
            Container.RegisterType<IAccessPointService, AccessServiceStub>();
            Container.RegisterType<IAccessPointFacade, AccessPointFacade>();
            Container.RegisterType<IReportGeneratorFacade, ReportGeneratorFacade>();
            Container.RegisterType<IRepository<Employee>, EmployeeRepository>();
            Container.RegisterType<IRepository<AccessPoint>, AccessPointRepository>();
            Container.RegisterType<IActivityReportProcessor, ActivityReportProcessor>();
            Container.RegisterType<IAttendanceReportProcessor, AttendanceReportProcessor>();
            Container.RegisterType<IUserRepository, UserRepository>();
            Container.RegisterType<IAccessPointDataService, AccessPointDataService>();
        }
    }
}
