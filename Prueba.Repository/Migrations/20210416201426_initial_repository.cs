using Microsoft.EntityFrameworkCore.Migrations;

namespace Prueba.Repository.Migrations
{
    public partial class initial_repository : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Pin",
                table: "Cards",
                newName: "Pan");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Pan",
                table: "Cards",
                newName: "Pin");
        }
    }
}
