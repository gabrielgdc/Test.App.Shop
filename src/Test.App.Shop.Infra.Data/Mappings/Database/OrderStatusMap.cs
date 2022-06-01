using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.App.Shop.Domain.Aggregates.OrdersAggregate;
using Test.App.Shop.Domain.SeedWork;

namespace Test.App.Shop.Infra.Data.Mappings.Database;

public class OrderStatusMap : IEntityTypeConfiguration<OrderStatus>
{
    public void Configure(EntityTypeBuilder<OrderStatus> builder)
    {
        builder.ToTable("OrderStatus");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Name)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Description")
            .IsRequired();

        builder.HasData(Enumeration.GetAll<OrderStatus>());
    }
}
