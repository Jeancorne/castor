using Api.Application.Interface;
using Api.Common.Dto.ClsUser;
using Api.Common.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Presentation.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        /// <summary>
        /// Iniciar sesión en el sistema
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost("loginSystem")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ActionResult<IActionResult>), StatusCodes.Status200OK)]
        public async Task<IActionResult> loginSystem(LoginUserDto login)
        {
            try
            {
                return Ok(await _userServices.loginSystem(login));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiException(e));
            }
        }

        /// <summary>
        /// Registrar usuario
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPost("registerUser")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ActionResult<IActionResult>), StatusCodes.Status200OK)]
        public async Task<IActionResult> registerUser(AddUserDto userDto)
        {
            try
            {
                return Ok(await _userServices.registerUser(userDto));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiException(e));
            }
        }
    }
}