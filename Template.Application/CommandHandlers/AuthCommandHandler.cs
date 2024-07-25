using MediatR;
using Template.Application.Commands.Auth;
using Template.Application.DTO.Responses.Auth;
using Template.Application.Interfaces.Services;
using Template.Domain.Identity;

namespace Template.Application.CommandHandlers
{
    public class AuthCommandHandler(
        IAuthService _authService)
        : IRequestHandler<AuthenticateCommand, AuthTokenResponse?>,
        IRequestHandler<CreateCredentialsCommand, bool>
    {
        public async Task<AuthTokenResponse?> Handle(AuthenticateCommand request, CancellationToken cancellationToken = default)
        {
            var result = await _authService.AuthenticateUserAsync(request.TxLogin, request.TxPassword, cancellationToken);

            return result;
        }

        public async Task<bool> Handle(CreateCredentialsCommand request, CancellationToken cancellationToken = default)
        {
            var identityUser = new ApplicationUser
            {
                UserName = request.TxEmail,
                Email = request.TxEmail,
                PhoneNumber = request.TxPhoneNumber,
                IdTemplateUser = request.IdTemplateUser,
                PhoneNumberConfirmed = true
            };

            return await _authService.CreateUserCredentialsAsync(identityUser, request.TxPassword, request.Role, cancellationToken);
        }
    }
}
