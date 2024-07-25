using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using FluentValidation;
using Template.Application.Services;
using Template.Application.Interfaces.Services;

namespace Template.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices (this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation();

            services.AddSingleton<ITokenService, TokenService>();
            services.AddScoped<IUserContextService, UserContextService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
