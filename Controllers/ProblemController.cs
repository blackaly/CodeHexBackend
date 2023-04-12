using CodeHex.Model.Domains;
using CodeHex.Model.DTOs;
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

        [HttpPost("Problems")]
        public async Task<IActionResult> CreateProblems(int contestId, List<ProblemDTO> model)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);


            var problems = new List<Problem>(); 

            foreach(var a in model)
            {
                problems.Add(new Problem()
                {
                    ContestId = contestId,
                    ProblemDetails = new ProblemDetail()
                    {
                        ExecutionTime = a.ExecutionTime,
                        MemoryLimit = a.MemoryLimit,
                        ProblemDescription = a.ProblemDescription
                    },
                    ProblemName = a.ProblemName
                });
            }

            try
            {
                await _problemService.CreateProblems(problems);
                return Ok(problems);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateProblem(int contestId, ProblemDTO model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);


            var problem = new Problem()
            {
                ContestId = contestId,
                ProblemDetails = new ProblemDetail()
                {
                    ExecutionTime = model.ExecutionTime,
                    MemoryLimit = model.MemoryLimit,
                    ProblemDescription = model.ProblemDescription
                },
                ProblemName = model.ProblemName
            };

            

            try
            {
                await _problemService.AddProblem(problem);
                return Ok(problem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
