using Template.Domain.Identity;

namespace Template.Application.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateTokenFromUser(ApplicationUser user, int idTemplateUser, string userName, IList<string> roles);

        bool ValidateToken(string token);
    }
}
