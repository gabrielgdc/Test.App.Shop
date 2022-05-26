using System.Threading.Tasks;

namespace Test.App.Shop.Domain.SeedWork;

public interface IUnitOfWork
{
	Task<bool> CommitAsync();
}
