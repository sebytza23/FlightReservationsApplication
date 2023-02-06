using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlightReservationsApplication.Models
{
    public class ReservationConfirmation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReservationConfirmationID { get; set; }
        [Display(Name = "Rezervare")]
        [Required(ErrorMessage = "Rezervarea este necesara.")]
        public int ReservationID { get; set; }
        [Display(Name = "Angajat")]
        [Required(ErrorMessage = "Angajatul este necesar.")]
        public int EmployeeID { get; set; }
        [Display(Name = "Data Confirmare")]
        public DateTime? ConfirmationDate { get; set; }
        [Display(Name = "Data Anulare")]
        public DateTime? DeclinedDate { get; set; }
        [Display(Name = "Detalii")]
        public string Notes { get; set; }
        [ForeignKey("ReservationID")]
        public virtual Reservation? Reservation { get; set; }
        [ForeignKey("EmployeeID")]
        public virtual Employee? Employee { get; set; }
    }
}

