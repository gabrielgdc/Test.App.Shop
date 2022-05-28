using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.App.Shop.Domain.Aggregates.PaymentMethodAggregate;

namespace Test.App.Shop.Infra.Data.Mappings.Database;

public class PaymentMethodMap : IEntityTypeConfiguration<PaymentMethod>
{
    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        builder.ToTable("PaymentMethod");

        builder.HasKey(paymentMethod => paymentMethod.Id);

        builder.Property(paymentMethod => paymentMethod.Alias)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Alias")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(paymentMethod => paymentMethod.CardHolderName)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("CardHolderName")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(paymentMethod => paymentMethod.CardNumber)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("CardNumber")
            .HasMaxLength(25)
            .IsRequired();

        builder.Property("_cardTypeId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("CardTypeId")
            .IsRequired();

        builder.HasOne(paymentMethod => paymentMethod.CardType)
            .WithMany()
            .HasForeignKey("_cardTypeId")
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(paymentMethod => paymentMethod.ExpirationDate)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("ExpirationDate")
            .HasMaxLength(25)
            .IsRequired();
    }
}
