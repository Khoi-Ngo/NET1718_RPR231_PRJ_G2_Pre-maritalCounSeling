using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pre_maritalCounSeling.BAL.Auth;
using Pre_maritalCounSeling.Common.DTOs.Auth;

namespace Pre_maritalCounSeling.API.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {

        #region Init
        private readonly ILogger<TokenController> _logger;
        private readonly JWTService _jwtService;
        public TokenController(ILogger<TokenController> logger, JWTService jWTService)
        {
            _logger = logger;
            _jwtService = jWTService;
        }

        #endregion

        //refresh token
        [HttpPost("refresh")]
        [AuthenticateOnly]
        public async Task<IActionResult> Refresh(RefreshRequest request)
        {
            try
            {
                return Ok(await _jwtService.Refresh(request));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }

        [HttpPost("revoke")]
        public async Task<IActionResult> Revoke()
        {
            await _jwtService.HandleRevoke();
            return NoContent();
        }
    }
}
