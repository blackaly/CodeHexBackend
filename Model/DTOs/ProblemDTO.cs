using CodeHex.Model.Domains;
using System.ComponentModel.DataAnnotations;

namespace CodeHex.Model.DTOs
{
    public class ProblemDTO
    {
        [MaxLength(100, ErrorMessage = "The max length of the problem is 100 character")]
        public string ProblemName { get; set; }
        public IFormFile? ProblemDescription { get; set; }
        [Required(ErrorMessage = "Memory Limit is required")]
        public decimal MemoryLimit { get; set; }
        [Required(ErrorMessage = "Execution Time is required")]
        public decimal ExecutionTime { get; set; }

    }
}
