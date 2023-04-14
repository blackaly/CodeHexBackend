using CodeHex.Model.Domains;
using CodeHex.Model.DTOs;
using CodeHex.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Runtime.Remoting;

namespace CodeHex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContestController : ControllerBase
    {
        private readonly IContestService _contestService;
        private IWebHostEnvironment _webHostEnvironment;

        public ContestController(IContestService contestService, IWebHostEnvironment webHostEnvironment)
        {
            _contestService = contestService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContests()
        {
            var obj = await _contestService.GetAllContests();

            if(obj != null) return Ok(obj);

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateContestWithoutProblms(ContestDTO model)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var contest = new Contest()
            {
                ContestName = model.ContestName,
                EndDate= model.EndDate,
                StartDate= model.StartDate,
            };

            try
            {
                await _contestService.CreateContest(contest);
                return Ok(contest);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("WithProblems")]
        public async Task<IActionResult> CreateContest(ContestProblemsDTO dto)
        {
            if (ModelState.IsValid)
            {


                var contest = new Contest()
                {
                    ContestName = dto.ContestName,
                    StartDate = dto.StartDate,
                    EndDate = dto.EndDate
                };

                var problems = new List<Problem>();

                foreach(var p in dto.Problems) {
                    var fakeProblem = Path.GetRandomFileName();
                    problems.Add(new Problem()
                    {
                        ProblemName = p.ProblemName,
                        ProblemDetails = new Model.Domains.ProblemDetail()
                        {
                            ExecutionTime= p.ExecutionTime,
                            MemoryLimit= p.MemoryLimit,
                            ProblemDescription= fakeProblem,
                        }
                        
                    });

                    if (!string.IsNullOrEmpty(p.ProblemDescription?.FileName))
                    {
                        var path = Path.Combine(_webHostEnvironment.WebRootPath, $"Uploads/{contest.Id}", fakeProblem);
                        using FileStream f = new FileStream(path, FileMode.Create);
                        p.ProblemDescription.CopyTo(f);
                    }
                }

                contest.Problems = problems;
                return Ok(await _contestService.CreateContest(contest));
            }

            return BadRequest(ModelState);
        }
        [HttpPut]
        public async Task<IActionResult> EditContest(int id, ContestDTO dto)
        {
            if (ModelState.IsValid)
            {
                var obj = await _contestService.GetContestById(id);

                if (obj == null) return BadRequest();

                obj.StartDate = dto.StartDate;
                obj.EndDate = dto.EndDate;
                obj.ContestName = dto.ContestName;

                try
                {
                    bool done = await _contestService.EditContest(obj);
                    if (done) return Ok();
                }
                catch
                {
                    return NoContent();
                }
            }

            return BadRequest(ModelState);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteContest(int id)
        {
            var obj = await _contestService.GetContestById(id);
          
            if(obj != null)
            {
                try
                {
                    await _contestService.DeleteContest(obj);
                    return Ok();
                }
                catch
                {
                    return BadRequest();
                }
            }

            return NoContent();
        }

    }
}
