using CodeHex.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeHex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProblemController : ControllerBase
    {

        private readonly IProblemService _problemService;

        public ProblemController(IProblemService problemService)
        {
            _problemService = problemService;
        }

        [HttpGet("ByContestId")]
        public async Task<IActionResult> GetProblemsByContestId(int contestId)
        {
            var obj = await _problemService.GetProblemsByContestId(contestId);
            if (obj != null) return Ok(obj);

            return NoContent();
        }
    }
}
