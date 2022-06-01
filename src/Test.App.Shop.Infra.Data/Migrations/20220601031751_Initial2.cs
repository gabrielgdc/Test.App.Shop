using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test.App.Shop.Infra.Data.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Application",
                keyColumn: "Id",
                keyValue: new Guid("02412b0a-ccf6-43e8-b9e0-5ad0a42142cb"));

            migrationBuilder.DeleteData(
                table: "Application",
                keyColumn: "Id",
                keyValue: new Guid("04aafaa0-609c-4bc8-b243-113e10d67406"));

            migrationBuilder.DeleteData(
                table: "Application",
                keyColumn: "Id",
                keyValue: new Guid("1c083e90-b3c0-4e81-a9d8-24f3c358bc53"));

            migrationBuilder.DeleteData(
                table: "Application",
                keyColumn: "Id",
                keyValue: new Guid("4e584071-735b-4561-bc4a-c83a25d424ff"));

            migrationBuilder.DeleteData(
                table: "Application",
                keyColumn: "Id",
                keyValue: new Guid("83f5a6e0-3c52-4783-b9c2-224235d2f313"));

            migrationBuilder.DeleteData(
                table: "Application",
                keyColumn: "Id",
                keyValue: new Guid("c3762b1e-a3d3-4a9c-a689-9dc2835a2ad1"));

            migrationBuilder.DeleteData(
                table: "Application",
                keyColumn: "Id",
                keyValue: new Guid("e06e4038-3ad9-47e4-9eaf-f3fa4c1b2155"));

            migrationBuilder.AlterColumn<Guid>(
                name: "PaymentMethodId",
                table: "Order",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "Application",
                columns: new[] { "Id", "Name", "OrderId", "Price" },
                values: new object[,]
                {
                    { new Guid("0d0b9ac8-b6df-448b-b20f-d091c8079eec"), "LightRoom", null, 15m },
                    { new Guid("1858b7f5-1377-4cd6-abe8-2a97789ef813"), "Maya", null, 25m },
                    { new Guid("18b0de87-e8e6-486a-b9bf-fb990359c57c"), "PhotoShop", null, 10m },
                    { new Guid("738b6cac-7c6d-493f-9abc-7810dde8a941"), "JetBrains Rider", null, 30m },
                    { new Guid("c65d7f55-f26c-4f72-b97c-dac8fce382f4"), "JetBrains IntelliJ", null, 35m },
                    { new Guid("e4723e74-0c85-4c5d-92bb-d7119fad55dd"), "SnapSeed", null, 20m },
                    { new Guid("eb47b6ad-d877-46fa-93f4-06e9c6bf16f7"), "JetBrains GoLang", null, 40m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Application",
                keyColumn: "Id",
                keyValue: new Guid("0d0b9ac8-b6df-448b-b20f-d091c8079eec"));

            migrationBuilder.DeleteData(
                table: "Application",
                keyColumn: "Id",
                keyValue: new Guid("1858b7f5-1377-4cd6-abe8-2a97789ef813"));

            migrationBuilder.DeleteData(
                table: "Application",
                keyColumn: "Id",
                keyValue: new Guid("18b0de87-e8e6-486a-b9bf-fb990359c57c"));

            migrationBuilder.DeleteData(
                table: "Application",
                keyColumn: "Id",
                keyValue: new Guid("738b6cac-7c6d-493f-9abc-7810dde8a941"));

            migrationBuilder.DeleteData(
                table: "Application",
                keyColumn: "Id",
                keyValue: new Guid("c65d7f55-f26c-4f72-b97c-dac8fce382f4"));

            migrationBuilder.DeleteData(
                table: "Application",
                keyColumn: "Id",
                keyValue: new Guid("e4723e74-0c85-4c5d-92bb-d7119fad55dd"));

            migrationBuilder.DeleteData(
                table: "Application",
                keyColumn: "Id",
                keyValue: new Guid("eb47b6ad-d877-46fa-93f4-06e9c6bf16f7"));

            migrationBuilder.AlterColumn<Guid>(
                name: "PaymentMethodId",
                table: "Order",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Application",
                columns: new[] { "Id", "Name", "OrderId", "Price" },
                values: new object[,]
                {
                    { new Guid("02412b0a-ccf6-43e8-b9e0-5ad0a42142cb"), "Maya", null, 25m },
                    { new Guid("04aafaa0-609c-4bc8-b243-113e10d67406"), "PhotoShop", null, 10m },
                    { new Guid("1c083e90-b3c0-4e81-a9d8-24f3c358bc53"), "JetBrains Rider", null, 30m },
                    { new Guid("4e584071-735b-4561-bc4a-c83a25d424ff"), "SnapSeed", null, 20m },
                    { new Guid("83f5a6e0-3c52-4783-b9c2-224235d2f313"), "JetBrains IntelliJ", null, 35m },
                    { new Guid("c3762b1e-a3d3-4a9c-a689-9dc2835a2ad1"), "LightRoom", null, 15m },
                    { new Guid("e06e4038-3ad9-47e4-9eaf-f3fa4c1b2155"), "JetBrains GoLang", null, 40m }
                });
        }
    }
}
