using System;

namespace StudentTestingApp.Model.DataAccess
{
    public class DbInfo
    {
        private static DbInfo _instance;

        public static DbInfo Instance => _instance ?? (_instance = new DbInfo());

        public string DbFileName { get; }
        public string DbLocalFilePath { get; }
        public string DbRemoteFilePath { get; }

        private DbInfo()
        {
            var dbFileName = "StudentTestingDb.db";
            DbFileName = dbFileName;
            DbLocalFilePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/{dbFileName}";
            DbRemoteFilePath = dbFileName;
        }
    }
}