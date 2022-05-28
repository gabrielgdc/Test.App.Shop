using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test.App.Shop.Infra.Data.Migrations
{
    public partial class RenamedTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserGenders_GenderId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserGenders",
                table: "UserGenders");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "UserGenders",
                newName: "UserGender");

            migrationBuilder.RenameIndex(
                name: "IX_Users_GenderId",
                table: "User",
                newName: "IX_User_GenderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserGender",
                table: "UserGender",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_UserGender_GenderId",
                table: "User",
                column: "GenderId",
                principalTable: "UserGender",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_UserGender_GenderId",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserGender",
                table: "UserGender");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "UserGender",
                newName: "UserGenders");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_User_GenderId",
                table: "Users",
                newName: "IX_Users_GenderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserGenders",
                table: "UserGenders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserGenders_GenderId",
                table: "Users",
                column: "GenderId",
                principalTable: "UserGenders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
