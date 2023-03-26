using System.ComponentModel.DataAnnotations;

namespace CodeHex.Model.DTOs
{
    public class ContestDTO
    {
        [Required(ErrorMessage = "The contest name is required")]
        [MaxLength(100, ErrorMessage = "Max length of contest name is 100 character")]
        public string ContestName { get; set; }
        [Required(ErrorMessage = "Start Date is required")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "End Date is required")]
        public DateTime EndDate { get; set; }
    }
}
