using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRMVCProjectDataAccess.Migrations
{
    public partial class forcreditcard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "55e93504-9cec-44d8-9fae-292f53e68887");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "e3205ee2-3357-4633-a2f2-7ff323c1a256");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "fa8e8baa-66b7-4ff9-9daf-d49b7a033dfd");

            migrationBuilder.AlterColumn<string>(
                name: "PackageName",
                table: "Packages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Packages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CreditCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardNumber = table.Column<double>(type: "float", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CardBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCards", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "7f854a00-4712-4387-b9c8-e9d2132b4160");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "bc5dccd8-f073-43f0-a4b6-b2f6003573fb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "12ef69f6-87d8-4b9b-baf3-7014dd4f0be4");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1cefa75c-1361-4133-945d-11fd0371fa21", "20f4ea93-bc8f-47a8-8cfb-89dc86181ec1", "User", "USER" },
                    { "06becd82-7cbb-4844-b0c2-e8a41ec716ca", "45a40a40-4df6-4c9b-bbad-e36610ca4faa", "Manager", "MANAGER" },
                    { "9b1ce33a-66e0-41db-af75-4e350fa231bc", "e9e8c181-96b4-4961-b2c0-f9c04dca887c", "Admin", "ADMIN" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditCards");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "06becd82-7cbb-4844-b0c2-e8a41ec716ca");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "1cefa75c-1361-4133-945d-11fd0371fa21");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "9b1ce33a-66e0-41db-af75-4e350fa231bc");

            migrationBuilder.AlterColumn<string>(
                name: "PackageName",
                table: "Packages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Packages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "53d9be21-23ca-4771-9384-ce5b3efc0aed");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "a5b7f2b6-703b-48dd-87c8-1275aaa29c26");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "f12fcc07-72a5-4394-8d5a-a4d0c54819a1");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "fa8e8baa-66b7-4ff9-9daf-d49b7a033dfd", "7d564026-2c13-4350-8e8c-f83319e87d48", "User", "USER" },
                    { "e3205ee2-3357-4633-a2f2-7ff323c1a256", "8ba5dae2-d9bd-4a5c-9a26-35e37f61d5e4", "Manager", "MANAGER" },
                    { "55e93504-9cec-44d8-9fae-292f53e68887", "895749d1-1909-4ab0-b844-f09660028276", "Admin", "ADMIN" }
                });
        }
    }
}
