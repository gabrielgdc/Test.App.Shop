using System.Threading.Tasks;
using Test.App.Shop.Domain.SeedWork;

namespace Test.App.Shop.Domain.Aggregates.UserAggregate;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetUserByCpf(string cpf);
}
