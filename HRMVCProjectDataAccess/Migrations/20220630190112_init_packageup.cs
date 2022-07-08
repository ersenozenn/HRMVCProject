using Microsoft.EntityFrameworkCore.Migrations;

namespace HRMVCProjectDataAccess.Migrations
{
    public partial class init_packageup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Packages",
                type: "nvarchar(max)",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Packages");

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
        }
    }
}
