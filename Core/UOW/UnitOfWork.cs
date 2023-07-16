using HyperProf.Core.Data.Contexts;
using HyperProf.Core.Models;
using HyperProf.Core.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace HyperProf.Core.UOW;

public class UnitOfWork : IUnitOfWork
{
    private readonly HyperprofDbContext _dbContext;
    private IDbContextTransaction? _transaction;

    private readonly IRepository<Teacher> _teachers;
    public IRepository<Teacher> Teachers => _teachers;

    private readonly IRepository<Student> _students;
    public IRepository<Student> Students => _students;

    private readonly IRepository<InvalidatedToken> _invalidatedTokens;
    public IRepository<InvalidatedToken> InvalidatedTokens => _invalidatedTokens;

    public UnitOfWork(
        HyperprofDbContext dbContext,
        IRepository<Teacher> teachers,
        IRepository<Student> students,
        IRepository<InvalidatedToken> invalidatedTokens)
    {
        _dbContext = dbContext;
        _teachers = teachers;
        _students = students;
        _invalidatedTokens = invalidatedTokens;
    }

    public void BeginTransaction()
    {
        _transaction = _dbContext.Database.BeginTransaction();
    }

    public void Commit()
    {
        try
        {
            _dbContext.SaveChanges();
            _transaction?.Commit();
        }
        catch
        {
            _transaction?.Rollback();
            throw;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _dbContext.Dispose();
    }

    public void Rollback()
    {
        _transaction?.Rollback();
    }

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }
}