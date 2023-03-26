using System.ComponentModel.DataAnnotations;

namespace CodeHex.Model.Domains
{
    public class Problem
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string ProblemName { get; set; }
        public int ProblemDetailsId { get; set; }
        public ProblemDetail ProblemDetails { get; set; }
        public int ContestId { get; set; }
        public Contest Contest { get; set; }
    }
}
