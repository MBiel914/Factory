using Factory.API.Core.Contracts;
using Factory.API.Core.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Factory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAuthManager authManager, ILogger<AccountController> logger)
        {
            this._authManager = authManager;
            this._logger = logger;
        }

        [HttpPost]
        [Route("registeruser")]
        public async Task<ActionResult> Register([FromBody] ApiUserDto apiUserDto)
        {
            _logger.LogInformation($"Registration Attempt for {apiUserDto.Email}");
            var errors = await _authManager.RegisterUser(apiUserDto);

            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpPost]
        [Route("registeradminitrator")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> RegisterAdminitrator([FromBody] ApiUserDto apiUserDto)
        {
            var errors = await _authManager.RegisterAdministrator(apiUserDto);

            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            var authResponse = await _authManager.Login(loginDto);

            if (authResponse is null)
            {
                return Unauthorized();
            }

            return Ok(authResponse);
        }
    }
}
