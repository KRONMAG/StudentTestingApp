using StudentTestingApp.Model.DataAccess.Interface;
using StudentTestingApp.Model.Entity.Interface;

namespace StudentTestingApp.Model.DataAccess
{
    /// <summary>
    /// Хранилище сущностей с доступом на чтение и запись
    /// </summary>
    /// <typeparam name="T">Тип хранимых сущностей</typeparam>
    public class Repository<T> : ReadOnlyRepository<T>, IRepository<T> where T : class, IEntity, new()
    {
        /// <summary>
        /// Создание экземпляра класса
        /// </summary>
        /// <param name="dbHelper">Создатель подключений к базам данных</param>
        public Repository(DbHelper dbHelper) : base(dbHelper)
        {

        }

        /// <summary>
        /// Добавление сущности в хранилище
        /// </summary>
        /// <param name="item">Добавляемая сущность</param>
        public void Add(T item) =>
            dbConnection.Insert(item);

        /// <summary>
        /// Удаление сущности из хранилище по ее идентификатору
        /// </summary>
        /// <param name="id">Идентификатор удаляемой сущности</param>
        public void Remove(int id) =>
            dbConnection.Delete<T>(id);

        /// <summary>
        /// Удаление всех сущностей из хранилища
        /// </summary>
        public void Clear() =>
            dbConnection.DeleteAll<T>();
    }
}