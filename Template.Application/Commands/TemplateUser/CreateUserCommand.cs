using MediatR;
using Template.Application.DTO.Requests;

namespace Template.Application.Commands
{
    public record CreateUserCommand(CreateTemplateUserRequest Request) : IRequest<int>;
}
