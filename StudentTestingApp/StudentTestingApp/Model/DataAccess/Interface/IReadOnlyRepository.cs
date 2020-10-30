using System;
using System.Collections.Generic;
using StudentTestingApp.Model.Entity.Interface;

namespace StudentTestingApp.Model.DataAccess.Interface
{
    /// <summary>
    /// Хранилище сущностей с доступом на чтение
    /// </summary>
    /// <typeparam name="T">Тип хранимых сущностей</typeparam>
    public interface IReadOnlyRepository<T> where T : class, IEntity, new()
    {
        /// <summary>
        /// Поиск сущности по ее идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <returns>
        /// Сущность с указанным идентификатором или null, если ее не удалось найти
        /// </returns>
        T Get(int id);

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
        List<T> Get(Predicate<T> predicate = null);
    }
}