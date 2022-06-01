using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test.App.Shop.Infra.Data.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<decimal>(
                name: "Limit",
                table: "PaymentMethod",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldMaxLength: 25);

            migrationBuilder.InsertData(
                table: "Application",
                columns: new[] { "Id", "Name", "OrderId", "Price" },
                values: new object[,]
                {
                    { new Guid("0b96ba04-a755-4655-8b6f-0d09a36bdcbd"), "Maya", null, 25m },
                    { new Guid("24b46734-ce0b-489e-9a58-3beea5a409dc"), "JetBrains GoLang", null, 40m },
                    { new Guid("2f25f744-1dc2-46d1-b6fa-7c657516820b"), "LightRoom", null, 15m },
                    { new Guid("618e1d34-8ae3-4efe-a017-17b5818a3469"), "JetBrains IntelliJ", null, 35m },
                    { new Guid("6460866f-cd9f-4bf2-ac45-2bcb90049953"), "PhotoShop", null, 10m },
                    { new Guid("71d60834-fffb-425e-9be7-95344a471ecd"), "JetBrains Rider", null, 30m },
                    { new Guid("d6ba51d8-6993-4204-971a-e35ade28230d"), "SnapSeed", null, 20m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Application",
                keyColumn: "Id",
                keyValue: new Guid("0b96ba04-a755-4655-8b6f-0d09a36bdcbd"));

            migrationBuilder.DeleteData(
                table: "Application",
                keyColumn: "Id",
                keyValue: new Guid("24b46734-ce0b-489e-9a58-3beea5a409dc"));

            migrationBuilder.DeleteData(
                table: "Application",
                keyColumn: "Id",
                keyValue: new Guid("2f25f744-1dc2-46d1-b6fa-7c657516820b"));

            migrationBuilder.DeleteData(
                table: "Application",
                keyColumn: "Id",
                keyValue: new Guid("618e1d34-8ae3-4efe-a017-17b5818a3469"));

            migrationBuilder.DeleteData(
                table: "Application",
                keyColumn: "Id",
                keyValue: new Guid("6460866f-cd9f-4bf2-ac45-2bcb90049953"));

            migrationBuilder.DeleteData(
                table: "Application",
                keyColumn: "Id",
                keyValue: new Guid("71d60834-fffb-425e-9be7-95344a471ecd"));

            migrationBuilder.DeleteData(
                table: "Application",
                keyColumn: "Id",
                keyValue: new Guid("d6ba51d8-6993-4204-971a-e35ade28230d"));

            migrationBuilder.AlterColumn<decimal>(
                name: "Limit",
                table: "PaymentMethod",
                type: "decimal(18,2)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

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
    }
}
