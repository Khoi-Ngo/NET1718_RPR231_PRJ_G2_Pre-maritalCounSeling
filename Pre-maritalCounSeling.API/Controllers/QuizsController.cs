using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pre_maritalCounSeling.BAL.ServiceQuiz;
using Pre_maritalCounSeling.DAL.Entities;

namespace Pre_maritalCounSeling.API.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class QuizsController : ControllerBase
    {
        #region Init
        private readonly ILogger<QuizsController> _logger;
        private readonly IQuizService _quizService;
        public QuizsController(ILogger<QuizsController> logger, IQuizService quizService)
        {
            _logger = logger;
            _quizService = quizService;
        }
        #endregion
        // GET: api/Quizs
        [HttpGet]
        public async Task<IActionResult> GetQuizzesAsync()
        {
            try
            {
                return Ok(await _quizService.GetQuizzesAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }
        }

    }
}
