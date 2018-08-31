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
        private readonly SQLiteConnection _db;

        public ReadOnlyRepository()
        {
            _db = new SQLiteConnection(DbInfo.Instance.DbLocalFilePath);
            _db.CreateTable<T>();
        }

        public T GetItem(int id)
        {
            return _db.Table<T>().First(item => item.Id == id);
        }

        public IEnumerable<T> GetItems(Predicate<T> predicate = null)
        {
            var items = _db.Table<T>();
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