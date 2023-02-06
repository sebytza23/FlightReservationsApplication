using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlightReservationsApplication.Models
{
    public class Employee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeID { get; set; }
        [Display(Name = "Cont")]
        [Required(ErrorMessage = "Contul este necesar.")]
        public Guid AccountID { get; set; }
        [Display(Name = "Administrator")]
        public bool IsAdmin { get; set; }
        [ForeignKey("AccountID")]
        public virtual Account? Account { get; set; }
        public virtual ICollection<ReservationConfirmation>? ReservationConfirmations { get; set; }
        public virtual ICollection<SalaryHistory>? SalaryHistories { get; set; }
    }
}
