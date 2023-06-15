using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _777.Migrations
{
    public partial class mig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Texts");

            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "InspireMessages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Texts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "InspireMessages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
