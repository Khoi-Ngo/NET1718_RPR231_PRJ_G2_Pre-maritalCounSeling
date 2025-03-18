using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Pre_maritalCounSeling.BAL.Auth;
using Pre_maritalCounSeling.BAL.ServiceQuiz;
using Pre_maritalCounSeling.DAL.Entities;

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
        [PermissionAuthorize("Admin", "Customer")]
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
        [PermissionAuthorize("Admin", "Customer")]
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
        [PermissionAuthorize("Admin", "Customer")]
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

        // UPDATE: api/QuizResults
        [HttpPut]
        [PermissionAuthorize("Admin")]
        public async Task<IActionResult> UpdateQuizResultAsync([FromBody] QuizResult request)
        {
            try
            {
                await _quizResultService.UpdateQuizResultAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Error when updating the quiz result.");
            }
        }

        //TODO: create act as quiz taking


    }
}
