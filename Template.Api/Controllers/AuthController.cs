using MediatR;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Commands.Auth;
using Template.Application.DTO.Requests.Auth;
using Template.Application.DTO.Responses.Auth;
using System.ComponentModel.DataAnnotations;

namespace Template.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IMediator _mediator) : ControllerBase
    {
        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<AuthTokenResponse>> Login([FromBody, Required] AuthenticateRequest request)
        {
            var result = await _mediator.Send(new AuthenticateCommand(request.TxLogin, request.TxPassword));

            if (result == null)
            {
                return Unauthorized("Invalid credentials.");
            }

            return Ok(result);
        }
    }
}