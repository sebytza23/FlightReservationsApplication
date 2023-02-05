using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlightReservationsApplication.Models
{
    public class ContactInformation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactInformationID { get; set; }
        [Display(Name = "Cont")]
        public Guid AccountID { get; set; }
        [Required(ErrorMessage = "Adresa este necesara.")]
        [Display(Name = "Adresa 1")]
        public string AddressLine1 { get; set; }
        [Display(Name = "Adresa 2")]
        public string? AddressLine2 { get; set; }
        [Required(ErrorMessage = "Orasul este necesar.")]
        [Display(Name = "Oras")]
        public string City { get; set; }
        [Required(ErrorMessage = "Judetul este necesar.")]
        [Display(Name = "Judet")]
        public string State { get; set; }
        [Required(ErrorMessage = "Codul postal este necesar.")]
        [Display(Name = "Cod Postal")]
        public string ZipCode { get; set; }
        [Required(ErrorMessage = "Numarul de telefon este necesar.")]
        [Display(Name = "Numar Telefon")]
        [Phone(ErrorMessage = "Numarul de telefon este invalid.")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Adresa Principala")]
        public bool IsPrimary { get; set; }
        [ForeignKey("AccountID")]
        public virtual Account? Account { get; set; }
    }
}

