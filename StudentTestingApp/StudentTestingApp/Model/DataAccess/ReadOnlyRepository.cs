using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using StudentTestingApp.Model.Entity.Interface;

namespace StudentTestingApp.Model.DataAccess
{
    /// <summary>
    /// Хранилище сущностей с доступом на чтение
    /// </summary>
    /// <typeparam name="T">Тип хранимых сущностей</typeparam>
    public class ReadOnlyRepository<T> where T : class, IEntity, new()
    {
        /// <summary>
        /// Подключение к базе данных
        /// </summary>
        protected readonly SQLiteConnection dbConnection;

        /// <summary>
        /// Создание экземпляра класса
        /// </summary>
        /// <param name="helper">Создатель подключений к базам данных</param>
        public ReadOnlyRepository(DbHelper helper)
        {
            dbConnection = helper.GetConnection<T>();
            dbConnection.CreateTable<T>();
        }

        /// <summary>
        /// Поиск сущности по ее идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <returns>
        /// Сущность с указанным идентификатором или null, если ее не удалось найти
        /// </returns>
        public T Get(int id) =>
            dbConnection.Find<T>(id);

        /// <summary>
        /// Поиск сущностей, удовлетворяющих предикату
        /// </summary>
        /// <param name="predicate">
        /// Предикат для отбора сущностей,
        /// значение предиката null - выбираются все сущности,
        /// представленные в хранилище</param>
        /// <returns>
        /// Сущности, для которых значение предиката истинно
        /// или все сущности хранилища, если предикат равен null
        /// </returns>
        public List<T> Get(Predicate<T> predicate = null)
        {
            var items = dbConnection.Table<T>();
            if (predicate == null)
                return items.ToList();
            return items.Where(new Func<T, bool>(predicate)).ToList();
        }
    }
}