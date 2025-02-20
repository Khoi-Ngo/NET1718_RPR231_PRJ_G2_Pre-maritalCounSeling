using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Pre_maritalCounSeling.BAL.Auth;
using Pre_maritalCounSeling.BAL.ServiceQuiz;

namespace Pre_maritalCounSeling.API.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class QuizResultsController : ControllerBase
    {
        #region Init
        private readonly ILogger<QuizResultsController> _logger;
        private readonly IQuizResultService _quizResultService;
        public QuizResultsController(ILogger<QuizResultsController> logger, IQuizResultService quizResultService)
        {
            _logger = logger;
            _quizResultService = quizResultService;
        }
        #endregion

        // GET: api/QuizResults
        [HttpGet]
        [PermissionAuthorize("ADMIN", "CUSTOMER")]
        [EnableQuery]
        public async Task<IActionResult> GetQuizResultsAsync()
        {
            try
            {
                return Ok(await _quizResultService.GetQuizResultsAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }
        }
        // GET: api/QuizResults/id
        [HttpGet("{id}")]
        [PermissionAuthorize("ADMIN", "CUSTOMER")]
        [EnableQuery]
        public async Task<IActionResult> GetQuizResultAsync(long id)
        {
            try
            {
                return Ok(await _quizResultService.GetQuizResultAsync(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }
        }
        // DELETE: api/QuizResults/id
        [HttpDelete("{id}")]
        [PermissionAuthorize("ADMIN", "CUSTOMER")]
        public async Task<IActionResult> DeleteQuizResultAsync(long id)
        {
            try
            {
                await _quizResultService.DeleteQuizResultAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Error when deleting the quiz result.");
            }
        }
        /*
         
        // GET: api/QuizResults
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizResult>>> GetQuizResults()
        {
            return await _context.QuizResults.ToListAsync();
        }

        // GET: api/QuizResults/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuizResult>> GetQuizResult(long id)
        {
            var quizResult = await _context.QuizResults.FindAsync(id);

            if (quizResult == null)
            {
                return NotFound();
            }

            return quizResult;
        }

        // PUT: api/QuizResults/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuizResult(long id, QuizResult quizResult)
        {
            if (id != quizResult.Id)
            {
                return BadRequest();
            }

            _context.Entry(quizResult).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuizResultExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/QuizResults
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<QuizResult>> PostQuizResult(QuizResult quizResult)
        {
            _context.QuizResults.Add(quizResult);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuizResult", new { id = quizResult.Id }, quizResult);
        }



        private bool QuizResultExists(long id)
        {
            return _context.QuizResults.Any(e => e.Id == id);
        }
         */
    }
}
