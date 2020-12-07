using Microsoft.EntityFrameworkCore.Migrations;

namespace ComaCuras.web.Migrations
{
    public partial class shop_relations_config : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Shop");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Shop",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
