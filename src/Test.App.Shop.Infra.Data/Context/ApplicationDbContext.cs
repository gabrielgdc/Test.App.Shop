using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Test.App.Shop.Infra.CrossCutting.Environments.Configurations;
using Test.App.Shop.Infra.Data.Mappings.Database;

namespace Test.App.Shop.Infra.Data.Context;

public class ApplicationDbContext : DbContext
{
    private readonly IMediator _bus;
    private readonly ApplicationConfiguration _applicationConfiguration;

    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(ApplicationConfiguration applicationConfiguration)
    {
        _applicationConfiguration = applicationConfiguration;
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ApplicationConfiguration applicationConfiguration) : base(options)
    {
        _applicationConfiguration = applicationConfiguration;
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediator mediator, ApplicationConfiguration applicationConfiguration) : base(options)
    {
        _bus = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _applicationConfiguration = applicationConfiguration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer("Server=127.0.0.1;Database=applications-shop;User=sa;Password=RootPassword@1234;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new UserGenderMap());
        modelBuilder.ApplyConfiguration(new PaymentMethodMap());
        modelBuilder.ApplyConfiguration(new CardTypeMap());
        modelBuilder.ApplyConfiguration(new ApplicationMap());
        modelBuilder.ApplyConfiguration(new OrderMap());
        modelBuilder.ApplyConfiguration(new OrderStatusMap());
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        // Dispatch Domain Events collection.
        // Choices:
        // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including
        // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
        // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions.
        // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers.
        await _bus.DispatchDomainEventsAsync(this);

        // After executing this line all the changes (from the Command Handler and Domain Event Handlers)
        // performed through the DbContext will be committed
        return await base.SaveChangesAsync(cancellationToken) > 0;
    }
}
