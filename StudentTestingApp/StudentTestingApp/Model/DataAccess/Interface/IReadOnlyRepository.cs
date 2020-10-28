using System;
using System.Collections.Generic;
using StudentTestingApp.Model.Entity.Interface;

namespace StudentTestingApp.Model.DataAccess.Interface
{
    public interface IReadOnlyRepository<T> where T : class, IEntity, new()
    {
        T Get(int id);
        List<T> Get(Predicate<T> predicate = null);
    }
}