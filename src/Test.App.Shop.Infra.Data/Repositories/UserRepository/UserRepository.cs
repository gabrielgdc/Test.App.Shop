using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Test.App.Shop.Infra.Data.Context;
using Test.App.Shop.Domain.Aggregates.UserAggregate;

namespace Test.App.Shop.Infra.Data.Repositories.UserRepository;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
    }

    public async Task<User> GetUserByCpf(string cpf)
    {
        return await DbSet.SingleOrDefaultAsync(user => user.Cpf.Equals(cpf));
    }
}
