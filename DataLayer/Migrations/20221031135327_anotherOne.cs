using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class anotherOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    PaymentMethodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentMethodName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BackNr = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.PaymentMethodId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    TypesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.TypesId);
                });

            migrationBuilder.CreateTable(
                name: "UserInformations",
                columns: table => new
                {
                    UserInformationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInformations", x => x.UserInformationId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypesId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Types_TypesId",
                        column: x => x.TypesId,
                        principalTable: "Types",
                        principalColumn: "TypesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserInformationId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_UserInformations_UserInformationId",
                        column: x => x.UserInformationId,
                        principalTable: "UserInformations",
                        principalColumn: "UserInformationId");
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethodUsers",
                columns: table => new
                {
                    PaymentMethodId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethodUsers", x => new { x.UserId, x.PaymentMethodId });
                    table.ForeignKey(
                        name: "FK_PaymentMethodUsers_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "PaymentMethodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentMethodUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productUsers",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productUsers", x => new { x.UserId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_productUsers_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_productUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "SoftAdmin" },
                    { 3, "User" }
                });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "TypesId", "Name" },
                values: new object[,]
                {
                    { 1, "Keyboards" },
                    { 2, "GPU's" },
                    { 3, "CPU's" },
                    { 4, "Motherboard" }
                });

            migrationBuilder.InsertData(
                table: "UserInformations",
                columns: new[] { "UserInformationId", "City", "Street", "ZipCode" },
                values: new object[,]
                {
                    { 1, "Kolding", "Problembarngade 5", 6000 },
                    { 2, "Sønderborg", "Fyunkergade 2", 6400 },
                    { 3, "Aabenraa", "Sondsgade 4", 6300 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Brand", "ImageUrl", "IsDeleted", "Name", "Price", "TypesId" },
                values: new object[,]
                {
                    { 1, "Nvida", "Images\\EggFace.png", false, "3080 RTX Nvida", 10099.99m, 2 },
                    { 2, "Nvida", "Images\\DinboyWithGlasses.png", false, "3090 RTX Nvida", 15999.99m, 2 },
                    { 3, "LogiTech", "Images\\ducklingFace.png", false, "LogiTech Meistro Keyboard", 1599.99m, 1 },
                    { 4, "Asus", "Images\\gundoggo.png", false, "Asus Motherboard 3000x", 2599.99m, 4 },
                    { 5, "AMD", "Images\\suitdoggo.png", false, "AMD ThredRipper 9999x", 59999.99m, 3 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "IsDeleted", "Password", "RoleId", "UserInformationId", "UserName" },
                values: new object[,]
                {
                    { 1, "gigga@gmail.com", false, "P@ssw0rd", 1, 1, "Admin" },
                    { 2, "megaa@gmail.com", false, "kodeord", 3, null, "Rene" },
                    { 3, "behemoth@gmail.com", false, "kodeord", 3, null, "Benjamin" }
                });

            migrationBuilder.InsertData(
                table: "productUsers",
                columns: new[] { "ProductId", "UserId", "Quantity" },
                values: new object[] { 1, 1, 5 });

            migrationBuilder.InsertData(
                table: "productUsers",
                columns: new[] { "ProductId", "UserId", "Quantity" },
                values: new object[] { 1, 3, 5 });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethodUsers_PaymentMethodId",
                table: "PaymentMethodUsers",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_TypesId",
                table: "Products",
                column: "TypesId");

            migrationBuilder.CreateIndex(
                name: "IX_productUsers_ProductId",
                table: "productUsers",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserInformationId",
                table: "Users",
                column: "UserInformationId",
                unique: true,
                filter: "[UserInformationId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentMethodUsers");

            migrationBuilder.DropTable(
                name: "productUsers");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "UserInformations");
        }
    }
}
