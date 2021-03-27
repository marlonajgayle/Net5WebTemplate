using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net5WebTemplate.Api.Routes.Version1;
using Net5WebTemplate.Application.Clients.Queries.GetClientDetail;
using System.Threading.Tasks;

namespace Net5WebTemplate.Api.Controllers.Version1
{
    [Produces("application/json")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrives Client detail by using client Id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/clients/1
        /// </remarks>
        /// <param name="clientId"></param>
        /// <returns>Client information details</returns>
        /// <response code="200">Returns client information details</response>
        /// <response code="404">If the client is not found</response>
        [HttpGet(ApiRoutes.Client.Get)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int clientId)
        {
            var query = new GetClientDetailQuery { ClientId = clientId };
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}
