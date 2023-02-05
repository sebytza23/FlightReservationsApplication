using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightReservationsApplication.Models
{
    public class CreditCard
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CreditCardID { get; set; }
        [Display(Name = "Client")]
        [Required(ErrorMessage = "Clientul este necesar.")]
        public int CustomerID { get; set; }
        [MinLength(16, ErrorMessage = "Va rugam sa introduceti un numar de card corect!")]
        [MaxLength(16, ErrorMessage = "Va rugam sa introduceti un numar de card corect!")]
        [Display(Name ="Numar Card")]
        public string CardNumber { get; set; }
        [Required(ErrorMessage = "Numele detinatorului este necesar.")]
        [Display(Name ="Nume detinator")]
        public string CardHolderName { get; set; }
        [Required(ErrorMessage = "Data de expirare este necesara.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data Expirare")]
        [Column(TypeName = "Date")]
        public DateTime ExpirationDate { get; set; }
        [Required(ErrorMessage = "Va rugam sa introduceti CVV-ul.")]
        [MaxLength(3, ErrorMessage = "Va rugam sa introduceti un CVV corect.")]
        [MinLength(3, ErrorMessage = "Va rugam sa introduceti un CVV corect.")]
        [Display(Name = "CVV")]
        public string SecurityCode { get; set; }
        [Display(Name = "Card Principal")]
        public bool IsPrimary { get; set; }
        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }
    }
}
