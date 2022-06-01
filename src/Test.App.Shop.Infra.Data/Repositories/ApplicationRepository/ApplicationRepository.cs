using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Test.App.Shop.Domain.Aggregates.ApplicationAggregate;
using Test.App.Shop.Infra.Data.Context;

namespace Test.App.Shop.Infra.Data.Repositories.ApplicationRepository;

public class ApplicationRepository : Repository<Domain.Aggregates.ApplicationAggregate.Application>, IApplicationRepository
{
    public ApplicationRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
    }

    public IEnumerable<Domain.Aggregates.ApplicationAggregate.Application> GetApplications()
    {
        return DbSet;
    }

    public async Task<Domain.Aggregates.ApplicationAggregate.Application> GetApplicationById(Guid id)
    {
        return await DbSet.SingleOrDefaultAsync(application => application.Id.Equals(id));
    }
}
