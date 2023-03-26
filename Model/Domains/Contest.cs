using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CodeHex.Model.Domains
{
    public class Contest
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string ContestName { get; set; }
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }
        [JsonIgnore]
        public ICollection<Problem> Problems { get; set; }
    }
}
