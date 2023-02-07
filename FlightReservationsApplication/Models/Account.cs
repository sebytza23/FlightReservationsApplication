using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace FlightReservationsApplication.Models
{
    public class Account
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid AccountID { get; set; }
        [Display(Name="Adresa de contact")]
        public int? ContactInformationID { get; set; }
        [Required(ErrorMessage = "Email-ul este necesar.")]
        [EmailAddress(ErrorMessage = "Email-ul nu este valid.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Parola este necesara.")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Parola trebuie sa contina minim 8 caractere.")]
        [MaxLength(16, ErrorMessage = "Parola trebuie sa contina maxim 16 caractere.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,16}$", ErrorMessage = "Parola trebuie sa contina minim o litera mare, o litera mica, un numar si un caracter special.")]
        [Display(Name = "Parola")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Prenumele este necesar.")]
        [Display(Name = "Prenume")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Numele este necesar.")]
        [Display(Name = "Nume")]
        public string LastName { get; set; }
        [Display(Name = "Este Angajat?")]
        public bool IsEmployee { get; set; }
        [Display(Name = "ID Angajat")]
        public int? EmployeeID { get; set; }
        [Display(Name = "ID Client")]
        public int? CustomerID { get; set; }
        [ForeignKey("ContactInformationID")]
        public virtual ICollection<ContactInformation>? ContactInformations { get; set; }
        [ForeignKey("EmployeeID")]
        public virtual Employee? Employee { get; set; }
        [ForeignKey("CustomerID")]
        public virtual Customer? Customer { get; set; }
    }
}
