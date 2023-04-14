using System.ComponentModel.DataAnnotations;

namespace CodeHex.Model.Domains
{
    public class ProblemDetail
    {
        public int Id { get; set; }
        public string? ProblemDescription { get; set; }
        public decimal MemoryLimit { get; set; }
        public decimal ExecutionTime { get ; set; }  
    }
}
