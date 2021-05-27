using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net5WebTemplate.Api.Common;
using Net5WebTemplate.Api.Routes.Version1;
using Net5WebTemplate.Application.Auth.Command.ForgotPassword;
using System.Threading.Tasks;

namespace Net5WebTemplate.Api.Controllers.Version1
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiVersion("1.0")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Send Forgot Password email.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     {
        ///         "email":"test@email.com"
        ///     }
        /// </remarks>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <response code="200"> Send Forgot password notification</response>
        /// <response code ="429">Too Many Requests</response>
        [HttpPost]
        [Route(ApiRoutes.Auth.ForgotPassword)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var command = new ForgotPasswordCommand()
            {
                Email = email.ToLower().Trim()
            };

            await _mediator.Send(command);

            return Ok();
        }
    }
}
