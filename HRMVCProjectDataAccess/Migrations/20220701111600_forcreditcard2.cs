using Microsoft.EntityFrameworkCore.Migrations;

namespace HRMVCProjectDataAccess.Migrations
{
    public partial class forcreditcard2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "CreditCards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "f46b201d-f0ef-4c47-bf16-6b59095e6a09");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "7432cbd5-db9f-4001-8782-c7808c2e9c73");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "01da374f-795c-4ee7-828c-280d75896507");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5c708173-23a1-4e03-8234-cf8048cba6a1", "1f5ef3b0-997f-4a72-971e-4e21d8711270", "User", "USER" },
                    { "a7ef3b40-224c-4b11-a06e-cb79684b0f4e", "7a4cb270-7647-48a7-a412-a5b94f694431", "Manager", "MANAGER" },
                    { "9fa24a6d-421e-4ea7-9b55-b8aa5586e4d1", "37fb7578-fc5e-4b67-9bd0-4d3e620478e3", "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_EmployeeId",
                table: "CreditCards",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCards_AspNetUsers_EmployeeId",
                table: "CreditCards",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditCards_AspNetUsers_EmployeeId",
                table: "CreditCards");

            migrationBuilder.DropIndex(
                name: "IX_CreditCards_EmployeeId",
                table: "CreditCards");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "5c708173-23a1-4e03-8234-cf8048cba6a1");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "9fa24a6d-421e-4ea7-9b55-b8aa5586e4d1");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "a7ef3b40-224c-4b11-a06e-cb79684b0f4e");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "CreditCards");

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
    }
}
