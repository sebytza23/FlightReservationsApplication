using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlightReservationsApplication.Models
{
    public class Class
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClassID { get; set; }
        [Required(ErrorMessage = "Numarul clasei este necesar.")]
        [Display(Name = "Clasa")]
        [MinLength(1, ErrorMessage = "Numarul clasei trebuie sa fie intre 1 si 2.")]
        [MaxLength(2, ErrorMessage = "Numarul clasei trebuie sa fie intre 1 si 2.")]
        public int Number { get; set; }
        [Required(ErrorMessage = "Aeroportul este necesar.")]
        [Display(Name = "Aeroport")]
        public int AirportID { get; set; }
        [Required(ErrorMessage = "Compania aeriana este necesara.")]
        [Display(Name = "Compania aeriana")]
        public int AirlineID { get; set; }
        [Required(ErrorMessage = "Pretul este necesar.")]
        [Display(Name = "Pret")]
        public double Price { get; set; }
        [ForeignKey("AirportID")]
        public virtual Airport? Airport { get; set; }
        [ForeignKey("AirlineID")]
        public virtual Airline? Airline { get; set; }
        public virtual ICollection<Seat>? Seats { get; set; }
    }
}
