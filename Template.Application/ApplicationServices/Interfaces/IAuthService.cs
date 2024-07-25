using Template.Application.DTO.Responses.Auth;
using Template.Domain.Identity;

namespace Template.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthTokenResponse?> AuthenticateUserAsync(string txLogin, string txPassword, CancellationToken cancellationToken = default);

        Task<bool> CreateUserCredentialsAsync(ApplicationUser user, string txPassword, string role, CancellationToken cancellationToken = default);
    }
}
