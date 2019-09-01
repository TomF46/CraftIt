using Microsoft.EntityFrameworkCore.Migrations;

namespace CraftIt.Api.Migrations
{
    public partial class AddUserAddedByToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddedById",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_AddedById",
                table: "Products",
                column: "AddedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_AddedById",
                table: "Products",
                column: "AddedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_AddedById",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_AddedById",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AddedById",
                table: "Products");
        }
    }
}
