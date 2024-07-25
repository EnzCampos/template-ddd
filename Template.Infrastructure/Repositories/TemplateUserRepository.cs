using Template.Domain.Interfaces.Repositories;
using Template.Domain.Interfaces.Services.UnitOfWork;
using Template.Domain.Models;
using Template.Infrastructure.DatabaseContext;

namespace Template.Infrastructure.Repositories
{
    public class TemplateUserRepository(TemplateDatabaseContext dbContext, IUnitOfWork unitOfWork)
        : BaseRepository<TemplateUser>(dbContext, unitOfWork), ITemplateUserRepository
    {
    }
}
