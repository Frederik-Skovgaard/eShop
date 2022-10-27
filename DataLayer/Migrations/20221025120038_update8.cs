using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class update8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.RenameColumn(
                name: "PaymentName",
                table: "PaymentMethods",
                newName: "PaymentMethodName");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PaymentMethods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "Name",
                table: "PaymentMethods");

            migrationBuilder.RenameColumn(
                name: "PaymentMethodName",
                table: "PaymentMethods",
                newName: "PaymentName");
        }
    }
}
