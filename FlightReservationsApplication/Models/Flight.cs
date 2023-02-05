using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlightReservationsApplication.Models
{
    public class Flight
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FlightID { get; set; }
        [Required(ErrorMessage = "Avionul este necesar.")]
        [Display(Name = "Avion")]
        public int AircraftID { get; set; }
        [Required(ErrorMessage = "Compania aeriana este necesara.")]
        [Display(Name = "Companie Aeriana")]
        public int AirlineID { get; set; }
        [Required(ErrorMessage = "Aeroportul de plecare este necesar.")]
        [Display(Name = "Aeroport Plecare")]
        public int DepartureAirportID { get; set; }
        [Required(ErrorMessage = "Aeroportul de sosire este necesar.")]
        [Display(Name = "Aeroport Sosire")]
        public int ArrivalAirportID { get; set; }
        [Required(ErrorMessage = "Data si ora de plecare sunt necesare.")]
        [Display(Name = "Data si ora de plecare")]
        public DateTime DepartureTime { get; set; }
        [Required(ErrorMessage = "Data si ora de sosire sunt necesare.")]
        [Display(Name = "Data si ora de sosire")]
        public DateTime ArrivalTime { get; set; }
        [Required(ErrorMessage = "Capacititate clasa I necesara.")]
        [Display(Name = "Capacitate Clasa I")]
        public int FirstClassCapacity { get; set; }
        [Required(ErrorMessage = "Capacitate clasa a II-a necesara.")]
        [Display(Name = "Capacitate Clasa II")]
        public int EconomyClassCapacity { get; set; }
        [ForeignKey("AircraftID")]
        public virtual Aircraft? Aircraft { get; set; }
        [ForeignKey("AirlineID")]
        public virtual Airline? Airline { get; set; }
        [ForeignKey("DepartureAirportID")]
        public virtual Airport? DepartureAirport { get; set; }
        [ForeignKey("ArrivalAirportID")]
        public virtual Airport? ArrivalAirport { get; set; }
        public virtual ICollection<Reservation>? Reservations { get; set; }
        public virtual ICollection<Seat>? Seats { get; set; }

    }
}
