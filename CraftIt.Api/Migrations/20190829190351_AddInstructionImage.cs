using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CraftIt.Api.Migrations
{
    public partial class AddInstructionImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Instructions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Instructions");
        }
    }
}
