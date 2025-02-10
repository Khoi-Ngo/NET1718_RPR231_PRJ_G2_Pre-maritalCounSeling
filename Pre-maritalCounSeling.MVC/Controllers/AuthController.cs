using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Pre_maritalCounSeling.MVC.Models;
using Pre_maritalCounSeling.Common.Util;

namespace Pre_maritalCounSeling.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _configuration;
        public AuthController(ILogger<AuthController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View("Login");
        }
        public async Task<IActionResult> Forbidden()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {

            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(_configuration["Pre-maritalCounSelingAPIEndpoint:Base"] + "Auth/sign-in", request))
                    {
                        var tokenString = (await AppUtil.GetDeserializedResponseFromApi(response))["accessToken"];
                        var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);
                        var userName = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
                        var role = jwtToken.Claims.FirstOrDefault(c => c.Type == "RoleName").Value;

                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, userName),
                            new Claim(ClaimTypes.Role, role),
                        };

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                        Response.Cookies.Append("UserName", userName);
                        Response.Cookies.Append("Role", role);
                        Response.Cookies.Append("TokenString", tokenString);

                        return RedirectToAction("Index", "Home");

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                ModelState.AddModelError("", "Login failure");
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("UserName");
            Response.Cookies.Delete("Role");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }


    }
}
