using System.Linq.Expressions;
using HyperProf.Core.Data.Contexts;
using HyperProf.Core.Models;

namespace HyperProf.Core.Repositories;

public class Repository<T> : IRepository<T> where T : BaseModel
{
    private readonly HyperprofDbContext _dbContext;

    public Repository(HyperprofDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public T? GetById(int id)
    {
        return _dbContext.Set<T>().Find(id);
    }

    public IEnumerable<T> GetAll()
    {
        return _dbContext.Set<T>().ToList();
    }

    public void Add(T entity)
    {
        _dbContext.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        _dbContext.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }

    public bool Any(Expression<Func<T, bool>> predicate)
    {
        return _dbContext.Set<T>().Any(predicate);
    }

    public IEnumerable<T> Search(Expression<Func<T, bool>> predicate)
    {
        return _dbContext.Set<T>().Where(predicate).ToList();
    }
}