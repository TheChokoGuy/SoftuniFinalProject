
#nullable disable

namespace Project.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class UpdateItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ItemId",
                table: "Categories",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Items_ItemId",
                table: "Categories",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Items_ItemId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ItemId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Categories");
        }
    }
}
