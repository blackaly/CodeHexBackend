namespace CodeHex.Model.Domains
{
    public class ProblemTestCase
    {
        public int Id { get; set; }
        public int ProblemId { get; set; }
        public Problem Problem { get; set; }
        public int TestCaseId { get; set; }
        public TestCase TestCase { get; set; }
    }
}
