using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlightReservationsApplication.Models
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerID { get; set; }
        [Display(Name = "Cont")]
        [Required(ErrorMessage = "Contul este necesar.")]
        public Guid AccountID { get; set; }
        [Display(Name = "Card")]
        public int? CreditCardID { get; set; }
        [ForeignKey("AccountID")]
        public virtual Account? Account { get; set; }
        [ForeignKey("CreditCardID")]
        public virtual ICollection<CreditCard>? CreditCards { get; set; }
        public virtual ICollection<Reservation>? Reservations { get; set; }
    }
}
