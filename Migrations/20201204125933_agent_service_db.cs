using Microsoft.EntityFrameworkCore.Migrations;

namespace ComaCuras.web.Migrations
{
    public partial class agent_service_db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgentService",
                columns: table => new
                {
                    AgentId = table.Column<int>(nullable: false),
                    ServiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentService", x => new { x.AgentId, x.ServiceId });
                    table.ForeignKey(
                        name: "FK_AgentService_Agent_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agent",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AgentService_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgentService_ServiceId",
                table: "AgentService",
                column: "ServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgentService");
        }
    }
}
