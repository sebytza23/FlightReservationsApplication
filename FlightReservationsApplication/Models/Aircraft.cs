using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlightReservationsApplication.Models
{
    public class Aircraft
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AircraftID { get; set; }
        [Required(ErrorMessage = "Numele modelului este necesar.")]
        [Display(Name = "Model Avion")]
        public string Model { get; set; }
        [Required(ErrorMessage = "Capacitatea este necesara.")]
        [Display(Name ="Capacitate Avion")]
        public int Capacity { get; set; }
        public virtual ICollection<Flight>? Flights { get; set; }
    }
}
