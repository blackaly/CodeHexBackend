using CodeHex.Model.Domains;
using CodeHex.Model.DTOs;
using CodeHex.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeHex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProblemController : ControllerBase
    {

        private readonly IProblemService _problemService;
        private IWebHostEnvironment _webHostEnvironment;

        public ProblemController(IProblemService problemService, IWebHostEnvironment webHostEnvironment)
        {
            _problemService = problemService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("ByContestId")]
        public async Task<IActionResult> GetProblemsByContestId(int contestId)
        {
            var obj = await _problemService.GetProblemsByContestId(contestId);
            if (obj != null) return Ok(obj);

            return NoContent();
        }

        [HttpPost("Problems")]
        public async Task<IActionResult> CreateProblems(int contestId, [FromForm]List<ProblemDTO> model)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);


            var problems = new List<Problem>(); 

            foreach(var a in model)
            {
                var fakeProblem = Path.GetRandomFileName();
                problems.Add(new Problem()
                {
                    ContestId = contestId,
                    ProblemDetails = new ProblemDetail()
                    {
                        ExecutionTime = a.ExecutionTime,
                        MemoryLimit = a.MemoryLimit,
                        ProblemDescription = fakeProblem
                    },
                    ProblemName = a.ProblemName
                });

                if (!string.IsNullOrEmpty(a.ProblemDescription?.FileName))
                {
                    string dir = Path.Combine(_webHostEnvironment.WebRootPath, $"Uploads/{contestId}");
                    Directory.CreateDirectory(dir);
                    var path = Path.Combine(_webHostEnvironment.WebRootPath, $"Uploads/{contestId}", fakeProblem);
                    using FileStream f = new FileStream(path, FileMode.Create);
                    a.ProblemDescription.CopyTo(f);
                }
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

            var fakeProblem = Path.GetRandomFileName();

            var problem = new Problem()
            {
                ContestId = contestId,
                ProblemDetails = new ProblemDetail()
                {
                    ExecutionTime = model.ExecutionTime,
                    MemoryLimit = model.MemoryLimit,
                    ProblemDescription = fakeProblem
                },
                ProblemName = model.ProblemName
            };

            if (!string.IsNullOrEmpty(model.ProblemDescription?.FileName))
            {
                var path = Path.Combine(_webHostEnvironment.WebRootPath, $"Uploads/{contestId}", fakeProblem);
                using FileStream f = new FileStream(path, FileMode.Create);
                model.ProblemDescription.CopyTo(f);
            }


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
