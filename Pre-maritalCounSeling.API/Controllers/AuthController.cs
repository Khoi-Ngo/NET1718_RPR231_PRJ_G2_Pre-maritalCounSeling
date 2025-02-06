using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pre_maritalCounSeling.BAL.Auth;
using Pre_maritalCounSeling.BAL.ServiceUser;
using Pre_maritalCounSeling.Common.DTOs;
using Pre_maritalCounSeling.Common.DTOs.Auth;
using Pre_maritalCounSeling.DAL.Entities;

namespace Pre_maritalCounSeling.API.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Init
        private readonly ILogger<AuthController> _logger;
        private readonly IUserService _userService;
        private readonly JWTService _jwtService;
        public AuthController(ILogger<AuthController> logger, IUserService userService, JWTService jwtService)
        {
            _logger = logger;
            _userService = userService;
            _jwtService = jwtService;
        }

        #endregion

        //Authenticate
        [HttpPost("sign-in")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            try
            {
                return Ok(await _userService.LoginAsync(request.UserName, request.Password));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest("Something wrong when login");
            }
        }

        //Sign-up
        [HttpPost("sign-up")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserRequest request)
        {
            try
            {
                await _userService.RegisterAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest("Something wrong when sign up");
            }
        }

    }
}