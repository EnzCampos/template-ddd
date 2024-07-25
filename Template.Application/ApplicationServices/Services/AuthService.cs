using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Template.Application.DTO.Responses.Auth;
using Template.Application.Exceptions;
using Template.Application.Interfaces.Services;
using Template.Domain.Constants;
using Template.Domain.Identity;
using Template.Domain.Interfaces.Repositories;
using Template.Domain.Models;
using Template.Domain.Specs;

namespace Template.Application.Services
{
    public class AuthService(
        UserManager<ApplicationUser> _userManager, 
        ITemplateUserRepository _templateUserRepository, 
        ITokenService _tokenService) : IAuthService
    {

        public async Task<AuthTokenResponse?> AuthenticateUserAsync(string txLogin, string txPassword, CancellationToken cancellationToken = default)
        {
            ApplicationUser? user = null;

            if (txLogin.Contains('@'))
            {
                user = await _userManager.FindByEmailAsync(txLogin);
            }
            else
            {
                user = await _userManager.Users.SingleOrDefaultAsync(u => u.PhoneNumber == txLogin, cancellationToken: cancellationToken);
            }

            if (user != null && await _userManager.CheckPasswordAsync(user, txPassword))
            {
                var roles = await _userManager.GetRolesAsync(user);

                var userSpec = new TemplateUserSpec.GetById(user.IdTemplateUser);
                var userEntity = await _templateUserRepository.QueryWithSpecification(userSpec).FirstOrDefaultAsync(cancellationToken)
                    ?? throw new NotFoundException(nameof(TemplateUser), user.IdTemplateUser);

                var isUserFirstLogin = userEntity.DtLastLoginDate == null;
                
                userEntity.DtLastLoginDate = DateTime.Now;

                await _templateUserRepository.UpdateAsync(userEntity, cancellationToken);

                var response = new AuthTokenResponse { 
                    Token = _tokenService.GenerateTokenFromUser(user, user.IdTemplateUser, userEntity.TxName, roles),
                    ExpiresIn = DateTime.Now.AddDays(1),
                    RefreshToken = String.Empty, // Not implemented yet
                    IsFirstAccess = isUserFirstLogin
                };

                return response;
            }

            return null;
        }

        public async Task<bool> CreateUserCredentialsAsync(ApplicationUser user, string txPassword, string role, CancellationToken cancellationToken = default)
        {
            var result = await _userManager.CreateAsync(user, txPassword);

            if (result.Succeeded)
            {
                var identityRole = role switch
                {
                    var e when e.Equals(UserProfileConstant.Admin.Value) => IdentityRoleConstant.Admin,
                    var e when e.Equals(UserProfileConstant.User.Value) => IdentityRoleConstant.User,
                    _ => throw new BadRequestException("Perfil inválido.")
                };

                await _userManager.AddToRoleAsync(user, identityRole);
            }

            else
            {
                throw new Exception("Não foi possível criar o usuário.");
            }

            return result.Succeeded;
        }
    }
}
