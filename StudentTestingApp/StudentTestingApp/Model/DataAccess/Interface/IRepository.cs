using StudentTestingApp.Model.Entity.Interface;

namespace StudentTestingApp.Model.DataAccess.Interface
{
    public interface IRepository<T> : IReadOnlyRepository<T> where T: class, IEntity, new()
    {
        void Add(T item);
        void Remove(T item);
        void Clear();
    }
}