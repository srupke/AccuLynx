using System.Diagnostics;
using System.Reflection;

namespace AccuLynx.Twitter.Web.Helpers
{
    public static class ApplicationHelper
    {
        public static string Name { get; private set; }
        public static string Version { get; private set; }
        public static string Description { get; private set; }
        public static string Company { get; private set; }
        public static string Copyright { get; private set; }

        static ApplicationHelper()
        {
            Name = GetTitle();
            Version = GetVersionNumber();
            Description = GetDescription();
            Company = GetCompany();
            Copyright = GetCopyright();
        }

        private static string GetCompany()
        {
            string result = string.Empty;
            Assembly assembly = Assembly.GetExecutingAssembly();

            if (assembly != null)
            {
                object[] customAttributes = assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if ((customAttributes != null) && (customAttributes.Length > 0))
                    result = ((AssemblyCompanyAttribute)customAttributes[0]).Company;
            }

            return result;
        }

        private static string GetCopyright()
        {
            string result = string.Empty;
            Assembly assembly = Assembly.GetExecutingAssembly();

            if (assembly != null)
            {
                object[] customAttributes = assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if ((customAttributes != null) && (customAttributes.Length > 0))
                    result = ((AssemblyCopyrightAttribute)customAttributes[0]).Copyright;
            }
            return result;
        }

        private static string GetTitle()
        {
            string result = string.Empty;
            Assembly assembly = Assembly.GetExecutingAssembly();

            if (assembly != null)
            {
                object[] customAttributes = assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if ((customAttributes != null) && (customAttributes.Length > 0))
                    result = ((AssemblyTitleAttribute)customAttributes[0]).Title;
            }

            return result;
        }

        private static string GetDescription()
        {
            string result = string.Empty;
            Assembly assembly = Assembly.GetExecutingAssembly();

            if (assembly != null)
            {
                object[] customAttributes = assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if ((customAttributes != null) && (customAttributes.Length > 0))
                    result = ((AssemblyDescriptionAttribute)customAttributes[0]).Description;
            }

            return result;
        }

        private static string GetVersionNumber()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fvi.FileVersion;
        }
    }
}