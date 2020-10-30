namespace StudentTestingApp.Model.DataAccess.Interface
{
    /// <summary>
    /// Загрузчик базы данных тестов
    /// </summary>
    public interface ITestsLoader
    {
        /// <summary>
        /// Была ли загружена база данных тестов
        /// </summary>
        bool HaveTestsBeenLoaded { get; }

        /// <summary>
        /// Была ли база данных тестов обновлена до последней версии
        /// </summary>
        bool HaveTestsBeenUpdated { get; }

        /// <summary>
        /// Имеет ли устройство в данный момент доступ к сети интернет
        /// </summary>
        bool IsInternetConnectionActive { get; }

        /// <summary>
        /// Загрузка базы данных тестов
        /// </summary>
        /// <returns>
        /// Истина, если загрузка базы данных выполнена успешно, иначе - ложь
        /// </returns>
        bool LoadTests();
    }
}