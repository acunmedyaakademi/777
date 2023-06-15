using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _777.Migrations
{
    public partial class mig5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Texts");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "InspireMessages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Texts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "InspireMessages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
