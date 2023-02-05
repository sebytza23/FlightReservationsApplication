using FlightReservationsApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationsApplication.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<ContactInformation> ContactInformations { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<ReservationConfirmation> ReservationConfirmations { get; set; }
        public DbSet<SalaryHistory> SalaryHistories { get; set; }
        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Class> Classes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=FlightReservations;Trusted_Connection=True;Trust Server Certificate=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasMany(a => a.ContactInformations)
                .WithOne(c => c.Account)
                .HasForeignKey(c => c.AccountID);

            modelBuilder.Entity<Account>()
                .HasOne(a => a.Employee)
                .WithOne(e => e.Account)
                .HasForeignKey<Employee>(e => e.AccountID);

            modelBuilder.Entity<Account>()
                .HasOne(a => a.Customer)
                .WithOne(c => c.Account)
                .HasForeignKey<Customer>(c => c.AccountID);

            modelBuilder.Entity<Aircraft>()
                .HasMany(a => a.Flights)
                .WithOne(f => f.Aircraft)
                .HasForeignKey(f => f.AircraftID);

            modelBuilder.Entity<Airline>()
                .HasMany(a => a.Flights)
                .WithOne(f => f.Airline)
                .HasForeignKey(f => f.AirlineID);

            modelBuilder.Entity<Airport>()
                .HasMany(a => a.ArrivingFlights)
                .WithOne(f => f.ArrivalAirport)
                .HasForeignKey(f => f.ArrivalAirportID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Airport>()
                .HasMany(a => a.DepartingFlights)
                .WithOne(f => f.DepartureAirport)
                .HasForeignKey(f => f.DepartureAirportID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CreditCard>()
                .HasOne(c => c.Customer)
                .WithMany(cu => cu.CreditCards)
                .HasForeignKey(cu => cu.CreditCardID);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Reservations)
                .WithOne(r => r.Customer)
                .HasForeignKey(r => r.CustomerID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.ReservationConfirmations)
                .WithOne(rc => rc.Employee)
                .HasForeignKey(rc => rc.EmployeeID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.SalaryHistories)
                .WithOne(sh => sh.Employee)
                .HasForeignKey(sh => sh.EmployeeID);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Seat)
                .WithMany()
                .HasForeignKey(r => r.SeatID);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.ReservationConfirmation)
                .WithOne(rc => rc.Reservation)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ReservationConfirmation>()
                 .HasOne(rc => rc.Reservation)
                 .WithOne(r => r.ReservationConfirmation)
                 .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SalaryHistory>()
                .HasOne(sh => sh.PreviousSalaryHistory)
                .WithOne(sh => sh.NextSalaryHistory)
                .HasForeignKey<SalaryHistory>(sh => sh.PreviousSalaryHistoryID);

            modelBuilder.Entity<SalaryHistory>()
                .HasOne(sh => sh.NextSalaryHistory)
                .WithOne(sh => sh.PreviousSalaryHistory)
                .HasForeignKey<SalaryHistory>(sh => sh.NextSalaryHistoryID);

            modelBuilder.Entity<Flight>()
                .HasMany(f => f.Seats)
                .WithOne(s => s.Flight)
                .HasForeignKey(s => s.FlightID);

            modelBuilder.Entity<Seat>()
                .HasOne(s => s.Flight)
                .WithMany(f => f.Seats)
                .HasForeignKey(s => s.FlightID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Seat>()
                .HasMany(s => s.Reservations)
                .WithOne(r => r.Seat)
                .HasForeignKey(r => r.SeatID);

            modelBuilder.Entity<Seat>()
                .HasOne(s => s.Class)
                .WithMany(c => c.Seats)
                .HasForeignKey(s => s.ClassID);

            modelBuilder.Entity<Class>()
                .HasOne(s => s.Airport)
                .WithMany(air => air.Classes)
                .HasForeignKey(s => s.AirportID);

            modelBuilder.Entity<Class>()
                .HasOne(s=>s.Airline)
                .WithMany(air => air.Classes)
                .HasForeignKey(s => s.AirlineID);
        }
    }
}