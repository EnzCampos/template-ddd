using MediatR;

namespace Template.Application.Commands.Auth
{
    public record CreateCredentialsCommand(int IdTemplateUser, string TxEmail, string TxPhoneNumber, string TxPassword, string Role) : IRequest<bool>;
}
