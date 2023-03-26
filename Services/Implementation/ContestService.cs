using CodeHex.Model;
using CodeHex.Model.Domains;
using CodeHex.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodeHex.Services.Implementation
{
    public class ContestService : IContestService
    {
        private readonly ApplicationDbContext _context;

        public ContestService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Contest> CreateContest(Contest contest)
        {
            await _context.Contests.AddAsync(contest);
            await _context.SaveChangesAsync();
            return contest;
            
        }

        public async Task DeleteContest(Contest contest)
        {

            _context.Contests.Remove(contest);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EditContest(Contest contest)
        {
            bool ok = true;
            try
            {
                _context.Entry(contest).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return ok;
            }
            catch
            {
                return !ok;
            }
        }

        public async Task<IEnumerable<Contest>> GetAllContests()
        {
            return await _context.Contests.ToListAsync();
        }

        public async Task<Contest> GetContestById(int id)
        {
            return await _context.Contests.FirstOrDefaultAsync(x => x.Id == id);    
        }
    }
}
