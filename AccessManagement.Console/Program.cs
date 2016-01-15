using AccessManagement.Application.CommandStack;
using AccessManagement.Application.ReportGenerators;
using AccessManagement.Console.Menus;
using AccessManagement.Domain.Entities;
using AccessManagement.Infrastructure;
using AccessManagement.Repository;
using Microsoft.Practices.Unity;
using System.Collections.Generic;

namespace AccessManagement.Console
{
    class Program
    {
        internal static bool IsUserLoggedIn { get; private set; }
        internal static bool IsUserManager { get; private set; }
        internal static string UserName { get; private set; }
        static int _userId = 0;
        static string _department;

        static IReportGeneratorFacade _ReportGeneratorFacade;
        static ILdapSyncFacade _LdapSyncFacade;
        static IAccessPointFacade _AccessPointFacade;
        static IUserRepository _UserRepository;
        static IRepository<AccessPoint> _AccessPointRepository;

        static void Main(string[] args)
        {
            Initializer.Bootstrap();
            SetupService();
            Start();

            System.Console.WriteLine("Exiting application.");
            System.Console.ReadLine();
        }

        public static void SetupService()
        {
            _ReportGeneratorFacade = Initializer.Container.Resolve<IReportGeneratorFacade>();
            _LdapSyncFacade = Initializer.Container.Resolve<ILdapSyncFacade>();
            _AccessPointFacade = Initializer.Container.Resolve<IAccessPointFacade>();
            _UserRepository = Initializer.Container.Resolve<IUserRepository>();
            _AccessPointRepository = Initializer.Container.Resolve<IRepository<AccessPoint>>();
        }

        private static void Start()
        {
            var userInput = MainMenu.ShowMainMenu();
            do
            {
                switch (userInput)
                {
                    case "I":
                        LoadBaseData();
                        break;
                    case "R":
                        GenerateDailyReports();
                        break;
                    case "L":
                        ProcessLogin();
                        break;
                    case "O":
                        ProcessLogout();
                        break;
                    case "N":
                        System.Console.WriteLine("Looking for access violation..." + MainMenu.GetNewLine());
                        break;
                    default:
                        System.Console.WriteLine("Invalid input!");
                        break;
                }
                System.Console.WriteLine(MainMenu.GetNewLine() + "Press `Enter` to continue...");
                System.Console.ReadLine();
                System.Console.Clear();
                userInput = MainMenu.ShowMainMenu();
            } while (!string.IsNullOrEmpty(userInput));
        }

        private static void GenerateDailyReports()
        {
            System.Console.WriteLine("Generating daily reports..." + MainMenu.GetNewLine());
            _ReportGeneratorFacade.GenerateCurrentReports();
            System.Console.WriteLine("Completd!" + MainMenu.GetNewLine());
        }

        private static void LoadBaseData()
        {
            System.Console.WriteLine("Processing initial data load..." + MainMenu.GetNewLine());
            _LdapSyncFacade.SyncFromLdap();
            _AccessPointFacade.SyncAccessPoints();
            System.Console.WriteLine("Completd!" + MainMenu.GetNewLine());
        }

        private static void ProcessLogin()
        {
            System.Console.Write("Enter email : ");
            var usr = System.Console.ReadLine();
            System.Console.Write("Enter password : ");
            var pwd = System.Console.ReadLine();

            var userDetails = _UserRepository.IsValidUser(usr, pwd);
            if (!userDetails.Equals(new KeyValuePair<int, string>()))
            {
                IsUserLoggedIn = true;
                UserName = userDetails.Value;
                _userId = userDetails.Key;
                //System.Console.WriteLine(MainMenu.GetNewLine() + (_isUserLoggedIn ? "Logged in as " + _userName : "Incorrect login"));
                System.Console.ForegroundColor = System.ConsoleColor.Green;
                System.Console.WriteLine(MainMenu.GetNewLine() + "Logged in as : " + UserName);

                ProcessManager(_UserRepository);
            }
            else
            {
                System.Console.ForegroundColor = System.ConsoleColor.Red;
                System.Console.WriteLine(MainMenu.GetNewLine() + "Incorrect login");
            }
            System.Console.ResetColor();
        }

        private static void ProcessLogout()
        {
            if (!IsUserLoggedIn)
                return;
            IsUserLoggedIn = false;
            UserName = string.Empty;
            _userId = 0;
            IsUserManager = false;
            _department = string.Empty;
            System.Console.WriteLine("Logged out");
        }

        private static void ProcessManager(IUserRepository userRepo)
        {
            var mgrDetails = userRepo.IsManager(_userId);
            if (mgrDetails.Key)
            {
                IsUserManager = true;
                _department = mgrDetails.Value;
                System.Console.WriteLine("You are manager of department : " + _department + MainMenu.GetNewLine());
                System.Console.ReadLine();

                var input = ManagerMenu.ShowManagerMenu(UserName, _department);
                while (!string.IsNullOrEmpty(input))
                {
                    switch (input)
                    {
                        case "1":
                            System.Console.WriteLine("You can see the logs @ " + ConfigHelper.GetLogPath());
                            break;
                        case "2":
                            var accessPoints = _AccessPointRepository.Get();
                            foreach (var ap in accessPoints)
                            {
                                System.Console.WriteLine("{0} | {1} | {2}", ap.Id, ap.Name, ap.Facility.Name);
                            }
                            break;
                        case "3":
                            System.Console.WriteLine("To grant user access, enter <AccessPointId> <EmployeeId> <access/manage/monitor>");
                            var data = System.Console.ReadLine().Split(' ');
                            if (data.Length != 3)
                                System.Console.WriteLine("Invalid details");
                            else
                            {
                                _AccessPointFacade.InsertUserAccess(data[1], data[0], data[2]);
                            }
                            break;
                        default:
                            System.Console.WriteLine("Invalid input!");
                            break;
                    }
                    System.Console.WriteLine(MainMenu.GetNewLine() + "Press `Enter` to continue..");
                    System.Console.ReadLine();
                    input = ManagerMenu.ShowManagerMenu(UserName, _department);
                }
            }
            else
                IsUserManager = false;
        }
    }
}
