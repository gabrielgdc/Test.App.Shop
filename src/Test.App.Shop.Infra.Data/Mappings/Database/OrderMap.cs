using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.App.Shop.Domain.Aggregates.OrdersAggregate;
using Test.App.Shop.Domain.Aggregates.UserAggregate;

namespace Test.App.Shop.Infra.Data.Mappings.Database
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");

            builder.HasKey(order => order.Id);

            builder.Property(o => o.UserId)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("UserId")
                .IsRequired();

            builder.Property(o => o.CreatedAt)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("CreatedAt")
                .IsRequired();

            builder.Property(o => o.PaymentMethodId)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("PaymentMethodId");

            builder.Property(order => order.TotalPrice)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("TotalPrice")
                .HasPrecision(18, 4)
                .IsRequired();

            builder.Property<int>("_statusId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("StatusId")
                .IsRequired();

            builder.HasOne(order => order.Status)
                .WithMany()
                .IsRequired()
                .HasForeignKey("_statusId")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<PaymentMethod>()
                .WithMany()
                .HasForeignKey(o => o.PaymentMethodId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(o => o.UserId);
        }
    }
}
