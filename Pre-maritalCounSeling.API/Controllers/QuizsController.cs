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


        /*
                // GET: api/Quizs
                [HttpGet]
                public async Task<ActionResult<IEnumerable<Quiz>>> GetQuizzes()
                {
                    return await _context.Quizzes.ToListAsync();
                }

                // GET: api/Quizs/5
                [HttpGet("{id}")]
                public async Task<ActionResult<Quiz>> GetQuiz(long id)
                {
                    var quiz = await _context.Quizzes.FindAsync(id);

                    if (quiz == null)
                    {
                        return NotFound();
                    }

                    return quiz;
                }

                // PUT: api/Quizs/5
                // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
                [HttpPut("{id}")]
                public async Task<IActionResult> PutQuiz(long id, Quiz quiz)
                {
                    if (id != quiz.Id)
                    {
                        return BadRequest();
                    }

                    _context.Entry(quiz).State = EntityState.Modified;

                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!QuizExists(id))
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

                // POST: api/Quizs
                // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
                [HttpPost]
                public async Task<ActionResult<Quiz>> PostQuiz(Quiz quiz)
                {
                    _context.Quizzes.Add(quiz);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction("GetQuiz", new { id = quiz.Id }, quiz);
                }

                // DELETE: api/Quizs/5
                [HttpDelete("{id}")]
                public async Task<IActionResult> DeleteQuiz(long id)
                {
                    var quiz = await _context.Quizzes.FindAsync(id);
                    if (quiz == null)
                    {
                        return NotFound();
                    }

                    _context.Quizzes.Remove(quiz);
                    await _context.SaveChangesAsync();

                    return NoContent();
                }

                private bool QuizExists(long id)
                {
                    return _context.Quizzes.Any(e => e.Id == id);
                }
                */
    }
}
