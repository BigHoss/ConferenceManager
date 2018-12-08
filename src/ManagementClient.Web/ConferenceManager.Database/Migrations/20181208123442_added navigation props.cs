using Microsoft.EntityFrameworkCore.Migrations;

namespace ConferenceManager.Database.Migrations
{
    public partial class addednavigationprops : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Conferences_ConferenceId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Speakers_TimeSlots_TimeSlotId",
                table: "Speakers");

            migrationBuilder.DropIndex(
                name: "IX_Speakers_TimeSlotId",
                table: "Speakers");

            migrationBuilder.AddColumn<int>(
                name: "SpeakerId",
                table: "TimeSlots",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ConferenceId",
                table: "Rooms",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_SpeakerId",
                table: "TimeSlots",
                column: "SpeakerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Conferences_ConferenceId",
                table: "Rooms",
                column: "ConferenceId",
                principalTable: "Conferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlots_Speakers_SpeakerId",
                table: "TimeSlots",
                column: "SpeakerId",
                principalTable: "Speakers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Conferences_ConferenceId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlots_Speakers_SpeakerId",
                table: "TimeSlots");

            migrationBuilder.DropIndex(
                name: "IX_TimeSlots_SpeakerId",
                table: "TimeSlots");

            migrationBuilder.DropColumn(
                name: "SpeakerId",
                table: "TimeSlots");

            migrationBuilder.AlterColumn<int>(
                name: "ConferenceId",
                table: "Rooms",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Speakers_TimeSlotId",
                table: "Speakers",
                column: "TimeSlotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Conferences_ConferenceId",
                table: "Rooms",
                column: "ConferenceId",
                principalTable: "Conferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Speakers_TimeSlots_TimeSlotId",
                table: "Speakers",
                column: "TimeSlotId",
                principalTable: "TimeSlots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
