using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlightReservationsApplication.Models
{
    public class Seat
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SeatID { get; set; }
        [Required(ErrorMessage = "Numarul locului este necesar.")]
        [Display(Name = "Numar Loc")]
        public int SeatNumber { get; set; }
        [Required(ErrorMessage = "Clasa este necesar.")]
        [Display(Name = "Clasa")]
        public int ClassID { get; set; }
        [Required(ErrorMessage = "Zborul este necesar.")]
        [Display(Name = "Zbor")]
        public int FlightID { get; set; }
        [Required(ErrorMessage = "Disponibilitatea este necesara.")]
        [Display(Name = "Disponibil")]
        public bool IsAvailable { get; set; }
        [ForeignKey("FlightID")]
        [Display(Name = "Zbor")]
        public virtual Flight? Flight { get; set; }
        [Display(Name = "Clasa")]
        public virtual Class? Class { get; set; }
        public virtual ICollection<Reservation>? Reservations { get; set; }
    }
}
