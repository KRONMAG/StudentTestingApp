using System;

namespace StudentTestingApp.Model.DataAccess
{
    /// <summary>
    /// Данные расположения баз данных
    /// </summary>
    public class DbInfo
    {
        /// <summary>
        /// Наименование файла базы данных тестов
        /// </summary>
        public string TestsDbFileName => "StudentTestingDb.db";

        /// <summary>
        /// Локальный путь к файлу с базой данных тестов
        /// </summary>
        public string TestsDbLocalFilePath => GetLocalFilePath(TestsDbFileName);

        /// <summary>
        /// Путь к файлу с базой данных тестов, расположенной в облачном хранилище
        /// </summary>
        public string TestsDbRemoteFilePath => $"/{TestsDbFileName}";

        /// <summary>
        /// Наименование файла базы данных приложения
        /// </summary>
        public string AppDbFileName => "App.db";

        /// <summary>
        /// Путь к файлу базы данных приложения
        /// </summary>
        public string AppDbFilePath => GetLocalFilePath(AppDbFileName);

        /// <summary>
        /// Получение полного пути к файлу, расположенному в папке данных приложения
        /// </summary>
        /// <param name="fileName">Наименование файла</param>
        /// <returns>Полный путь к файлу, находящемуся в каталоге данных приложения</returns>
        private string GetLocalFilePath(string fileName) =>
            $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/{fileName}";
    }
}