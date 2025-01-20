using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pre_maritalCounSeling.BAL.Auth;
using Pre_maritalCounSeling.BAL.ServiceUser;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pre_maritalCounSeling.API.Controllers
{
    #region DTOs
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    #endregion

    [ApiVersion("1.0")]
    [Route("api/{apiversion}/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Init
        private readonly ILogger<AuthController> _logger;
        private readonly IUserService _userService;
        private readonly JWTService _jwtService;
        public AuthController(ILogger<AuthController> logger, IUserService userService, JWTService jWTService)
        {
            _logger = logger;
            _userService = userService;
            _jwtService = jWTService;
        }

        #endregion

        //Authenticate
        [HttpPost("sign-in")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            try
            {
                var result = await _userService.LoginAsync(request.UserName, request.Password);
                if (result is null)
                {
                    return BadRequest("Invalid username or password");
                }
                return Ok(_jwtService.GenerateAccessToken(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //Sign-up

        //Refresh token
    }
}
