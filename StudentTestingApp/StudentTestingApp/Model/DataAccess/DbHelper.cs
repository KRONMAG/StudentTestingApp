using SQLite;
using StudentTestingApp.Model.Entity;

namespace StudentTestingApp.Model.DataAccess
{
    public class DbHelper
    {
        private DbInfo _dbInfo;

        public DbHelper(DbInfo dbInfo)
        {
            _dbInfo = dbInfo;
        }

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