using System.IO;
using Xamarin.Essentials;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using StudentTestingApp.Model.DataAccess.Interface;

namespace StudentTestingApp.Model.DataAccess
{
    /// <summary>
    /// Загрузчик базы данных тестов
    /// </summary>
    public class TestsLoader : ITestsLoader
    {
        /// <summary>
        /// Ключ хранилища настроек приложения,
        /// содержащий хеш-значение содержимого файла базы данных тестов
        /// </summary>
        private const string FILE_HASH_KEY = "tests-file-hash";

        /// <summary>
        /// Данные о расположении файлов баз данных
        /// </summary>
        private readonly DbInfo _dbInfo;

        /// <summary>
        /// Средство работы с облачным хранилищем
        /// </summary>
        private readonly CloudStorage _cloudStorage;

        /// <summary>
        /// Хранилище настроек приложения
        /// </summary>
        private readonly ISettings _settings;

        /// <summary>
        /// Была ли загружена база данных тестов
        /// </summary>
        public bool HaveTestsBeenLoaded =>
            File.Exists(_dbInfo.TestsDbLocalFilePath) &&
                _settings.GetValueOrDefault(FILE_HASH_KEY, null) != null;

        /// <summary>
        /// Была ли база данных тестов обновлена до последней версии
        /// </summary>
        public bool HaveTestsBeenUpdated
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

        /// <summary>
        /// Имеет ли устройство в данный момент доступ к сети интернет
        /// </summary>
        public bool IsInternetConnectionActive =>
            Connectivity.NetworkAccess == NetworkAccess.Internet;

        /// <summary>
        /// Создание экзмпляра класса
        /// </summary>
        /// <param name="dbInfo">Данные о расположении файлов баз данных</param>
        /// <param name="cloudStorage">Средство работы с облачным хранилищем</param>
        public TestsLoader(DbInfo dbInfo, CloudStorage cloudStorage)
        {
            _dbInfo = dbInfo;
            _cloudStorage = cloudStorage;
            _settings = CrossSettings.Current;
        }

        /// <summary>
        /// Загрузка базы данных тестов
        /// </summary>
        /// <returns>Истина, если загрузка базы данных выполнена успешно, иначе - ложь</returns>
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