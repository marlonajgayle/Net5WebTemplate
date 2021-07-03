using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net5WebTemplate.Api.Common;
using Net5WebTemplate.Api.Contracts.Version1.Requests;
using Net5WebTemplate.Api.Routes.Version1;
using Net5WebTemplate.Application.Account.Commands.RegisterUserAccount;
using System.Threading.Tasks;

namespace Net5WebTemplate.Api.Controllers.Version1
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new user account.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/v1/register
        ///     {
        ///         "email:"foo@bar.com",
        ///         "password":"password123",
        ///         "confirmPassword:"password123"
        ///     }
        ///     
        /// </remarks>
        /// <response code="201"> Success creating new account user</response>
        /// <response code ="429">Too Many Requests</response>
        [HttpPost]
        [Route(ApiRoutes.Account.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(RegisterRequest request)
        {
            var command = new CreateUserAccountCommand
            {
                Email = request.Email.ToLower().Trim(),
                Password = request.Password.Trim(),
                ConfirmPassword = request.ConfirmPassword.Trim()
            };
            await _mediator.Send(command);

            return Created(ApiRoutes.Account.Create, "");
        }
    }
}
