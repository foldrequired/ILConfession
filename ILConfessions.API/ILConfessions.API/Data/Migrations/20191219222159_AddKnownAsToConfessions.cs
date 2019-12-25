using Microsoft.EntityFrameworkCore.Migrations;

namespace ILConfessions.API.Migrations
{
    public partial class AddKnownAsToConfessions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KnownAs",
                table: "Confessions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KnownAs",
                table: "Confessions");
        }
    }
}
