using System.Linq.Expressions;
using HyperProf.Core.Models;

namespace HyperProf.Core.Repositories;

public interface IRepository<T>
{
    T? GetById(int id);
    IEnumerable<T> GetAll();
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    bool Any(Expression<Func<T, bool>> predicate);
    IEnumerable<T> Search(Expression<Func<T, bool>> predicate);
}