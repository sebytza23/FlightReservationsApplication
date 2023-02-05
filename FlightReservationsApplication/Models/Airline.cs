using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlightReservationsApplication.Models
{
    public class Airline
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AirlineID { get; set; }
        [Required(ErrorMessage = "Numele companiei aeriene este necesar.")]
        [Display(Name = "Companie Aeriana")]
        public string Name { get; set; }
        public virtual ICollection<Flight>? Flights { get; set; }
        public virtual ICollection<Class>? Classes { get; set; }

    }
}
