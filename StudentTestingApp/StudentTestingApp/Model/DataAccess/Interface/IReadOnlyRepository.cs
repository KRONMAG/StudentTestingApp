using System;
using System.Collections.Generic;
using StudentTestingApp.Model.Entity.Interface;

namespace StudentTestingApp.Model.DataAccess.Interface
{
    public interface IReadOnlyRepository<out T> where T : class, IEntity, new()
    {
        T Get(int id);
        IEnumerable<T> GetAll(Predicate<T> predicate = null);
    }
}