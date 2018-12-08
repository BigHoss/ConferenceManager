using Microsoft.EntityFrameworkCore.Migrations;

namespace ConferenceManager.Database.Migrations
{
    public partial class updatescounteradded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Updates",
                table: "TimeSlots",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Updates",
                table: "Speakers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Updates",
                table: "Rooms",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Updates",
                table: "Days",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Updates",
                table: "Conferences",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Updates",
                table: "TimeSlots");

            migrationBuilder.DropColumn(
                name: "Updates",
                table: "Speakers");

            migrationBuilder.DropColumn(
                name: "Updates",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Updates",
                table: "Days");

            migrationBuilder.DropColumn(
                name: "Updates",
                table: "Conferences");
        }
    }
}
