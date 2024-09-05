using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AngularApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class updatesubnet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1f5bea30-d97a-4311-ab5b-b14fb8b063ad");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "720716f2-fde6-4b8f-9a29-eccadfec261b");

            migrationBuilder.AddColumn<string>(
                name: "FirstIpAddress",
                table: "Subnets",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8a4b3247-e9e3-437c-9832-57bf94beb777", null, "User", "USER" },
                    { "e8c52fa7-bbb2-403a-b7ad-2d9e6e6a8fd9", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8a4b3247-e9e3-437c-9832-57bf94beb777");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e8c52fa7-bbb2-403a-b7ad-2d9e6e6a8fd9");

            migrationBuilder.DropColumn(
                name: "FirstIpAddress",
                table: "Subnets");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1f5bea30-d97a-4311-ab5b-b14fb8b063ad", null, "User", "USER" },
                    { "720716f2-fde6-4b8f-9a29-eccadfec261b", null, "Admin", "ADMIN" }
                });
        }
    }
}
