using Factory.API.Core.Contracts;
using Factory.API.Core.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Factory.API.Controllers
{
    [Route("api/v{version:apiVersion}/accounts")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AccountsController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(IAuthManager authManager, ILogger<AccountsController> logger)
        {
            this._authManager = authManager;
            this._logger = logger;
        }

        [HttpPost("register-user")]
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

        [HttpPost("register-administrator")]
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
