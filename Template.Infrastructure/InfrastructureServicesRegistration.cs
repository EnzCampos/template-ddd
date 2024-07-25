using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Template.Domain.Interfaces.Repositories;
using Template.Domain.Interfaces.Services.UnitOfWork;
using Template.Infrastructure.Services.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Template.Domain.Identity;
using Template.Identity.DatabaseContext;
using Template.Infrastructure.DatabaseContext;
using Template.Infrastructure.Repositories;

namespace Template.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("TemplateDatabaseConnectionString");

        var mySQLVersion = ServerVersion.AutoDetect(connectionString);

        services.AddDbContext<TemplateDatabaseContext>(options =>
        {
            options.UseMySql(connectionString, mySQLVersion);
        });

        services.AddDbContext<IdentityDatabaseContext>(options =>
        {
            options.UseMySql(connectionString, mySQLVersion);
        });

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<IdentityDatabaseContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<ITemplateUserRepository, TemplateUserRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}