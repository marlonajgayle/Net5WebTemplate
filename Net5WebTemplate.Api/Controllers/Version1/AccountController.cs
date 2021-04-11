using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net5WebTemplate.Api.Routes.Version1;
using Net5WebTemplate.Application.Account.Commands.Login;
using Net5WebTemplate.Application.Account.Commands.RegisterUserAccount;
using System.Threading.Tasks;

namespace Net5WebTemplate.Api.Controllers.Version1
{
    [Produces("application/json")]
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
        /// Registers new Account User for platform
        /// </summary>
        /// <remarks>
        /// POST /api/v1/register
        /// {
        ///     "email:"foo@bar.com",
        ///     "password":"password123",
        ///     "confirmPassword:"password123"
        /// }
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        [Route(ApiRoutes.Account.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateUserAccountCommand command)
        {
            await _mediator.Send(command);

            return Created(ApiRoutes.Account.Create, "");
        }

        /// <summary>
        ///  Login
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(ApiRoutes.Account.Login)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
