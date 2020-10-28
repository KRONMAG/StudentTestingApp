using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using StudentTestingApp.Model.DataAccess.Interface;
using StudentTestingApp.Model.Entity.Interface;

namespace StudentTestingApp.Model.DataAccess
{
    public class ReadOnlyRepository<T> : IReadOnlyRepository<T> where T : class, IEntity, new()
    {
        protected readonly SQLiteConnection dbConnection;

        public ReadOnlyRepository(DbHelper helper)
        {
            dbConnection = helper.GetConnection<T>();
            dbConnection.CreateTable<T>();
        }

        public T Get(int id) =>
            dbConnection.Table<T>().First(item => item.Id == id);

        public List<T> Get(Predicate<T> predicate = null)
        {
            var items = dbConnection.Table<T>();
            if (predicate == null)
                return items.ToList();
            return items.Where(new Func<T, bool>(predicate)).ToList();
        }
    }
}