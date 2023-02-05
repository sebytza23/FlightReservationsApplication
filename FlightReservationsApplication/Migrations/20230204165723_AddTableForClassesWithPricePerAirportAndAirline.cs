using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightReservationsApplication.Migrations
{
    public partial class AddTableForClassesWithPricePerAirportAndAirline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactInformationID = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsEmployee = table.Column<bool>(type: "bit", nullable: false),
                    EmployeeID = table.Column<int>(type: "int", nullable: true),
                    CustomerID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountID);
                });

            migrationBuilder.CreateTable(
                name: "Aircrafts",
                columns: table => new
                {
                    AircraftID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aircrafts", x => x.AircraftID);
                });

            migrationBuilder.CreateTable(
                name: "Airlines",
                columns: table => new
                {
                    AirlineID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airlines", x => x.AirlineID);
                });

            migrationBuilder.CreateTable(
                name: "Airports",
                columns: table => new
                {
                    AirportID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airports", x => x.AirportID);
                });

            migrationBuilder.CreateTable(
                name: "ContactInformations",
                columns: table => new
                {
                    ContactInformationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInformations", x => x.ContactInformationID);
                    table.ForeignKey(
                        name: "FK_ContactInformations_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreditCardID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                    table.ForeignKey(
                        name: "FK_Customers_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeID);
                    table.ForeignKey(
                        name: "FK_Employees_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    ClassID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", maxLength: 2, nullable: false),
                    AirportID = table.Column<int>(type: "int", nullable: false),
                    AirlineID = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.ClassID);
                    table.ForeignKey(
                        name: "FK_Classes_Airlines_AirlineID",
                        column: x => x.AirlineID,
                        principalTable: "Airlines",
                        principalColumn: "AirlineID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Classes_Airports_AirportID",
                        column: x => x.AirportID,
                        principalTable: "Airports",
                        principalColumn: "AirportID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    FlightID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AircraftID = table.Column<int>(type: "int", nullable: false),
                    AirlineID = table.Column<int>(type: "int", nullable: false),
                    DepartureAirportID = table.Column<int>(type: "int", nullable: false),
                    ArrivalAirportID = table.Column<int>(type: "int", nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstClassCapacity = table.Column<int>(type: "int", nullable: false),
                    EconomyClassCapacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.FlightID);
                    table.ForeignKey(
                        name: "FK_Flights_Aircrafts_AircraftID",
                        column: x => x.AircraftID,
                        principalTable: "Aircrafts",
                        principalColumn: "AircraftID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Flights_Airlines_AirlineID",
                        column: x => x.AirlineID,
                        principalTable: "Airlines",
                        principalColumn: "AirlineID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Flights_Airports_ArrivalAirportID",
                        column: x => x.ArrivalAirportID,
                        principalTable: "Airports",
                        principalColumn: "AirportID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Flights_Airports_DepartureAirportID",
                        column: x => x.DepartureAirportID,
                        principalTable: "Airports",
                        principalColumn: "AirportID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CreditCards",
                columns: table => new
                {
                    CreditCardID = table.Column<int>(type: "int", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    CardHolderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "Date", nullable: false),
                    SecurityCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCards", x => x.CreditCardID);
                    table.ForeignKey(
                        name: "FK_CreditCards_Customers_CreditCardID",
                        column: x => x.CreditCardID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservationConfirmations",
                columns: table => new
                {
                    ReservationConfirmationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationID = table.Column<int>(type: "int", nullable: false),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    ConfirmationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeclinedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationConfirmations", x => x.ReservationConfirmationID);
                    table.ForeignKey(
                        name: "FK_ReservationConfirmations_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalaryHistories",
                columns: table => new
                {
                    SalaryHistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    PreviousSalaryHistoryID = table.Column<int>(type: "int", nullable: true),
                    NextSalaryHistoryID = table.Column<int>(type: "int", nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryHistories", x => x.SalaryHistoryID);
                    table.ForeignKey(
                        name: "FK_SalaryHistories_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalaryHistories_SalaryHistories_NextSalaryHistoryID",
                        column: x => x.NextSalaryHistoryID,
                        principalTable: "SalaryHistories",
                        principalColumn: "SalaryHistoryID");
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    SeatID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeatNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassID = table.Column<int>(type: "int", nullable: false),
                    FlightID = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.SeatID);
                    table.ForeignKey(
                        name: "FK_Seats_Classes_ClassID",
                        column: x => x.ClassID,
                        principalTable: "Classes",
                        principalColumn: "ClassID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Seats_Flights_FlightID",
                        column: x => x.FlightID,
                        principalTable: "Flights",
                        principalColumn: "FlightID");
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    SeatID = table.Column<int>(type: "int", nullable: false),
                    ReservationConfirmationID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    FlightID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationID);
                    table.ForeignKey(
                        name: "FK_Reservations_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Flights_FlightID",
                        column: x => x.FlightID,
                        principalTable: "Flights",
                        principalColumn: "FlightID");
                    table.ForeignKey(
                        name: "FK_Reservations_ReservationConfirmations_ReservationConfirmationID",
                        column: x => x.ReservationConfirmationID,
                        principalTable: "ReservationConfirmations",
                        principalColumn: "ReservationConfirmationID");
                    table.ForeignKey(
                        name: "FK_Reservations_Seats_SeatID",
                        column: x => x.SeatID,
                        principalTable: "Seats",
                        principalColumn: "SeatID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classes_AirlineID",
                table: "Classes",
                column: "AirlineID");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_AirportID",
                table: "Classes",
                column: "AirportID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactInformations_AccountID",
                table: "ContactInformations",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AccountID",
                table: "Customers",
                column: "AccountID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_AccountID",
                table: "Employees",
                column: "AccountID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Flights_AircraftID",
                table: "Flights",
                column: "AircraftID");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_AirlineID",
                table: "Flights",
                column: "AirlineID");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_ArrivalAirportID",
                table: "Flights",
                column: "ArrivalAirportID");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_DepartureAirportID",
                table: "Flights",
                column: "DepartureAirportID");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationConfirmations_EmployeeID",
                table: "ReservationConfirmations",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CustomerID",
                table: "Reservations",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_FlightID",
                table: "Reservations",
                column: "FlightID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ReservationConfirmationID",
                table: "Reservations",
                column: "ReservationConfirmationID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_SeatID",
                table: "Reservations",
                column: "SeatID");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryHistories_EmployeeID",
                table: "SalaryHistories",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryHistories_NextSalaryHistoryID",
                table: "SalaryHistories",
                column: "NextSalaryHistoryID",
                unique: true,
                filter: "[NextSalaryHistoryID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_ClassID",
                table: "Seats",
                column: "ClassID");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_FlightID",
                table: "Seats",
                column: "FlightID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactInformations");

            migrationBuilder.DropTable(
                name: "CreditCards");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "SalaryHistories");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "ReservationConfirmations");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Aircrafts");

            migrationBuilder.DropTable(
                name: "Airlines");

            migrationBuilder.DropTable(
                name: "Airports");
        }
    }
}
