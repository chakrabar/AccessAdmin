using System.Configuration;

namespace AccessManagement.Infrastructure
{
    public class ConfigHelper
    {
        public static string AppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static string GetLogPath()
        {
            return AppSetting("ActivityLogPath");
        }

        public static string GetHRSharedDrivePath()
        {
            return AppSetting("HRSharedDrive");
        }
    }
}
