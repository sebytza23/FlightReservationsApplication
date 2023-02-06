using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightReservationsApplication.Migrations
{
    public partial class ReservationConfirmationShouldBeNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reservations_ReservationConfirmationID",
                table: "Reservations");

            migrationBuilder.AlterColumn<int>(
                name: "ReservationConfirmationID",
                table: "Reservations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ReservationConfirmationID",
                table: "Reservations",
                column: "ReservationConfirmationID",
                unique: true,
                filter: "[ReservationConfirmationID] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reservations_ReservationConfirmationID",
                table: "Reservations");

            migrationBuilder.AlterColumn<int>(
                name: "ReservationConfirmationID",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ReservationConfirmationID",
                table: "Reservations",
                column: "ReservationConfirmationID",
                unique: true);
        }
    }
}
