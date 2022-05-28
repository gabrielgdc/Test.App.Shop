using System;
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
        return await DbSet
            .Include(user => user.Gender)
            .Include("PaymentMethods.CardType")
            .SingleOrDefaultAsync(user => user.Cpf == cpf);
    }

    public async Task<User> GetUserById(Guid userId)
    {
        return await DbSet
            .Include(user => user.Gender)
            .Include("PaymentMethods.CardType")
            .SingleOrDefaultAsync(user => user.Id.Equals(userId));
    }
}
