using System.Threading.Tasks;
using Test.App.Shop.Domain.SeedWork;
using Test.App.Shop.Infra.Data.Context;

namespace Test.App.Shop.Infra.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
	private readonly ApplicationDbContext _applicationDbContext;

	public UnitOfWork(ApplicationDbContext applicationDbContext)
	{
		_applicationDbContext = applicationDbContext;
	}

	public async Task<bool> CommitAsync()
	{
		return await _applicationDbContext.SaveEntitiesAsync();
	}

	public void Dispose()
	{
		_applicationDbContext.Dispose();
	}
}