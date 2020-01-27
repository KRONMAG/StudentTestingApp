using StudentTestingApp.Model.DataAccess.Interface;
using StudentTestingApp.Model.Entity.Interface;

namespace StudentTestingApp.Model.DataAccess
{
    public class Repository<T> : ReadOnlyRepository<T>, IRepository<T> where T : class, IEntity, new()
    {
        public Repository(IDbHelper dbHelper) : base(dbHelper)
        {

        }

        public void Add(T item)
        {
            db.Insert(item);
        }
    }
}