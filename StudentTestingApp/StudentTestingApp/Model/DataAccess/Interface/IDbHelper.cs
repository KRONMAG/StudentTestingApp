using SQLite;
using StudentTestingApp.Model.Entity.Interface;

namespace StudentTestingApp.Model.DataAccess.Interface
{
    public interface IDbHelper
    {
        SQLiteConnection GetConnection<T>() where T : class, IEntity, new();
    }
}