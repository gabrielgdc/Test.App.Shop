// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Test.App.Shop.Infra.Data.Context;

#nullable disable

namespace Test.App.Shop.Infra.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220601031056_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Test.App.Shop.Domain.Aggregates.ApplicationAggregate.Application", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 4)
                        .HasColumnType("decimal(18,4)")
                        .HasColumnName("Price");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("Application", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("04aafaa0-609c-4bc8-b243-113e10d67406"),
                            Name = "PhotoShop",
                            Price = 10m
                        },
                        new
                        {
                            Id = new Guid("c3762b1e-a3d3-4a9c-a689-9dc2835a2ad1"),
                            Name = "LightRoom",
                            Price = 15m
                        },
                        new
                        {
                            Id = new Guid("4e584071-735b-4561-bc4a-c83a25d424ff"),
                            Name = "SnapSeed",
                            Price = 20m
                        },
                        new
                        {
                            Id = new Guid("02412b0a-ccf6-43e8-b9e0-5ad0a42142cb"),
                            Name = "Maya",
                            Price = 25m
                        },
                        new
                        {
                            Id = new Guid("1c083e90-b3c0-4e81-a9d8-24f3c358bc53"),
                            Name = "JetBrains Rider",
                            Price = 30m
                        },
                        new
                        {
                            Id = new Guid("83f5a6e0-3c52-4783-b9c2-224235d2f313"),
                            Name = "JetBrains IntelliJ",
                            Price = 35m
                        },
                        new
                        {
                            Id = new Guid("e06e4038-3ad9-47e4-9eaf-f3fa4c1b2155"),
                            Name = "JetBrains GoLang",
                            Price = 40m
                        });
                });

            modelBuilder.Entity("Test.App.Shop.Domain.Aggregates.OrdersAggregate.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedAt");

                    b.Property<Guid>("PaymentMethodId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("PaymentMethodId");

                    b.Property<decimal>("TotalPrice")
                        .HasPrecision(18, 4)
                        .HasColumnType("decimal(18,4)")
                        .HasColumnName("TotalPrice");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UserId");

                    b.Property<int>("_statusId")
                        .HasColumnType("int")
                        .HasColumnName("StatusId");

                    b.HasKey("Id");

                    b.HasIndex("PaymentMethodId");

                    b.HasIndex("UserId");

                    b.HasIndex("_statusId");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("Test.App.Shop.Domain.Aggregates.OrdersAggregate.OrderStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Description");

                    b.HasKey("Id");

                    b.ToTable("OrderStatus", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Pending"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Accept"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Rejected"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Cancelled"
                        });
                });

            modelBuilder.Entity("Test.App.Shop.Domain.Aggregates.UserAggregate.CardType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Description");

                    b.HasKey("Id");

                    b.ToTable("CardType", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Visa"
                        },
                        new
                        {
                            Id = 2,
                            Name = "MasterCard"
                        });
                });

            modelBuilder.Entity("Test.App.Shop.Domain.Aggregates.UserAggregate.PaymentMethod", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Alias")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("Alias");

                    b.Property<string>("CardHolderName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("CardHolderName");

                    b.Property<long>("CardNumber")
                        .HasMaxLength(25)
                        .HasColumnType("bigint")
                        .HasColumnName("CardNumber");

                    b.Property<DateTime>("ExpirationDate")
                        .HasMaxLength(25)
                        .HasColumnType("datetime2")
                        .HasColumnName("ExpirationDate");

                    b.Property<decimal>("Limit")
                        .HasMaxLength(25)
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Limit");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("_cardTypeId")
                        .HasColumnType("int")
                        .HasColumnName("CardTypeId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("_cardTypeId");

                    b.ToTable("PaymentMethod", (string)null);
                });

            modelBuilder.Entity("Test.App.Shop.Domain.Aggregates.UserAggregate.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("BirthDate");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Cpf");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("FullName");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Password");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("Salt");

                    b.Property<int>("_genderId")
                        .HasColumnType("int")
                        .HasColumnName("GenderId");

                    b.HasKey("Id");

                    b.HasIndex("_genderId");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("Test.App.Shop.Domain.Aggregates.UserAggregate.UserGender", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Description");

                    b.HasKey("Id");

                    b.ToTable("UserGender", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Male"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Female"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Other"
                        });
                });

            modelBuilder.Entity("Test.App.Shop.Domain.Aggregates.ApplicationAggregate.Application", b =>
                {
                    b.HasOne("Test.App.Shop.Domain.Aggregates.OrdersAggregate.Order", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("Test.App.Shop.Domain.Aggregates.OrdersAggregate.Order", b =>
                {
                    b.HasOne("Test.App.Shop.Domain.Aggregates.UserAggregate.PaymentMethod", null)
                        .WithMany()
                        .HasForeignKey("PaymentMethodId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Test.App.Shop.Domain.Aggregates.UserAggregate.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Test.App.Shop.Domain.Aggregates.OrdersAggregate.OrderStatus", "Status")
                        .WithMany()
                        .HasForeignKey("_statusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Status");
                });

            modelBuilder.Entity("Test.App.Shop.Domain.Aggregates.UserAggregate.PaymentMethod", b =>
                {
                    b.HasOne("Test.App.Shop.Domain.Aggregates.UserAggregate.User", null)
                        .WithMany("PaymentMethods")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Test.App.Shop.Domain.Aggregates.UserAggregate.CardType", "CardType")
                        .WithMany()
                        .HasForeignKey("_cardTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CardType");
                });

            modelBuilder.Entity("Test.App.Shop.Domain.Aggregates.UserAggregate.User", b =>
                {
                    b.HasOne("Test.App.Shop.Domain.Aggregates.UserAggregate.UserGender", "Gender")
                        .WithMany()
                        .HasForeignKey("_genderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.OwnsOne("Test.App.Shop.Domain.Aggregates.UserAggregate.UserAddress", "Address", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("City");

                            b1.Property<string>("Complement")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Complement");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Country");

                            b1.Property<string>("District")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("District");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Number");

                            b1.Property<string>("State")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("State");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Street");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("ZipCode");

                            b1.HasKey("UserId");

                            b1.ToTable("User");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Address");

                    b.Navigation("Gender");
                });

            modelBuilder.Entity("Test.App.Shop.Domain.Aggregates.OrdersAggregate.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("Test.App.Shop.Domain.Aggregates.UserAggregate.User", b =>
                {
                    b.Navigation("PaymentMethods");
                });
#pragma warning restore 612, 618
        }
    }
}
