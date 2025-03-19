using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Pre_maritalCounSeling.Common.Util;
using Pre_maritalCounSeling.DAL.Entities;
using Pre_maritalCounSeling.MVC.Models;
using System.Text;

namespace Pre_maritalCounSeling.MVC.Controllers
{
    public class UserDetailsController : Controller
    {
        #region INIT
        private readonly ILogger<UserDetailsController> _logger;
        private readonly IConfiguration _configuration;
        public UserDetailsController(ILogger<UserDetailsController> logger, IConfiguration configuration)
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
        private async Task<string> GetFileUploadURL(IFormFile inputFile)
        {
            try
            {
                string supabaseUrl = "https://ynukcgulpeejixpngtvv.supabase.co";
                string supabaseKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InludWtjZ3VscGVlaml4cG5ndHZ2Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDE3Mjk3NDgsImV4cCI6MjA1NzMwNTc0OH0.rfuwAZYfalGXhi69MSb0xR7Kbo1SO_umBmm_8UQjARI"; // Replace with your actual key
                string bucketName = "fu-testing";
                string filePath = $"uploads/{DateTime.UtcNow.Ticks}_{inputFile.FileName}";

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("apikey", supabaseKey);
                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {supabaseKey}");

                    using (var content = new MultipartFormDataContent())
                    {
                        using (var stream = inputFile.OpenReadStream())
                        {
                            var fileContent = new StreamContent(stream);
                            content.Add(fileContent, "file", inputFile.FileName);

                            var uploadUrl = $"{supabaseUrl}/storage/v1/object/{bucketName}/{filePath}";
                            var response = await httpClient.PostAsync(uploadUrl, content);

                            if (!response.IsSuccessStatusCode)
                            {
                                _logger.LogError("Supabase Upload Error: " + response.ReasonPhrase);
                                return null;
                            }
                        }
                    }
                }

                // Generate public URL
                return $"{supabaseUrl}/storage/v1/object/public/{bucketName}/{filePath}";
            }
            catch (Exception ex)
            {
                _logger.LogError("File Upload Error: " + ex.Message);
                return null;
            }
        }


        #endregion

        // GET: UserDetails
        public async Task<IActionResult> Index()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    await AppUtil.AddJwtTokenToRequestHeader(httpClient, HttpContext);

                    using (var response = await httpClient
                        .GetAsync(_configuration["Pre-maritalCounSelingAPIEndpoint:Base"] + "QuizResults/UserDetails"))
                    {
                        var userDetails = await AppUtil.GetDeserializedResponseFromApi<List<UserDetail>>(response);
                        if (response.IsSuccessStatusCode)
                        {
                            return View(userDetails);
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

        // GET: UserDetails/Details/5
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
                        .GetAsync(_configuration["Pre-maritalCounSelingAPIEndpoint:Base"] + "QuizResults/UserDetails/" + id))
                    {
                        var userDetail = await AppUtil.GetDeserializedResponseFromApi<UserDetail>(response);
                        if (userDetail == null)
                        {
                            _logger.LogWarning("UserDetail not found");
                            return NotFound();
                        }
                        if (response.IsSuccessStatusCode)
                        {
                            return View(userDetail);
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

        // GET: UserDetails/Create
        public async Task<IActionResult> Create()
        {
            if (!IsValidRole("Admin")) return View("~/Views/ERR_PAGE/403page.cshtml");

            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var userResponse = await httpClient.GetAsync(_configuration["Pre-maritalCounSelingAPIEndpoint:Base"] + "QuizResults/userDetailCateSelectBox"))
                    {
                        var userDetailCates = await AppUtil.GetDeserializedResponseFromApi<List<UserDetailCategory>>(userResponse);
                        ViewBag.CateOption = userDetailCates?.Select(u => new SelectListItem
                        {
                            Value = u.Id.ToString(),
                            Text = u.Name
                        }).ToList();
                    }

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

        // POST: UserDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( CreateUserDetailModel modelRequest)
        {
            if (!IsValidRole("Admin")) return View("~/Views/ERR_PAGE/403page.cshtml");
            // Upload the file to Supabase and get the URL
            UserDetail entity = new UserDetail();
            if (modelRequest.AttachedDocument != null)
            {
                entity.AttachedDocument = await GetFileUploadURL(modelRequest.AttachedDocument);
            }

            try
            {
                string userName = Request.Cookies["UserName"];
                entity.CreatedBy = userName;

                //mapping the value from model to the entity
                entity.Summary = modelRequest.Summary;
                entity.Name = modelRequest.Name;
                entity.ReferedLink = modelRequest.ReferedLink;
                entity.Image = modelRequest.Image;
                entity.StartDate = modelRequest.StartDate;
                entity.EndDate = modelRequest.EndDate;
                entity.CategoryId = modelRequest.CategoryId;
                entity.UserId = modelRequest.UserId;

                using (var httpClient = new HttpClient())
                {
                    await AppUtil.AddJwtTokenToRequestHeader(httpClient, HttpContext);
                    var content = new StringContent(
                     JsonConvert.SerializeObject(entity),
                     Encoding.UTF8, "application/json");
                    using (var response = await httpClient
                       .PostAsync(_configuration["Pre-maritalCounSelingAPIEndpoint:Base"] + "QuizResults/UserDetails", content))
                    {
                        if (!response.IsSuccessStatusCode)
                        {
                            TempData["ErrorMessage"] = "Failed to create.Please try again.";
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


    }
}
