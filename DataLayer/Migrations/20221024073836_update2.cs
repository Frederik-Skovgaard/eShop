using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductUser_Products_ProductId",
                table: "ProductUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductUser_Users_UserId",
                table: "ProductUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductUser",
                table: "ProductUser");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.RenameTable(
                name: "ProductUser",
                newName: "productUsers");

            migrationBuilder.RenameIndex(
                name: "IX_ProductUser_ProductId",
                table: "productUsers",
                newName: "IX_productUsers_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_productUsers",
                table: "productUsers",
                columns: new[] { "UserId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_productUsers_Products_ProductId",
                table: "productUsers",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productUsers_Users_UserId",
                table: "productUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productUsers_Products_ProductId",
                table: "productUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_productUsers_Users_UserId",
                table: "productUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_productUsers",
                table: "productUsers");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.RenameTable(
                name: "productUsers",
                newName: "ProductUser");

            migrationBuilder.RenameIndex(
                name: "IX_productUsers_ProductId",
                table: "ProductUser",
                newName: "IX_ProductUser_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductUser",
                table: "ProductUser",
                columns: new[] { "UserId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductUser_Products_ProductId",
                table: "ProductUser",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductUser_Users_UserId",
                table: "ProductUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
