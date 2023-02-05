using FlightReservationsApplication.Controllers;
using FlightReservationsApplication.Models;
using FlightReservationsApplication.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Configuration;
using System.Drawing.Printing;
using System.Security.Cryptography;

namespace FlightReservationsApplication.Context
{
    public static class ModelBuilderExtensions
    {
        public static async void Seed(ApplicationDbContext context)
        {
            //Conturi
            Account account1 = new Account { AccountID = Guid.NewGuid(), FirstName = "Marin-Eusebiu", LastName = "Serban", Email = "sebby.serban@yahoo.com", Password = "Sebytza23.", IsEmployee = true };
            Account account2 = new Account { AccountID = Guid.NewGuid(), FirstName = "Marin-Eusebiu", LastName = "Test", Email = "test.serban@yahoo.com", Password = "Sebytza23.", IsEmployee = false };
            context.Accounts.Add(account1);
            context.Accounts.Add(account2);
            context.SaveChanges();
            // Clienti
            Customer customer = new Customer { AccountID = account2.AccountID };
            await context.Customers.AddAsync(customer);
            // Angajati
            Employee employee = new Employee { AccountID = account2.AccountID, IsAdmin = true };
            await context.Employees.AddAsync(employee);
            account2.CustomerID = customer.CustomerID;
            account1.EmployeeID = employee.EmployeeID;
            context.Accounts.Update(account1);
            context.Accounts.Update(account2);
            context.SaveChanges();
            //Avioane
            await context.Aircrafts.AddAsync(new Aircraft { Model = "Boeing 747", Capacity = 489 });
            await context.Aircrafts.AddAsync(new Aircraft { Model = "Boeing 747", Capacity = 489 });
            await context.Aircrafts.AddAsync(new Aircraft { Model = "Airbus A380", Capacity = 853 });
            await context.Aircrafts.AddAsync(new Aircraft { Model = "Boeing 777", Capacity = 334 });
            await context.Aircrafts.AddAsync(new Aircraft { Model = "Airbus A320", Capacity = 165 });
            await context.Aircrafts.AddAsync(new Aircraft { Model = "Boeing 787 Dreamliner", Capacity = 286 });
            await context.Aircrafts.AddAsync(new Aircraft { Model = "Airbus A350", Capacity = 440 });
            await context.Aircrafts.AddAsync(new Aircraft { Model = "Embraer E-Jet", Capacity = 96 });
            await context.Aircrafts.AddAsync(new Aircraft { Model = "McDonnell Douglas MD-80", Capacity = 152 });
            await context.Aircrafts.AddAsync(new Aircraft { Model = "Boeing 737", Capacity = 162 });
            await context.Aircrafts.AddAsync(new Aircraft { Model = "Comac C919", Capacity = 168 });
            await context.Aircrafts.AddAsync(new Aircraft { Model = "Bomardier CRJ", Capacity = 75 });
            await context.Aircrafts.AddAsync(new Aircraft { Model = "ATR 72", Capacity = 75 });
            await context.Aircrafts.AddAsync(new Aircraft { Model = "Boeing 757", Capacity = 200 });
            await context.Aircrafts.AddAsync(new Aircraft { Model = "Airbus A330", Capacity = 370 });
            await context.Aircrafts.AddAsync(new Aircraft { Model = "Boeing 767", Capacity = 287 });
            await context.Aircrafts.AddAsync(new Aircraft { Model = "Embraer 190", Capacity = 111 });
            await context.Aircrafts.AddAsync(new Aircraft { Model = "Bombardier Q400", Capacity = 84 });
            await context.Aircrafts.AddAsync(new Aircraft { Model = "Tupolev Tu-154", Capacity = 200 });
            await context.Aircrafts.AddAsync(new Aircraft { Model = "McDonnell Douglas DC-9", Capacity = 110 });
            await context.Aircrafts.AddAsync(new Aircraft { Model = "Fokker 70/100", Capacity = 85 });
            await context.Aircrafts.AddAsync(new Aircraft { Model = "Boeing 787", Capacity = 341 });
            await context.Aircrafts.AddAsync(new Aircraft { Model = "Sukhoi Superjet 100", Capacity = 103 });
            await context.Aircrafts.AddAsync(new Aircraft { Model = "Irkut MC-21", Capacity = 187 });
            await context.Aircrafts.AddAsync(new Aircraft { Model = "Comac C919", Capacity = 168 });
            // Aeroporturi
            await context.Airports.AddAsync(new Airport { Name = "Henri Coandă International Airport", Location = "București" });
            await context.Airports.AddAsync(new Airport { Name = "Avram Iancu Cluj International Airport", Location = "Cluj-Napoca" });
            await context.Airports.AddAsync(new Airport { Name = "Traian Vuia International Airport", Location = "Timișoara" });
            await context.Airports.AddAsync(new Airport { Name = "Aurel Vlaicu International Airport", Location = "București" });
            await context.Airports.AddAsync(new Airport { Name = "Iași International Airport", Location = "Iași" });
            await context.Airports.AddAsync(new Airport { Name = "Sibiu International Airport", Location = "Sibiu" });
            await context.Airports.AddAsync(new Airport { Name = "Mihail Kogălniceanu International Airport", Location = "Constanta" });
            await context.Airports.AddAsync(new Airport { Name = "Târgu Mureș International Airport", Location = "Târgu Mureș" });
            await context.Airports.AddAsync(new Airport { Name = "Bacău International Airport", Location = "Bacău" });
            await context.Airports.AddAsync(new Airport { Name = "Oradea International Airport", Location = "Oradea" });
            await context.Airports.AddAsync(new Airport { Name = "Satu Mare International Airport", Location = "Satu Mare" });
            await context.Airports.AddAsync(new Airport { Name = "Suceava International Airport", Location = "Suceava" });
            await context.Airports.AddAsync(new Airport { Name = "Arad International Airport", Location = "Arad" });
            await context.Airports.AddAsync(new Airport { Name = "Craiova International Airport", Location = "Craiova" });
            await context.Airports.AddAsync(new Airport { Name = "Targu Jiu International Airport", Location = "Targu Jiu" });
            await context.Airports.AddAsync(new Airport { Name = "Alba Iulia International Airport", Location = "Alba Iulia" });
            await context.Airports.AddAsync(new Airport { Name = "Baia Mare International Airport", Location = "Baia Mare" });
            // Companii Aeriene
            await context.Airlines.AddAsync(new Airline { Name = "TAROM" });
            await context.Airlines.AddAsync(new Airline { Name = "Blue Air" });
            await context.Airlines.AddAsync(new Airline { Name = "Wizz Air" });
            await context.Airlines.AddAsync(new Airline { Name = "Ryanair" });
            await context.Airlines.AddAsync(new Airline { Name = "Carpatair" });
            await context.Airlines.AddAsync(new Airline { Name = "Pegasus Airlines" });
            await context.Airlines.AddAsync(new Airline { Name = "Air Bucharest" });
            await context.Airlines.AddAsync(new Airline { Name = "Azur Air" });
            await context.Airlines.AddAsync(new Airline { Name = "AtlasGlobal" });
            await context.Airlines.AddAsync(new Airline { Name = "MyAir" });
            await context.SaveChangesAsync();
            // Classes
            Airport[] airports = context.Airports.ToArray();
            Airline[] airlines = context.Airlines.ToArray();
            Aircraft[] aircrafts = context.Aircrafts.ToArray();
            int[] classes = { 1, 2 };
            foreach (Airport airport in airports)
            {
                foreach (Airline airline in airlines)
                {
                    foreach (int _class in classes)
                    {
                        context.Classes.Add(new Class { Number = _class, AirlineID=airline.AirlineID, AirportID=airport.AirportID, Price= ModelBuilderExtensions.CalculateTicketPrice(_class) });
                    }
                }
            }
            context.SaveChanges();
            CreditCard creditCard = new CreditCard { CreditCardID=1,CustomerID = customer.CustomerID, CardHolderName = "Serban Eusebiu", CardNumber = "123456789123456", SecurityCode="323", IsPrimary=true, ExpirationDate=new DateTime(2025, 1,1) };
            context.CreditCards.Add(creditCard);
            context.SaveChanges();
            customer.CreditCardID = creditCard.CreditCardID;
            context.Customers.Update(customer);
            context.SaveChanges();
            // Zboruri
            foreach (Airport departureAirport in airports)
            {
                foreach(Airport arrivalAirport in airports)
                {
                    if (departureAirport.AirportID == arrivalAirport.AirportID) continue; 
                    foreach (Airline airline in airlines)
                    {
                        foreach(Aircraft aircraft in aircrafts)
                        {
                            DateTime[] arrivalAndDeparture = ModelBuilderExtensions.GenerateDepartureAndArrivals();
                            Random random = new Random();
                            int rand = random.Next(20, 40);
                            int[] capacity = ModelBuilderExtensions.GetSeatsByClass(aircraft.Capacity, rand);
                            context.Flights.Add(new Flight { AircraftID = aircraft.AircraftID, AirlineID = airline.AirlineID, DepartureAirportID = departureAirport.AirportID, ArrivalAirportID = arrivalAirport.AirportID, DepartureTime = arrivalAndDeparture[0], ArrivalTime = arrivalAndDeparture[1], FirstClassCapacity = capacity[0], EconomyClassCapacity = capacity[1] });
                        }
                    }
                }
            }
            context.SaveChanges();
            // Locuri
           
            int pageSize = 1000;
            Class[] _classes = context.Classes.ToArray();
            for (int pageNumber = 1; pageNumber <= 68; pageNumber++)
            {
                Flight[] flights = context.Flights.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToArray();
                foreach (Flight flight in flights)
                {
                    for (int i = 1; i <= flight.Aircraft.Capacity; i++)
                    {
                        int classID;
                        if (i <= flight.FirstClassCapacity)
                        {
                            classID = _classes.Where(e => e.Number == 1 && e.AirportID == flight.DepartureAirportID && e.AirlineID == flight.AirlineID).First().ClassID;
                        }
                        else
                        {
                            classID = _classes.Where(e => e.Number == 2 && e.AirportID == flight.DepartureAirportID && e.AirlineID == flight.AirlineID).First().ClassID;
                        }
                        context.Seats.Add(new Seat { ClassID = classID, SeatNumber = i, FlightID = flight.FlightID, IsAvailable = true });
                    }
                    context.SaveChanges();
                }

            }


            // Istoric Salariu
            SalaryHistory istoric1 = new SalaryHistory { EmployeeID = employee.EmployeeID, EffectiveDate = new DateTime(DateTime.Now.Year, 1, 1), Amount = 2850};
            context.SalaryHistories.Add(istoric1);
            context.SaveChanges();
            SalaryHistory istoric2 = new SalaryHistory { EmployeeID = employee.EmployeeID, EffectiveDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), Amount = 2950, PreviousSalaryHistoryID = istoric1.SalaryHistoryID };
            context.SalaryHistories.Add(istoric2);
            context.SaveChanges();
            istoric1.NextSalaryHistoryID = istoric2.SalaryHistoryID;
            context.SalaryHistories.Update(istoric1);
            context.SaveChanges();

        }

        public static double CalculateTicketPrice(int classType)
        {
            Random rand = new Random();
            double basePrice = 200;

            if (classType == 1)
            {
                return basePrice + (basePrice * 0.5) + rand.Next(50, 200);
            }
            return basePrice + rand.Next(50, 150);
        }

        public static DateTime[] GenerateDepartureAndArrivals()
        {
            Random random = new Random();
            DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            int range = (DateTime.Now.AddYears(1) - start).Days;
            DateTime departureDate = start.AddDays(random.Next(range));
            DateTime arrivalDate = departureDate.AddHours(random.Next(24));
            DateTime[] dates = { departureDate, arrivalDate };
            return dates;
        }
        public static int[] GetSeatsByClass(int totalSeats, int class1Percent)
        {
            int class1Seats = (class1Percent * totalSeats) / 100;
            int class2Seats = totalSeats - class1Seats;
            int[] seats = { class1Seats, class2Seats };
            return seats;
        }
    }
}
