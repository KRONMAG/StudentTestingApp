using SQLite;
using StudentTestingApp.Model.DataAccess.Interface;
using StudentTestingApp.Model.Entity;
using StudentTestingApp.Model.Entity.Interface;

namespace StudentTestingApp.Model.DataAccess
{
    public class DbHelper : IDbHelper
    {
        private DbInfo _dbInfo;

        public DbHelper(DbInfo dbInfo)
        {
            _dbInfo = dbInfo;
        }

        public SQLiteConnection GetConnection<T>() where T: class, IEntity, new()
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