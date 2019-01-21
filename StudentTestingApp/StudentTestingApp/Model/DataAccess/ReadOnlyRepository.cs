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
        protected readonly SQLiteConnection db;

        public ReadOnlyRepository()
        {
            db = new SQLiteConnection(DbInfo.Instance.DbLocalFilePath);
            db.CreateTable<T>();
        }

        public T Get(int id)
        {
            return db.Table<T>().First(item => item.Id == id);
        }

        public IEnumerable<T> GetAll(Predicate<T> predicate = null)
        {
            var items = db.Table<T>();
            if (predicate == null)
            {
                return items;
            }
            else
            {
                return items.Where(new Func<T, bool>(predicate));
            }
        }
    }
}