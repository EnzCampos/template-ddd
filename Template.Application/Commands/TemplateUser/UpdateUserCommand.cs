using MediatR;
using Template.Application.DTO.Requests;

namespace Template.Application.Commands
{
    public record UpdateUserCommand(int IdUser, UpdateTemplateUserRequest Request) : IRequest<bool>;
}
