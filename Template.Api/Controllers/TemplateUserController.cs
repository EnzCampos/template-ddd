using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Commands;
using Template.Application.Commands.Auth;
using Template.Application.DTO;
using Template.Application.DTO.Requests;
using System.ComponentModel.DataAnnotations;
using Template.Domain.API;

namespace Template.Api.Controllers
{
    [Route("api/template-user")]
    [ApiController]
    public class TemplateUserController(IMediator _mediator) : ControllerBase
    {
        [HttpGet("{idTemplateUser:int}")]
        [Authorize]
        public async Task<ActionResult<TemplateUserDTO>> GetUserById(int idTemplateUser)
        {
            var result = await _mediator.Send(new GetUserByIdCommand(idTemplateUser));

            return Ok(result);
        }

        [HttpGet("list")]
        [ProducesResponseType(200)]
        [Authorize]
        public async Task<ActionResult<PagedList<TemplateUserDTO>>> GetUserList([FromQuery] GetUserListFilterRequest request, [FromQuery] PagingParams pagingParams)
        {
            var result = await _mediator.Send(new GetUserListCommand(request, pagingParams));

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<bool>> CreateTemplateUser([FromBody] CreateTemplateUserRequest request)
        {
            var result = await _mediator.Send(new CreateUserCommand(request));

            var credentials = await _mediator.Send(new CreateCredentialsCommand(result, request.TxEmail, request.TxPhone, request.TxPassword, request?.CoProfile ?? "USER"));

            if (!credentials)
            {
                throw new Exception("Error creating user credentials");
            }

            return Ok(result);
        }

        [HttpPatch("{idUser:int}")]
        [Authorize]
        public async Task<ActionResult<bool>> UpdateTemplateUser([FromRoute, Required]int idUser, [FromBody, Required] UpdateTemplateUserRequest request)
        {
            var result = await _mediator.Send(new UpdateUserCommand(idUser, request));

            return Ok(result);
        }
    }
}
