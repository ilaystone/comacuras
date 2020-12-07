using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComaCuras.web.Migrations
{
    public partial class agent_contains_image_directly : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Picture");

            migrationBuilder.DropColumn(
                name: "PictureId",
                table: "Agent");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Agent",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Agent");

            migrationBuilder.AddColumn<int>(
                name: "PictureId",
                table: "Agent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Picture",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picture", x => x.Id);
                });
        }
    }
}
