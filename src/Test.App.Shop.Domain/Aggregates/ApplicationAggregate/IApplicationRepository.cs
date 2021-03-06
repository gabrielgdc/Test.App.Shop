using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Test.App.Shop.Domain.Aggregates.ApplicationAggregate;

public interface IApplicationRepository
{
    IEnumerable<Application> GetApplications();
    Task<Application> GetApplicationById(Guid id);
}
