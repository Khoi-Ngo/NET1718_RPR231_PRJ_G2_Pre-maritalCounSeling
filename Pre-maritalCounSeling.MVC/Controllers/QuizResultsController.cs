using Microsoft.AspNetCore.Mvc;
using Pre_maritalCounSeling.Common.Util;
using Pre_maritalCounSeling.DAL.Entities;

namespace Pre_maritalCounSeling.MVC.Controllers
{
    public class QuizResultsController : Controller
    {
        private readonly ILogger<QuizResultsController> _logger;
        private readonly IConfiguration _configuration;
        public QuizResultsController(ILogger<QuizResultsController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        // GET: QuizResults
        public async Task<IActionResult> Index()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    await AppUtil.AddJwtTokenToRequestHeader(httpClient, HttpContext);

                    using (var response = await httpClient
                        .GetAsync(_configuration["Pre-maritalCounSelingAPIEndpoint:Base"] + "QuizResults"))
                    {
                        var quizResults = await AppUtil.GetDeserializedResponseFromApi<List<QuizResult>>(response);
                        return View(quizResults);
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View(new List<QuizResult>());
            }
        }

        // GET: QuizResults/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                using (var httpClient = new HttpClient())
                {
                    await AppUtil.AddJwtTokenToRequestHeader(httpClient, HttpContext);

                    using (var response = await httpClient
                        .GetAsync(_configuration["Pre-maritalCounSelingAPIEndpoint:Base"] + "QuizResults/" + id))
                    {
                        var quizResult = await AppUtil.GetDeserializedResponseFromApi<QuizResult>(response);
                        if (quizResult == null)
                        {
                            _logger.LogWarning("QuizResult not found");
                            return NotFound();
                        }
                        return View(quizResult);
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }
        }

        // POST: QuizResults/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    await AppUtil.AddJwtTokenToRequestHeader(httpClient, HttpContext);

                    using (var response = await httpClient
                        .DeleteAsync(_configuration["Pre-maritalCounSelingAPIEndpoint:Base"] + "QuizResults/" + id))
                    {
                        if (!response.IsSuccessStatusCode)
                        {
                            TempData["ErrorMessage"] = "Failed to delete the quiz result. Please try again.";
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                TempData["ErrorMessage"] = "Failed to delete the quiz result. Please try again.";
            }
            return RedirectToAction(nameof(Index));
        }

    }
}

