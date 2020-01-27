using System.IO;
using Xamarin.Essentials;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using StudentTestingApp.Model.DataAccess.Interface;

namespace StudentTestingApp.Model.DataAccess
{
    public class TestsLoader : ITestsLoader
    {
        private const string FILE_HASH_KEY = "tests-file-hash";

        private IDbInfo _dbInfo;
        private ICloudStorage _cloudStorage;
        private ISettings _settings;

        public bool TestsAreLoaded => File.Exists(_dbInfo.TestsDbLocalFilePath) &&
                                      _settings.GetValueOrDefault(FILE_HASH_KEY, null) != null;

        public bool TestsAreUpdated
        {
            get
            {
                var hash = _settings.GetValueOrDefault(FILE_HASH_KEY, null);
                if (hash == null)
                    return false;
                if (hash != _cloudStorage.GetFileHash(_dbInfo.TestsDbRemoteFilePath))
                    return false;
                return true;
            }
        }

        public bool InternetConnectionIsActive => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public TestsLoader(IDbInfo dbInfo, ICloudStorage cloudStorage)
        {
            _dbInfo = dbInfo;
            _cloudStorage = cloudStorage;
            _settings = CrossSettings.Current;
        }

        public bool LoadTests()
        {
            var result = _cloudStorage.DownloadFile(_dbInfo.TestsDbRemoteFilePath, _dbInfo.TestsDbLocalFilePath);
            var hash = _cloudStorage.GetFileHash(_dbInfo.TestsDbRemoteFilePath);
            if (result && hash != null)
            {
                CrossSettings.Current.AddOrUpdateValue(FILE_HASH_KEY, hash);
                return true;
            }
            return false;
        }
    }
}