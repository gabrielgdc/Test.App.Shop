using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.App.Shop.Domain.Aggregates.UserAggregate;
using Test.App.Shop.Domain.SeedWork;

namespace Test.App.Shop.Infra.Data.Mappings.Database;

public class UserGenderMap : IEntityTypeConfiguration<UserGender>
{
    public void Configure(EntityTypeBuilder<UserGender> builder)
    {
        builder.ToTable("UserGender");

        builder.HasKey(gender => gender.Id);

        builder.Property(gender => gender.Name)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Description")
            .IsRequired();

        builder.HasData(Enumeration.GetAll<UserGender>());
    }
}
