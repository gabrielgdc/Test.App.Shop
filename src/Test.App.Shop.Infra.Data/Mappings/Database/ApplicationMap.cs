using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Test.App.Shop.Infra.Data.Mappings.Database;

public class ApplicationMap : IEntityTypeConfiguration<Domain.Aggregates.ApplicationAggregate.Application>
{
    public void Configure(EntityTypeBuilder<Domain.Aggregates.ApplicationAggregate.Application> builder)
    {
        builder.ToTable("Application");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Name")
            .IsRequired();

        builder.Property(a => a.Price)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Price")
            .HasPrecision(18, 4)
            .IsRequired();

        var applicationData = new List<Domain.Aggregates.ApplicationAggregate.Application>
        {
            new("PhotoShop", 10),
            new("LightRoom", 15),
            new("SnapSeed", 20),
            new("Maya", 25),
            new("JetBrains Rider", 30),
            new("JetBrains IntelliJ", 35),
            new("JetBrains GoLang", 40)
        };

        applicationData.ForEach(a => a.SetId());

        builder.HasData(applicationData);
    }
}
