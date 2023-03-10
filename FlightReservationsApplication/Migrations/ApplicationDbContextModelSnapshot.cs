// <auto-generated />
using System;
using FlightReservationsApplication.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FlightReservationsApplication.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FlightReservationsApplication.Models.Account", b =>
                {
                    b.Property<Guid>("AccountID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("ContactInformationID")
                        .HasColumnType("int");

                    b.Property<int?>("CustomerID")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsEmployee")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.HasKey("AccountID");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("FlightReservationsApplication.Models.Aircraft", b =>
                {
                    b.Property<int>("AircraftID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AircraftID"), 1L, 1);

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AircraftID");

                    b.ToTable("Aircrafts");
                });

            modelBuilder.Entity("FlightReservationsApplication.Models.Airline", b =>
                {
                    b.Property<int>("AirlineID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AirlineID"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AirlineID");

                    b.ToTable("Airlines");
                });

            modelBuilder.Entity("FlightReservationsApplication.Models.Airport", b =>
                {
                    b.Property<int>("AirportID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AirportID"), 1L, 1);

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AirportID");

                    b.ToTable("Airports");
                });

            modelBuilder.Entity("FlightReservationsApplication.Models.Class", b =>
                {
                    b.Property<int>("ClassID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClassID"), 1L, 1);

                    b.Property<int>("AirlineID")
                        .HasColumnType("int");

                    b.Property<int>("AirportID")
                        .HasColumnType("int");

                    b.Property<int>("Number")
                        .HasMaxLength(2)
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("ClassID");

                    b.HasIndex("AirlineID");

                    b.HasIndex("AirportID");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("FlightReservationsApplication.Models.ContactInformation", b =>
                {
                    b.Property<int>("ContactInformationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ContactInformationID"), 1L, 1);

                    b.Property<Guid>("AccountID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AddressLine1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressLine2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPrimary")
                        .HasColumnType("bit");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ContactInformationID");

                    b.HasIndex("AccountID");

                    b.ToTable("ContactInformations");
                });

            modelBuilder.Entity("FlightReservationsApplication.Models.CreditCard", b =>
                {
                    b.Property<int>("CreditCardID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CardHolderName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("Date");

                    b.Property<bool>("IsPrimary")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityCode")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.HasKey("CreditCardID");

                    b.ToTable("CreditCards");
                });

            modelBuilder.Entity("FlightReservationsApplication.Models.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerID"), 1L, 1);

                    b.Property<Guid>("AccountID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("CreditCardID")
                        .HasColumnType("int");

                    b.HasKey("CustomerID");

                    b.HasIndex("AccountID")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("FlightReservationsApplication.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeID"), 1L, 1);

                    b.Property<Guid>("AccountID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.HasKey("EmployeeID");

                    b.HasIndex("AccountID")
                        .IsUnique();

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("FlightReservationsApplication.Models.Flight", b =>
                {
                    b.Property<int>("FlightID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FlightID"), 1L, 1);

                    b.Property<int>("AircraftID")
                        .HasColumnType("int");

                    b.Property<int>("AirlineID")
                        .HasColumnType("int");

                    b.Property<int>("ArrivalAirportID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ArrivalTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("DepartureAirportID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DepartureTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("EconomyClassCapacity")
                        .HasColumnType("int");

                    b.Property<int>("FirstClassCapacity")
                        .HasColumnType("int");

                    b.HasKey("FlightID");

                    b.HasIndex("AircraftID");

                    b.HasIndex("AirlineID");

                    b.HasIndex("ArrivalAirportID");

                    b.HasIndex("DepartureAirportID");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("FlightReservationsApplication.Models.Reservation", b =>
                {
                    b.Property<int>("ReservationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReservationID"), 1L, 1);

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<int?>("FlightID")
                        .HasColumnType("int");

                    b.Property<int?>("ReservationConfirmationID")
                        .HasColumnType("int");

                    b.Property<int>("SeatID")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("ReservationID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("FlightID");

                    b.HasIndex("ReservationConfirmationID")
                        .IsUnique()
                        .HasFilter("[ReservationConfirmationID] IS NOT NULL");

                    b.HasIndex("SeatID");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("FlightReservationsApplication.Models.ReservationConfirmation", b =>
                {
                    b.Property<int>("ReservationConfirmationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReservationConfirmationID"), 1L, 1);

                    b.Property<DateTime?>("ConfirmationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeclinedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReservationID")
                        .HasColumnType("int");

                    b.HasKey("ReservationConfirmationID");

                    b.HasIndex("EmployeeID");

                    b.ToTable("ReservationConfirmations");
                });

            modelBuilder.Entity("FlightReservationsApplication.Models.SalaryHistory", b =>
                {
                    b.Property<int>("SalaryHistoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SalaryHistoryID"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("EffectiveDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<int?>("NextSalaryHistoryID")
                        .HasColumnType("int");

                    b.Property<int?>("PreviousSalaryHistoryID")
                        .HasColumnType("int");

                    b.HasKey("SalaryHistoryID");

                    b.HasIndex("EmployeeID");

                    b.HasIndex("NextSalaryHistoryID")
                        .IsUnique()
                        .HasFilter("[NextSalaryHistoryID] IS NOT NULL");

                    b.ToTable("SalaryHistories");
                });

            modelBuilder.Entity("FlightReservationsApplication.Models.Seat", b =>
                {
                    b.Property<int>("SeatID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SeatID"), 1L, 1);

                    b.Property<int>("ClassID")
                        .HasColumnType("int");

                    b.Property<int>("FlightID")
                        .HasColumnType("int");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<int>("SeatNumber")
                        .HasColumnType("int");

                    b.HasKey("SeatID");

                    b.HasIndex("ClassID");

                    b.HasIndex("FlightID");

                    b.ToTable("Seats");
                });

            modelBuilder.Entity("FlightReservationsApplication.Models.Class", b =>
                {
                    b.HasOne("FlightReservationsApplication.Models.Airline", "Airline")
                        .WithMany("Classes")
                        .HasForeignKey("AirlineID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlightReservationsApplication.Models.Airport", "Airport")
                        .WithMany("Classes")
                        .HasForeignKey("AirportID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Airline");

                    b.Navigation("Airport");
                });

            modelBuilder.Entity("FlightReservationsApplication.Models.ContactInformation", b =>
                {
                    b.HasOne("FlightReservationsApplication.Models.Account", "Account")
                        .WithMany("ContactInformations")
                        .HasForeignKey("AccountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("FlightReservationsApplication.Models.CreditCard", b =>
                {
                    b.HasOne("FlightReservationsApplication.Models.Customer", "Customer")
                        .WithMany("CreditCards")
                        .HasForeignKey("CreditCardID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("FlightReservationsApplication.Models.Customer", b =>
                {
                    b.HasOne("FlightReservationsApplication.Models.Account", "Account")
                        .WithOne("Customer")
                        .HasForeignKey("FlightReservationsApplication.Models.Customer", "AccountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("FlightReservationsApplication.Models.Employee", b =>
                {
                    b.HasOne("FlightReservationsApplication.Models.Account", "Account")
                        .WithOne("Employee")
                        .HasForeignKey("FlightReservationsApplication.Models.Employee", "AccountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("FlightReservationsApplication.Models.Flight", b =>
                {
                    b.HasOne("FlightReservationsApplication.Models.Aircraft", "Aircraft")
                        .WithMany("Flights")
                        .HasForeignKey("AircraftID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlightReservationsApplication.Models.Airline", "Airline")
                        .WithMany("Flights")
                        .HasForeignKey("AirlineID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlightReservationsApplication.Models.Airport", "ArrivalAirport")
                        .WithMany("ArrivingFlights")
                        .HasForeignKey("ArrivalAirportID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FlightReservationsApplication.Models.Airport", "DepartureAirport")
                        .WithMany("DepartingFlights")
                        .HasForeignKey("DepartureAirportID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Aircraft");

                    b.Navigation("Airline");

                    b.Navigation("ArrivalAirport");

                    b.Navigation("DepartureAirport");
                });

            modelBuilder.Entity("FlightReservationsApplication.Models.Reservation", b =>
                {
                    b.HasOne("FlightReservationsApplication.Models.Customer", "Customer")
                        .WithMany("Reservations")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlightReservationsApplication.Models.Flight", null)
                        .WithMany("Reservations")
                        .HasForeignKey("FlightID");

                    b.HasOne("FlightReservationsApplication.Models.ReservationConfirmation", "ReservationConfirmation")
                        .WithOne("Reservation")
                        .HasForeignKey("FlightReservationsApplication.Models.Reservation", "ReservationConfirmationID")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("FlightReservationsApplication.Models.Seat", "Seat")
                        .WithMany("Reservations")
                        .HasForeignKey("SeatID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("ReservationConfirmation");

                    b.Navigation("Seat");
                });

            modelBuilder.Entity("FlightReservationsApplication.Models.ReservationConfirmation", b =>
                {
                    b.HasOne("FlightReservationsApplication.Models.Employee", "Employee")
                        .WithMany("ReservationConfirmations")
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("FlightReservationsApplication.Models.SalaryHistory", b =>
                {
                    b.HasOne("FlightReservationsApplication.Models.Employee", "Employee")
                        .WithMany("SalaryHistories")
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlightReservationsApplication.Models.SalaryHistory", "NextSalaryHistory")
                        .WithOne("PreviousSalaryHistory")
                        .HasForeignKey("FlightReservationsApplication.Models.SalaryHistory", "NextSalaryHistoryID");

                    b.Navigation("Employee");

                    b.Navigation("NextSalaryHistory");
                });

            modelBuilder.Entity("FlightReservationsApplication.Models.Seat", b =>
                {
                    b.HasOne("FlightReservationsApplication.Models.Class", "Class")
                        .WithMany("Seats")
                        .HasForeignKey("ClassID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlightReservationsApplication.Models.Flight", "Flight")
                        .WithMany("Seats")
                        .HasForeignKey("FlightID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Class");

                    b.Navigation("Flight");
                });

            modelBuilder.Entity("FlightReservationsApplication.Models.Account", b =>
                {
                    b.Navigation("ContactInformations");

                    b.Navigation("Customer");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("FlightReservationsApplication.Models.Aircraft", b =>
                {
                    b.Navigation("Flights");
                });

            modelBuilder.Entity("FlightReservationsApplication.Models.Airline", b =>
                {
                    b.Navigation("Classes");

                    b.Navigation("Flights");
                });

            modelBuilder.Entity("FlightReservationsApplication.Models.Airport", b =>
                {
                    b.Navigation("ArrivingFlights");

                    b.Navigation("Classes");

                    b.Navigation("DepartingFlights");
                });

            modelBuilder.Entity("FlightReservationsApplication.Models.Class", b =>
                {
                    b.Navigation("Seats");
                });

            modelBuilder.Entity("FlightReservationsApplication.Models.Customer", b =>
                {
                    b.Navigation("CreditCards");

                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("FlightReservationsApplication.Models.Employee", b =>
                {
                    b.Navigation("ReservationConfirmations");

                    b.Navigation("SalaryHistories");
                });

            modelBuilder.Entity("FlightReservationsApplication.Models.Flight", b =>
                {
                    b.Navigation("Reservations");

                    b.Navigation("Seats");
                });

            modelBuilder.Entity("FlightReservationsApplication.Models.ReservationConfirmation", b =>
                {
                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("FlightReservationsApplication.Models.SalaryHistory", b =>
                {
                    b.Navigation("PreviousSalaryHistory");
                });

            modelBuilder.Entity("FlightReservationsApplication.Models.Seat", b =>
                {
                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
