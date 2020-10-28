using StudentTestingApp.Model.DataAccess.Interface;
using StudentTestingApp.Model.Entity.Interface;

namespace StudentTestingApp.Model.DataAccess
{
    public class Repository<T> : ReadOnlyRepository<T>, IRepository<T> where T : class, IEntity, new()
    {
        public Repository(DbHelper dbHelper) : base(dbHelper)
        {

        }

        public void Add(T item) =>
            dbConnection.Insert(item);

        public void Remove(T item) =>
            dbConnection.Delete(item);

        public void Clear() =>
            dbConnection.DeleteAll<T>();
    }
}