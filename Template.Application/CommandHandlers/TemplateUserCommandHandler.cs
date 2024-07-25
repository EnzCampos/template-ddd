using MediatR;
using Microsoft.EntityFrameworkCore;
using Template.Application.Commands;
using Template.Application.DTO;
using Template.Application.Exceptions;
using Template.Application.Interfaces.Services;
using Template.Domain.API;
using Template.Domain.Interfaces.Repositories;
using Template.Domain.Models;
using Template.Domain.Specs;

namespace Template.Application.CommandHandlers
{
    public class TemplateUserCommandHandler(
            ITemplateUserRepository _templateUserRepository,
            IUserContextService _userContextService
        ) :
        IRequestHandler<GetUserByIdCommand, TemplateUserDTO>,
        IRequestHandler<CreateUserCommand, int>,
        IRequestHandler<UpdateUserCommand, bool>,
        IRequestHandler<GetUserListCommand, PagedList<TemplateUserDTO>>
    {

        public async Task<TemplateUserDTO> Handle(GetUserByIdCommand request, CancellationToken cancellationToken)
        {
            var spec = new TemplateUserSpec.GetById(request.IdUser);

            var isCurrentUser = _userContextService.GetUserId() == request.IdUser;

            var templateUser = await _templateUserRepository.QueryWithSpecification(spec)
                .AsNoTracking()
                .Select(user => new TemplateUserDTO
                {
                    IdUser = user.IdUser,
                    TxName = user.TxName,
                    TxCpf = isCurrentUser ? user.TxCpf : null, // This is sensitive data, so we should not return it to other users
                    TxEmail = user.TxEmail,
                    TxPhone = user.TxPhone,
                })
                .FirstOrDefaultAsync(cancellationToken)
                    ?? throw new NotFoundException(nameof(TemplateUser), request.IdUser);

            return templateUser;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var req = request.Request;

            var templateUser = new TemplateUser
            {
                TxName = req.TxName,
                TxCpf = req.TxCpf.Trim().Replace(".", "").Replace("-", ""),
                TxEmail = req.TxEmail,
                TxPhone = req.TxPhone,
                IsActive = true,
            };

            var result = await _templateUserRepository.AddAsync(templateUser, cancellationToken);

            return result.IdUser;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var spec = new TemplateUserSpec.GetById(request.IdUser);
            var user = await _templateUserRepository.QueryWithSpecification(spec)
                .FirstOrDefaultAsync(cancellationToken)
                    ?? throw new NotFoundException(nameof(TemplateUser), request.IdUser);

            user.TxName = request.Request.TxName;
            user.TxEmail = request.Request.TxEmail;
            user.TxCpf = request.Request.TxCpf;
            user.TxPhone = request.Request.TxPhone;

            await _templateUserRepository.UpdateAsync(user, cancellationToken);

            return true;
        }

        public async Task<PagedList<TemplateUserDTO>> Handle(GetUserListCommand request, CancellationToken cancellationToken)
        {
            var spec = new TemplateUserSpec.GetByFilters(request.Filters.TxPhone, request.Filters.TxEmail, request.Filters.TxName, request.Filters.CoProfile);

            var query = _templateUserRepository.QueryWithSpecification(spec)
                .Select(user => new TemplateUserDTO
                {
                    IdUser = user.IdUser,
                    TxName = user.TxName,
                    TxEmail = user.TxEmail,
                    TxPhone = user.TxPhone,
                    TxCpf = user.TxCpf,
                }).AsQueryable();

            var list = await _templateUserRepository.PagedListAsync(query, request.PagingParams, cancellationToken);
            return list;
        }
    }
}
