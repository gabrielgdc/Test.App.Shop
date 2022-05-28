using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.App.Shop.Domain.Aggregates.UserAggregate;

namespace Test.App.Shop.Infra.Data.Mappings.Database;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.HasKey(user => user.Id);

        builder.Property(user => user.FullName)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("FullName")
            .IsRequired();

        builder.Property(user => user.Cpf)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Cpf")
            .IsRequired();

        builder.Property(user => user.BirthDate)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("BirthDate")
            .IsRequired();

        builder.Property("_genderId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("GenderId")
            .IsRequired();

        builder.HasOne(user => user.Gender)
            .WithMany()
            .IsRequired()
            .HasForeignKey("_genderId")
            .OnDelete(DeleteBehavior.Restrict);

        builder.OwnsOne(user => user.Address, address =>
        {
            address.Property(a => a.City)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("City")
                .IsRequired();

            address.Property(a => a.State)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("State")
                .IsRequired();

            address.Property(a => a.Complement)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Complement")
                .IsRequired();

            address.Property(a => a.Country)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Country")
                .IsRequired();

            address.Property(a => a.District)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("District")
                .IsRequired();

            address.Property(a => a.Number)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Number")
                .IsRequired();

            address.Property(a => a.Street)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Street")
                .IsRequired();

            address.Property(a => a.ZipCode)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("ZipCode")
                .IsRequired();

            address.WithOwner();
        });

        builder.Property(user => user.Salt)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Salt")
            .IsRequired();

        builder.Property(user => user.Password)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Password")
            .IsRequired();

        builder.HasMany(user => user.PaymentMethods)
            .WithOne()
            .HasForeignKey("UserId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
