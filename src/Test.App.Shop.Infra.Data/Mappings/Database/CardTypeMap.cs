using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.App.Shop.Domain.Aggregates.PaymentMethodAggregate;
using Test.App.Shop.Domain.SeedWork;

namespace Test.App.Shop.Infra.Data.Mappings.Database;

public class CardTypeMap : IEntityTypeConfiguration<CardType>
{
    public void Configure(EntityTypeBuilder<CardType> builder)
    {
        builder.ToTable("CardType");

        builder.HasKey(gender => gender.Id);

        builder.Property(gender => gender.Name)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Description")
            .IsRequired();

        builder.HasData(Enumeration.GetAll<CardType>());
    }
}
