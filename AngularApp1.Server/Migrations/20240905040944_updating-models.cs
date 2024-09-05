using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AngularApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class updatingmodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserSubnet_AspNetUsers_AppUserId",
                table: "AppUserSubnet");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserSubnet_Subnet_SubnetsId",
                table: "AppUserSubnet");

            migrationBuilder.DropForeignKey(
                name: "FK_IpAddress_Subnet_SubnetId",
                table: "IpAddress");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subnet",
                table: "Subnet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IpAddress",
                table: "IpAddress");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a317a445-cc97-40e6-94c8-74fcc9479524");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "be3e144c-1ba6-451c-94e0-78519a5e6e5e");

            migrationBuilder.RenameTable(
                name: "Subnet",
                newName: "Subnets");

            migrationBuilder.RenameTable(
                name: "IpAddress",
                newName: "IpAddresses");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "AppUserSubnet",
                newName: "OwnersId");

            migrationBuilder.RenameIndex(
                name: "IX_IpAddress_SubnetId",
                table: "IpAddresses",
                newName: "IX_IpAddresses_SubnetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subnets",
                table: "Subnets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IpAddresses",
                table: "IpAddresses",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1f5bea30-d97a-4311-ab5b-b14fb8b063ad", null, "User", "USER" },
                    { "720716f2-fde6-4b8f-9a29-eccadfec261b", null, "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserSubnet_AspNetUsers_OwnersId",
                table: "AppUserSubnet",
                column: "OwnersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserSubnet_Subnets_SubnetsId",
                table: "AppUserSubnet",
                column: "SubnetsId",
                principalTable: "Subnets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IpAddresses_Subnets_SubnetId",
                table: "IpAddresses",
                column: "SubnetId",
                principalTable: "Subnets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserSubnet_AspNetUsers_OwnersId",
                table: "AppUserSubnet");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserSubnet_Subnets_SubnetsId",
                table: "AppUserSubnet");

            migrationBuilder.DropForeignKey(
                name: "FK_IpAddresses_Subnets_SubnetId",
                table: "IpAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subnets",
                table: "Subnets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IpAddresses",
                table: "IpAddresses");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1f5bea30-d97a-4311-ab5b-b14fb8b063ad");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "720716f2-fde6-4b8f-9a29-eccadfec261b");

            migrationBuilder.RenameTable(
                name: "Subnets",
                newName: "Subnet");

            migrationBuilder.RenameTable(
                name: "IpAddresses",
                newName: "IpAddress");

            migrationBuilder.RenameColumn(
                name: "OwnersId",
                table: "AppUserSubnet",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_IpAddresses_SubnetId",
                table: "IpAddress",
                newName: "IX_IpAddress_SubnetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subnet",
                table: "Subnet",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IpAddress",
                table: "IpAddress",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a317a445-cc97-40e6-94c8-74fcc9479524", null, "Admin", "ADMIN" },
                    { "be3e144c-1ba6-451c-94e0-78519a5e6e5e", null, "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserSubnet_AspNetUsers_AppUserId",
                table: "AppUserSubnet",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserSubnet_Subnet_SubnetsId",
                table: "AppUserSubnet",
                column: "SubnetsId",
                principalTable: "Subnet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IpAddress_Subnet_SubnetId",
                table: "IpAddress",
                column: "SubnetId",
                principalTable: "Subnet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
