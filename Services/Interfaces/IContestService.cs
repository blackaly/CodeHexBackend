using CodeHex.Model.Domains;

namespace CodeHex.Services.Interfaces
{
    public interface IContestService
    {
        Task<IEnumerable<Contest>> GetAllContests();
        Task<Contest> CreateContest(Contest contest);
        Task DeleteContest(Contest contest);
        Task<bool> EditContest(Contest contest);
        Task<Contest> GetContestById(int id);
    }
}
