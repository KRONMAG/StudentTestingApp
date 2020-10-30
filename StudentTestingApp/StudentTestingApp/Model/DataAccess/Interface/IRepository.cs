using StudentTestingApp.Model.Entity.Interface;

namespace StudentTestingApp.Model.DataAccess.Interface
{
    /// <summary>
    /// Хранилище сущностей с доступом на чтение и запись
    /// </summary>
    /// <typeparam name="T">Тип хранимых сущностей</typeparam>
    public interface IRepository<T> : IReadOnlyRepository<T> where T: class, IEntity, new()
    {
        /// <summary>
        /// Добавление сущности в хранилище
        /// </summary>
        /// <param name="item">Добавляемая сущность</param>
        void Add(T item);

        /// <summary>
        /// Удаление сущности из хранилище по ее идентификатору
        /// </summary>
        /// <param name="id">Идентификатор удаляемой сущности</param>
        void Remove(int id);

        /// <summary>
        /// Удаление всех сущностей из хранилища
        /// </summary>
        void Clear();
    }
}