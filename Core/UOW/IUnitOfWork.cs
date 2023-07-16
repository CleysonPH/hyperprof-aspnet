using HyperProf.Core.Models;
using HyperProf.Core.Repositories;

namespace HyperProf.Core.UOW;

public interface IUnitOfWork : IDisposable
{
    IRepository<Teacher> Teachers { get; }
    IRepository<Student> Students { get; }
    IRepository<InvalidatedToken> InvalidatedTokens { get; }

    void BeginTransaction();
    void Commit();
    void Rollback();
    void SaveChanges();
}