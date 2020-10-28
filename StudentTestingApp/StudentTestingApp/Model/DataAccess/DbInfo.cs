using System;

namespace StudentTestingApp.Model.DataAccess
{
    public class DbInfo
    {
        public string TestsDbFileName => "StudentTestingDb.db";

        public string TestsDbLocalFilePath => GetLocalFilePath(TestsDbFileName);

        public string TestsDbRemoteFilePath => $"/{TestsDbFileName}";

        public string AppDbFileName => "App.db";

        public string AppDbFilePath => GetLocalFilePath(AppDbFileName);

        private string GetLocalFilePath(string fileName) =>
            $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/{fileName}";
    }
}