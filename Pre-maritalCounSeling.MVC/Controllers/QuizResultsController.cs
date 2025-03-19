using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pre_maritalCounSeling.Common.Util;
using Pre_maritalCounSeling.DAL.Entities;
using System.Net.Http;
using System.Text;

namespace Pre_maritalCounSeling.MVC.Controllers
{
    public class QuizResultsController : Controller
    {
        #region INIT
        private readonly ILogger<QuizResultsController> _logger;
        private readonly IConfiguration _configuration;
        public QuizResultsController(ILogger<QuizResultsController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        //Authentication in front end
        private bool IsValidRole(string role)
        {
            string roleName = Request.Cookies["Role"];
            return roleName == role;
        }

        #endregion


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
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return View("~/Views/ERR_PAGE/404page.cshtml");
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
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return View("~/Views/ERR_PAGE/404page.cshtml");
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
        [HttpGet]
        public async Task<IActionResult> Edit(long? id)
        {
            if (!IsValidRole("Admin")) return View("~/Views/ERR_PAGE/403page.cshtml");
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                using (var httpClient = new HttpClient())
                {
                    await AppUtil.AddJwtTokenToRequestHeader(httpClient, HttpContext);

                    // Fetch the quiz result
                    using (var response = await httpClient.GetAsync(_configuration["Pre-maritalCounSelingAPIEndpoint:Base"] + "QuizResults/" + id))
                    {
                        var quizResult = await AppUtil.GetDeserializedResponseFromApi<QuizResult>(response);
                        if (quizResult == null)
                        {
                            _logger.LogWarning("QuizResult not found");
                            return NotFound();
                        }

                        // Fetch quizzes
                        using (var quizResponse = await httpClient.GetAsync(_configuration["Pre-maritalCounSelingAPIEndpoint:Base"] + "Quizs"))
                        {
                            var quizzes = await AppUtil.GetDeserializedResponseFromApi<List<Quiz>>(quizResponse);
                            ViewBag.QuizOption = quizzes?.Select(q => new SelectListItem
                            {
                                Value = q.Id.ToString(),
                                Text = q.Title
                            }).ToList();
                        }


                        // Fetch users
                        using (var userResponse = await httpClient.GetAsync(_configuration["Pre-maritalCounSelingAPIEndpoint:Base"] + "QuizResults/userlistselectbox"))
                        {
                            var users = await AppUtil.GetDeserializedResponseFromApi<List<User>>(userResponse);
                            ViewBag.UserId = users?.Select(u => new SelectListItem
                            {
                                Value = u.Id.ToString(),
                                Text = u.Email
                            }).ToList();
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
                        .PutAsync(_configuration["Pre-maritalCounSelingAPIEndpoint:Base"] + "QuizResults/", content))
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

        // GET: Create page
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            if (!IsValidRole("Admin")) return View("~/Views/ERR_PAGE/403page.cshtml");

            try
            {
                using (var httpClient = new HttpClient())
                {
                    //load the view bag quiz
                    using (var quizResponse = await httpClient.GetAsync(_configuration["Pre-maritalCounSelingAPIEndpoint:Base"] + "Quizs"))
                    {
                        var quizzes = await AppUtil.GetDeserializedResponseFromApi<List<Quiz>>(quizResponse);
                        ViewBag.QuizOption = quizzes?.Select(q => new SelectListItem
                        {
                            Value = q.Id.ToString(),
                            Text = q.Title
                        }).ToList();
                    }
                    //load the view bag user
                    using (var userResponse = await httpClient.GetAsync(_configuration["Pre-maritalCounSelingAPIEndpoint:Base"] + "QuizResults/userlistselectbox"))
                    {
                        var users = await AppUtil.GetDeserializedResponseFromApi<List<User>>(userResponse);
                        ViewBag.UserId = users?.Select(u => new SelectListItem
                        {
                            Value = u.Id.ToString(),
                            Text = u.Email
                        }).ToList();
                    }
                }

            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, ex.Message);
            }
            return View();// auto mapping with the UI - controller mapping folder UI || mapping action ...
        }

        //POST: Create the quiz result saving into database
        [HttpPost]
        public async Task<IActionResult> Create(QuizResult request)
        {
            try
            {
                string userName = Request.Cookies["UserName"];
                request.CreatedBy = userName;

                //calling API to save
                using (var httpClient = new HttpClient())
                {
                    await AppUtil.AddJwtTokenToRequestHeader(httpClient, HttpContext);
                    var content = new StringContent(
                     JsonConvert.SerializeObject(request),
                     Encoding.UTF8, "application/json");
                    using (var response = await httpClient
                       .PostAsync(_configuration["Pre-maritalCounSelingAPIEndpoint:Base"] + "QuizResults/", content))
                    {
                        if (!response.IsSuccessStatusCode)
                        {
                            TempData["ErrorMessage"] = "Failed to create the quiz result. Please try again.";
                            return View();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        #region USING AJAX FOR THE PROJECT

        //GET: QuizResults using AJAX JQUERY
        public async Task<IActionResult> Index02()
        {
            return View();
        }

        public async Task<IActionResult> Details02()
        {
            return View();
        }
        public async Task<IActionResult> Create02()
        {
            return View();
        }
        public async Task<IActionResult> Edit02()
        {
            return View();
        }

        #endregion

    }
}

/*

 */

