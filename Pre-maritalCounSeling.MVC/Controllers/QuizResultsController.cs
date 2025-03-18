using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pre_maritalCounSeling.Common.Util;
using Pre_maritalCounSeling.DAL.Entities;
using System.Text;

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
                        if (response.IsSuccessStatusCode)
                        {
                            return View(quizResults);
                        }
                        return View("~/Views/ERR_PAGE/404page.cshtml");


                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View(new List<QuizResult>());
            }
        }
        //GET: QuizResults using AJAX JQUERY
        public async Task<IActionResult> Index02()
        {
            return View();
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
                        if (response.IsSuccessStatusCode)
                        {
                            return View(quizResult);
                        }
                        return View("~/Views/ERR_PAGE/404page.cshtml");
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
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction(nameof(Index));

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                TempData["ErrorMessage"] = "Failed to delete the quiz result. Please try again.";
            }
            return View("~/Views/ERR_PAGE/403page.cshtml");
        }




        // GET: QuizResults/Edit/5
        public async Task<IActionResult> Edit(long? id)
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
                        if (response.IsSuccessStatusCode)
                        {
                            return View(quizResult);
                        }
                        return View("~/Views/ERR_PAGE/404page.cshtml");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }
        }

        // POST: QuizResults/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, QuizResult quizResult)
        {
            if (id != quizResult.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(quizResult);
            }

            try
            {
                using (var httpClient = new HttpClient())
                {
                    await AppUtil.AddJwtTokenToRequestHeader(httpClient, HttpContext);
                    var content = new StringContent(
                        JsonConvert.SerializeObject(quizResult),
                        Encoding.UTF8, "application/json");

                    using (var response = await httpClient
                        .PutAsync(_configuration["Pre-maritalCounSelingAPIEndpoint:Base"] + "QuizResults/" + id, content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction(nameof(Index));
                        }
                        TempData["ErrorMessage"] = "Failed to update the quiz result. Please try again.";
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                TempData["ErrorMessage"] = "An error occurred while updating the quiz result.";
            }
            return View(quizResult);
        }

        #region TEMP: code following up
        // GET: QuizResults/Create
        //public async Task<IActionResult> Create()
        //{
        //    List<Quiz> result = new List<Quiz>();
        //    try
        //    {
        //        using (var httpClient = new HttpClient())
        //        {
        //            await AppUtil.AddJwtTokenToRequestHeader(httpClient, HttpContext);

        //            using (var response = await httpClient
        //                .GetAsync(_configuration["Pre-maritalCounSelingAPIEndpoint:Base"] + "QuizResults"))
        //            {
        //                //result = await AppUtil.GetDeserializedResponseFromApi<List<QuizResult>>(response);
        //                return null;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, ex.Message);
        //    }
        //    ViewData["QuizId"] = new SelectList(result, "Id", "Title");
        //    return View();
        //}

        //// POST: QuizResults/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Title,Description,Image,Duration,DurationUnit,AvgTimeCompleted,Tags,Difficulty,PassScore,CreatedAt,ModifiedAt,CreatedBy,ModifiedBy,IsActive,IsDeleted")] Quiz quiz)
        //{
        //    //if (ModelState.IsValid)
        //    //{
        //    //    _context.Add(quiz);
        //    //    await _context.SaveChangesAsync();
        //    //    return RedirectToAction(nameof(Index));
        //    //}
        //    return View(quiz);
        //}
        #endregion
    }
}

