using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRMVCProjectDataAccess.Migrations
{
    public partial class forwallet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "3353e891-0509-4c00-859c-e0f67f2937ef");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "52234128-e3ea-4462-9d52-5942989c77bd");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "92ef076a-31d0-4dd1-a4b5-debd4065e2f0");

            migrationBuilder.AddColumn<int>(
                name: "WalletId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "20a3b1d1-84b0-4cbc-b0c8-2e6ef957337f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "be9c43d4-49e0-4042-a60f-eac098b840b1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "a19ead3e-f319-4323-be9a-61faa540ee60");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c1a120e9-1aa6-415a-8e88-9993be4eec1a", "7250b5dd-98ae-447f-8570-00fb10fc9f34", "User", "USER" },
                    { "607c48de-a007-43ba-8774-611d079393a9", "5a08370f-0332-4cfa-9047-66b9cb081a83", "Manager", "MANAGER" },
                    { "9d407fb9-1bd7-408b-a167-4f2a5ad47d47", "5230c24b-540f-4e42-84c7-a5f8f8156dac", "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_WalletId",
                table: "AspNetUsers",
                column: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Wallets_WalletId",
                table: "AspNetUsers",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Wallets_WalletId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_WalletId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "607c48de-a007-43ba-8774-611d079393a9");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "9d407fb9-1bd7-408b-a167-4f2a5ad47d47");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "c1a120e9-1aa6-415a-8e88-9993be4eec1a");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "e368d0a3-6985-44d7-80bb-c4f8bf0daf5a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "2eb55cc8-95fa-4e3b-952e-7f0b7dfc8640");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "45a1a89d-0507-4de2-9235-b7396274e64e");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3353e891-0509-4c00-859c-e0f67f2937ef", "8ebbf1ef-f7ad-471e-ad96-976d9c615597", "User", "USER" },
                    { "92ef076a-31d0-4dd1-a4b5-debd4065e2f0", "da31f3db-0fae-4978-a26a-4f0fef0c4778", "Manager", "MANAGER" },
                    { "52234128-e3ea-4462-9d52-5942989c77bd", "6f7bc7d1-ca9c-486b-a557-1530820e56cb", "Admin", "ADMIN" }
                });
        }
    }
}
