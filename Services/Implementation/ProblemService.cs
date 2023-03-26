using CodeHex.Model;
using CodeHex.Model.Domains;
using CodeHex.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodeHex.Services.Implementation
{
    public class ProblemService : IProblemService
    {

        private readonly ApplicationDbContext _context;

        public ProblemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Problem> AddProblem(Problem problem)
        {
            await _context.Problems.AddAsync(problem);
            await _context.SaveChangesAsync(); 
            return problem;
        }

        public async Task<IEnumerable<Problem>> CreateProblems(IEnumerable<Problem> problem)
        {
            await _context.Problems.AddRangeAsync(problem);
            await _context.SaveChangesAsync();
            return problem;
        }

        public async Task<IEnumerable<Problem>> GetProblemsByContestId(int contestId)
        {
            var obj = await _context.Problems.Where(x => x.ContestId == contestId).Include(y => y.ProblemDetails).ToListAsync();
            return obj;

        }

        public async Task<bool> DeleteProblem(int id)
        {
            bool ok = true; 
            var obj = await _context.Problems.FindAsync(id);
            if(obj != null)
            {
                _context.Problems.Remove(obj);
                await _context.SaveChangesAsync();
                return ok;
            }
            return !ok;
        }

        public async Task<bool> EditProblem(Problem problem)
        {
            bool ok = true;
            try
            {
                _context.Entry(problem).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return ok;
            }
            catch
            {
                return !ok;
            }
        }

        public async Task<IEnumerable<Problem>> GetAllProblems()
        {
            return await _context.Problems.ToListAsync();
        }

        public async Task<Problem> GetProblemById(int id)
        {
            return await _context.Problems.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
