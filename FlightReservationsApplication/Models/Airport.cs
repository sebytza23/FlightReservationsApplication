using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlightReservationsApplication.Models
{
    public class Airport
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AirportID { get; set; }
        [Required(ErrorMessage = "Numele aeroportului este necesar.")]
        [Display(Name = "Nume Aeroport")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Locatia este necesara.")]
        [Display(Name = "Locatie")]
        public string Location { get; set; }
        public virtual ICollection<Flight>? ArrivingFlights { get; set; }
        public virtual ICollection<Flight>? DepartingFlights { get; set; }
        public virtual ICollection<Class>? Classes { get; set; }
    }
}
