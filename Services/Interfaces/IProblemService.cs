using CodeHex.Model.Domains;

namespace CodeHex.Services.Interfaces
{
    public interface IProblemService
    {
        Task<IEnumerable<Problem>> GetAllProblems();
        Task<IEnumerable<Problem>> CreateProblems(IEnumerable<Problem> problem);
        Task<IEnumerable<Problem>> GetProblemsByContestId(int contestId);
        Task<Problem> AddProblem(Problem problem);
        Task<bool> DeleteProblem(int id);
        Task<bool> EditProblem(Problem problem);
        Task<Problem> GetProblemById(int id);
    }
}
