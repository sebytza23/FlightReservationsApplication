using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlightReservationsApplication.Models
{
    public class SalaryHistory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SalaryHistoryID { get; set; }
        [Display(Name = "Angajat")]
        [Required(ErrorMessage = "Angajatul este necesar.")]
        public int EmployeeID { get; set; }
        [Display(Name = "Salariu Anterior")]
        public int? PreviousSalaryHistoryID { get; set; }
        [Display(Name = "Salariu Urmator")]
        public int? NextSalaryHistoryID { get; set; }
        [Display(Name = "Data")]
        public DateTime EffectiveDate { get; set; }
        [Display(Name = "Salariu")]
        public decimal Amount { get; set; }
        [ForeignKey("EmployeeID")]
        public virtual Employee? Employee { get; set; }
        [ForeignKey("PreviousSalaryHistoryID")]
        public virtual SalaryHistory? PreviousSalaryHistory { get; set; }
        [ForeignKey("NextSalaryHistoryID")]
        public virtual SalaryHistory? NextSalaryHistory { get; set; }
    }
}
