using Microsoft.EntityFrameworkCore.Migrations;

namespace ILConfessions.API.Data.Migrations
{
    public partial class Added_UserId_Confession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Confessions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Confessions_UserId",
                table: "Confessions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Confessions_AspNetUsers_UserId",
                table: "Confessions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Confessions_AspNetUsers_UserId",
                table: "Confessions");

            migrationBuilder.DropIndex(
                name: "IX_Confessions_UserId",
                table: "Confessions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Confessions");
        }
    }
}
