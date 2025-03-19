using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Pre_maritalCounSeling.BAL.Auth;
using Pre_maritalCounSeling.BAL.ServiceQuiz;
using Pre_maritalCounSeling.BAL.ServiceUser;
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
        private readonly IUserService _userService;
        public QuizResultsController(ILogger<QuizResultsController> logger, IQuizResultService quizResultService, IUserService userService)
        {
            _logger = logger;
            _quizResultService = quizResultService;
            _userService = userService;
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
        //GET: get the data user list for selectable box
        [HttpGet("userlistselectbox")]
        public async Task<IActionResult> GetUserListSimplyAsync()
        {
            try
            {
                var results = await _userService.GetAllaAsync();
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving user list." });
            }
        }

        //TODO: create act as quiz taking (if have time)
        /// <summary>
        /// Create new quiz result - fake creating a quiz result with role Admin
        /// </summary>
        [HttpPost]
        //[PermissionAuthorize("Admin", "Customer")]
        [PermissionAuthorize("Admin")]

        public async Task<IActionResult> CreateQuizResultAsync([FromBody] QuizResult request)
        {
            try
            {
                await _quizResultService.CreateQuizResultSimplyAsync(request);
                return Ok(new { message = "Quiz result created successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating quiz result." });
            }
        }

        [HttpPost("UserDetails")]
        [PermissionAuthorize("Admin")]

        public async Task<IActionResult> CreateUserDetailAsync([FromBody] UserDetail request)
        {
            try
            {
                await _quizResultService.CreateUserDetailAsync(request);
                return Ok(new { message = "created successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating " });
            }
        }


        [HttpGet("UserDetails")]
        [PermissionAuthorize("Admin")]
        [EnableQuery]
        public async Task<IActionResult> GetUserDetailsAsync()
        {
            try
            {
                return Ok(await _quizResultService.GetUserDetailsAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }
        }
        [HttpGet("UserDetails/{id}")]
        [PermissionAuthorize("Admin")]
        [EnableQuery]
        public async Task<IActionResult> GetUserDetailAsync(long id)
        {
            try
            {
                return Ok(await _quizResultService.GetUserDetailAsync(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }
        }

        //GET: get the data for user detail category select box
        [HttpGet("userDetailCateSelectBox")]
        public async Task<IActionResult> GetUserDetailCateSimplyAsync()
        {
            try
            {
                var results = await _quizResultService.GetUserDetailCateSimplyAsync();
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving user detail cate list." });
            }
        }


    }
}
