using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlightReservationsApplication.Models
{
    public enum Status
    {
        InProgress,
        Declined,
        Accepted
    }
    public class Reservation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReservationID { get; set; }
        [Required(ErrorMessage = "Clientul este necesar.")]
        [Display(Name = "Client")]
        public int CustomerID { get; set; }
        [Required(ErrorMessage = "Locul este necesar.")]
        [Display(Name = "Loc")]
        public int SeatID { get; set; }
        [Display(Name = "Confirmare Rezervare")]
        public int? ReservationConfirmationID { get; set; }
        [Display(Name = "Status")]
        public Status Status { get; set; }
        [ForeignKey("CustomerID")]
        public virtual Customer? Customer { get; set; }
        [ForeignKey("SeatID")]
        public virtual Seat? Seat { get; set; }
        [ForeignKey("ReservationConfirmationID")]
        public virtual ReservationConfirmation? ReservationConfirmation { get; set; }

    }
}
