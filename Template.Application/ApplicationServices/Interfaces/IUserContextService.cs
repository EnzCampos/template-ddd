using Template.Domain.API;

namespace Template.Application.Interfaces.Services
{
    public interface IUserContextService
    {
        int GetUserId();
        List<string> GetUserProfiles();

        string GetUserEmail();
        string GetUserPhoneNumber();
        string GetUserUserName();
    }
}
