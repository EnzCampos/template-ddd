using Microsoft.AspNetCore.Http;
using Template.Application.Interfaces.Services;
using System.Security.Claims;

namespace Template.Application.Services
{
    public class UserContextService(IHttpContextAccessor _httpContextAccessor) : IUserContextService
    {
        private const string unauthorizedMessage = "Usuário não autorizado";

        public int GetUserId()
        {
            if (int.TryParse(_httpContextAccessor.HttpContext?.User.FindFirst("IdTemplateUser")?.Value, out int idUser))
            {
                return idUser;
            }

            else throw new UnauthorizedAccessException(unauthorizedMessage);
        }

        public List<string> GetUserProfiles()
        {
            var context = _httpContextAccessor.HttpContext?.User;

            var roles = context?.Claims.Where(x => x.Type == ClaimTypes.Role)
                .Select(x => x.Value)
                    ?? throw new UnauthorizedAccessException(unauthorizedMessage);

            return roles.ToList();
        }

        public string GetUserEmail()
        {
            if (_httpContextAccessor.HttpContext?.User.FindFirst("txEmail") is Claim email)
            {
                return email.Value;
            }

            else throw new UnauthorizedAccessException(unauthorizedMessage);
        }

        public string GetUserPhoneNumber()
        {
            if (_httpContextAccessor.HttpContext?.User.FindFirst("txPhone") is Claim phoneNumber)
            {
                return phoneNumber.Value;
            }

            else throw new UnauthorizedAccessException(unauthorizedMessage);
        }

        public string GetUserUserName()
        {
            if (_httpContextAccessor.HttpContext?.User.FindFirst("txName") is Claim userName)
            {
                return userName.Value;
            }

            else throw new UnauthorizedAccessException(unauthorizedMessage);
        }
    }
}
