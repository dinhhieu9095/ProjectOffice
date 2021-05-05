using System.Configuration;
using System.IO;

namespace DaiPhatDat.Core.Kernel.Application
{
    public class AppSettings
    {
        protected AppSettings()
        {

        }
        private static string _rootFolderPath = "";
        public static string RootFolderPath
        {
            get
            {
                if (!string.IsNullOrEmpty(_rootFolderPath))
                {
                    return _rootFolderPath;
                }
                if (string.IsNullOrEmpty(_rootFolderPath))
                {
                    _rootFolderPath = ConfigurationManager.AppSettings["SurePortalFolder"];
                }
                if (string.IsNullOrEmpty(_rootFolderPath))
                {
                    _rootFolderPath = $"C:\\SurePortal";
                }
                if (!Directory.Exists(_rootFolderPath))
                {
                    Directory.CreateDirectory(_rootFolderPath);
                }
                return _rootFolderPath;

            }
        }
        public static string FileFolderPath
        {
            get
            {
                var fileFolder = Path.Combine(RootFolderPath, "Files");
                if (!Directory.Exists(fileFolder))
                {
                    Directory.CreateDirectory(fileFolder);
                }
                return fileFolder;

            }
        }
        public static string ImageFolderPath
        {
            get
            {
                var imageFolder = Path.Combine(RootFolderPath, "Images");
                if (!Directory.Exists(imageFolder))
                {
                    Directory.CreateDirectory(imageFolder);
                }
                return imageFolder;

            }
        }
        public static string SignFileFolderPath
        {
            get
            {
                var fileFolder = Path.Combine(RootFolderPath, "SignFiles");
                if (!Directory.Exists(fileFolder))
                {
                    Directory.CreateDirectory(fileFolder);
                }
                return fileFolder;

            }
        }
        public static string LogsFolderPath
        {
            get
            {
                var folder = Path.Combine(RootFolderPath, "Logs");
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                return folder;

            }
        }
        public static string EncryptKey
        {
            get
            {
                return ConfigurationManager.AppSettings["EncryptKey"] + string.Empty;
            }
        }
        public static string SiteUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["SiteUrl"] + string.Empty;
            }
        }
        public static string HomeUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["HomeUrl"] + string.Empty;
            }
        }
        public static string LoginUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["LoginUrl"] + string.Empty;
            }
        }
        public static string LogoutUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["LogoutUrl"] + string.Empty;
            }
        }
        public static string DomainName
        {
            get
            {
                return ConfigurationManager.AppSettings["DomainName"] + string.Empty;
            }
        }


    }
}
