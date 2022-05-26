using Test.App.Shop.Domain.SeedWork;
using Test.App.Shop.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Test.App.Shop.Infra.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IAggregateRoot
{
    protected readonly ApplicationDbContext ApplicationDbContext;
    protected readonly DbSet<TEntity> DbSet;

    public Repository(ApplicationDbContext applicationDbContext)
    {
        ApplicationDbContext = applicationDbContext;
        DbSet = applicationDbContext.Set<TEntity>();
    }

    public void Add(TEntity obj)
    {
        ApplicationDbContext.Add(obj);
    }
}
