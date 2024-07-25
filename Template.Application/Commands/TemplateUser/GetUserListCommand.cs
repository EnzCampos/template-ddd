using MediatR;
using Template.Application.DTO;
using Template.Application.DTO.Requests;
using Template.Domain.API;

namespace Template.Application.Commands
{
    public record GetUserListCommand(GetUserListFilterRequest Filters, PagingParams PagingParams) : IRequest<PagedList<TemplateUserDTO>>;
}