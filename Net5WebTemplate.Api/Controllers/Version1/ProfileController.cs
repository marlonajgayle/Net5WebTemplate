using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net5WebTemplate.Api.Contracts.Version1.Requests;
using Net5WebTemplate.Api.Routes.Version1;
using Net5WebTemplate.Application.Profiles.Commands.CreateProfile;
using Net5WebTemplate.Application.Profiles.Commands.DeleteProfile;
using Net5WebTemplate.Application.Profiles.Queries.GetProfileById;
using Net5WebTemplate.Application.Profiles.Queries.GetProfiles;
using System.Net;
using System.Threading.Tasks;

namespace Net5WebTemplate.Api.Controllers.Version1
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProfileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create user's profile
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="201">Success creating new account user</response>
        /// <response code ="429">Too Many Requests</response>
        [HttpPost]
        [Route(ApiRoutes.Profile.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ProfileRequest request)
        {
            var command = new CreateProfileCommand
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                AddressLine1 = request.AddressLine1,
                AddressLine2 = request.AddressLine2,
                Parish = request.Parish,
                PhoneNumber = request.PhoneNumber
            };

            await _mediator.Send(command);

            return Created("/profile", "");
        }

        /// <summary>
        /// Retrieve user profile list
        /// </summary>
        /// <returns></returns>
        /// <response code = "200" >Success in retrieving user profile list</response>
        /// <response code ="429">Too Many Requests</response>
        [HttpGet]
        [Route(ApiRoutes.Profile.GetAll)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetProfilesQuery());

            return Ok(result);
        }

        /// <summary>
        /// Retrieve User profile details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Success retrieving user profile details</response>
        /// <response code="404">Unable to find Profile record </response>
        /// <response code ="429">Too Many Requests</response>
        [HttpGet]
        [Route(ApiRoutes.Profile.Get)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetProfileByIdQuery()
            {
                Id = id
            };

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        /// <summary>
        /// Delete user profile entry
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">Success deleting user profile entry</response>
        /// <response code="404">Unable to find record to delete</response>
        /// <response code ="429">Too Many Requests</response>
        [HttpDelete]
        [Route(ApiRoutes.Profile.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteProfileCommand()
            {
                Id = id
            };

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
