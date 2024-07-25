using MediatR;
using Template.Application.DTO;

namespace Template.Application.Commands
{
    public record GetUserByIdCommand(int IdUser) : IRequest<TemplateUserDTO>;
}