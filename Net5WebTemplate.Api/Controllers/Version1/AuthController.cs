using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net5WebTemplate.Api.Common;
using Net5WebTemplate.Api.Contracts.Version1.Requests;
using Net5WebTemplate.Api.Routes.Version1;
using Net5WebTemplate.Application.Auth.Commands.Login;
using Net5WebTemplate.Application.Auth.Command.ForgotPassword;
using Net5WebTemplate.Application.Auth.Command.RefreshToken;
using Net5WebTemplate.Application.Auth.Command.ResetPassword;
using Net5WebTemplate.Application.Common.Models;
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
        ///  Authenticate user
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     { 
        ///         "email":"test@email.com", 
        ///         "password":"Password"
        ///     }
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <response code ="401">Unauthorized - not authenticated</response>
        /// <response code ="429">Too Many Requests</response>
        [HttpPost]
        [Route(ApiRoutes.Auth.Login)]
        [ProducesResponseType(typeof(TokenResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var command = new LoginCommand
            {
                Email = request.Email.ToLower().Trim(),
                Password = request.Password.Trim()
            };

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        /// <summary>
        /// Refresh JWT tokens.
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     {
        ///         "token":"eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9l",
        ///         "resfreshToken":"IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ"
        ///      }
        ///      
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(ApiRoutes.Auth.RefreshToken)]
        [ProducesResponseType(typeof(TokenResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest request)
        {
            var command = new RefreshTokenCommand
            {
                Token = request.Token,
                RefreshToken = request.RefreshToken.Trim()
            };

            var result = await _mediator.Send(command);

            return Ok(result);
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
        /// <param name="email">The user's email address that was used to register.</param>
        /// <returns></returns>
        /// <response code="200">Success</response>
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

        /// <summary>
        ///  Resets user's password
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     {
        ///         "email":"test@email.com",
        ///         "token":"ascdefghijklmnopqrstuvxyz",
        ///         "password":"password",
        ///         "confirmPassword":"password"
        ///      }
        ///      
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Success </response>
        /// <response code ="429">Too Many Requests</response>
        [HttpPost]
        [Route(ApiRoutes.Auth.ResetPassword)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            var command = new ResetPasswordCommand
            {
                Email = request.Email.ToLower().Trim(),
                Token = request.Token.Trim(),
                Password = request.Password.Trim(),
                ConfirmPassword = request.ConfirmPasword.Trim()
            };

            await _mediator.Send(command);

            return Ok();
        }
    }
}
