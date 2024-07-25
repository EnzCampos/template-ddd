using MediatR;
using Template.Application.DTO.Responses.Auth;

namespace Template.Application.Commands.Auth
{
    public record AuthenticateCommand(string TxLogin, string TxPassword) : IRequest<AuthTokenResponse?>;
}
