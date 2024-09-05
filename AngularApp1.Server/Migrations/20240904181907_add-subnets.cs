using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AngularApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class addsubnets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0333cbb9-aefc-4cdb-93fb-a74f32ec83fe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a0c308db-12f8-46ea-a8ac-17a7c7cb4a90");

            migrationBuilder.CreateTable(
                name: "Subnet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SubnetCIDR = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subnet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserSubnet",
                columns: table => new
                {
                    AppUserId = table.Column<string>(type: "TEXT", nullable: false),
                    SubnetsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserSubnet", x => new { x.AppUserId, x.SubnetsId });
                    table.ForeignKey(
                        name: "FK_AppUserSubnet_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserSubnet_Subnet_SubnetsId",
                        column: x => x.SubnetsId,
                        principalTable: "Subnet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IpAddress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IpAddressString = table.Column<string>(type: "TEXT", nullable: false),
                    SubnetId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IpAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IpAddress_Subnet_SubnetId",
                        column: x => x.SubnetId,
                        principalTable: "Subnet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a317a445-cc97-40e6-94c8-74fcc9479524", null, "Admin", "ADMIN" },
                    { "be3e144c-1ba6-451c-94e0-78519a5e6e5e", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserSubnet_SubnetsId",
                table: "AppUserSubnet",
                column: "SubnetsId");

            migrationBuilder.CreateIndex(
                name: "IX_IpAddress_SubnetId",
                table: "IpAddress",
                column: "SubnetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserSubnet");

            migrationBuilder.DropTable(
                name: "IpAddress");

            migrationBuilder.DropTable(
                name: "Subnet");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a317a445-cc97-40e6-94c8-74fcc9479524");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "be3e144c-1ba6-451c-94e0-78519a5e6e5e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0333cbb9-aefc-4cdb-93fb-a74f32ec83fe", null, "Admin", "ADMIN" },
                    { "a0c308db-12f8-46ea-a8ac-17a7c7cb4a90", null, "User", "USER" }
                });
        }
    }
}
