using CodeHex.Model.Domains;
using Microsoft.EntityFrameworkCore;

namespace CodeHex.Model
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Problem> Problems { get; set; }
        public DbSet<Contest> Contests{ get; set; }
        public DbSet<ProblemDetail> ProblemDetails{ get; set; }
        public DbSet<ProblemTestCase> ProblemTestCases{ get; set; }
        public DbSet<TestCase> TestCases{ get; set; }
    }
}
