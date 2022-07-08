using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRMVCProjectDataAccess.Migrations
{
    public partial class init_package : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<int>(
                name: "PackageId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PackagePhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    NumberOfEmployee = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "8e558262-2017-4650-bd14-803a8886ee1f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "3b4bc97f-1fce-466e-869c-3faa91e0c3d1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "8df459e6-34b9-4e52-bda6-8386bfbe9805");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "eb82382a-1b33-48d5-9214-6b56ea0d5f74", "d8b68236-8e4f-48db-97f7-0ffb63ccdc9d", "User", "USER" },
                    { "804cd41f-88c6-439d-abe5-c54255e1e88c", "23828650-3669-4f79-9eb4-57054cb98e5a", "Manager", "MANAGER" },
                    { "7e1b6427-c0ef-477a-8353-2613705a7ca2", "aae03e49-db2a-4575-b94b-64496f4ca9bc", "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PackageId",
                table: "AspNetUsers",
                column: "PackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Packages_PackageId",
                table: "AspNetUsers",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Packages_PackageId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PackageId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "7e1b6427-c0ef-477a-8353-2613705a7ca2");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "804cd41f-88c6-439d-abe5-c54255e1e88c");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "eb82382a-1b33-48d5-9214-6b56ea0d5f74");

            migrationBuilder.DropColumn(
                name: "PackageId",
                table: "AspNetUsers");

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
        }
    }
}
