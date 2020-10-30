using SQLite;
using StudentTestingApp.Model.Entity;

namespace StudentTestingApp.Model.DataAccess
{
    /// <summary>
    /// Вспомогательный класс, отвечающий за создание подключений к базам данных
    /// </summary>
    public class DbHelper
    {
        /// <summary>
        /// Данные расположения файлов баз данных
        /// </summary>
        private DbInfo _dbInfo;

        /// <summary>
        /// Создание экземпляра класса
        /// </summary>
        /// <param name="dbInfo">Данные расположения файлов баз данных</param>
        public DbHelper(DbInfo dbInfo)
        {
            _dbInfo = dbInfo;
        }

        /// <summary>
        /// Создание соединения с базой данных для сущностей указанного типа
        /// </summary>
        /// <typeparam name="T">Тип хранимых сущностей</typeparam>
        /// <returns>
        /// Для типа сущности TestResult возвращается подключение к базе данных приложения,
        /// для остальных типов сущностей - подключение к базе данных тестов
        /// </returns>
        public SQLiteConnection GetConnection<T>()
        {
            SQLiteConnection connection;
            if (typeof(T) == typeof(TestResult))
                connection = new SQLiteConnection(_dbInfo.AppDbFilePath);
            else
                connection = new SQLiteConnection(_dbInfo.TestsDbLocalFilePath);
            connection.CreateTable<T>();
            return connection;
        }
    }
}