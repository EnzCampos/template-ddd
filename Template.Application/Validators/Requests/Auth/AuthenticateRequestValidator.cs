using FluentValidation;
using Template.Application.DTO.Requests.Auth;
using Template.Application.Validators.CustomValidators;

namespace Template.Application.Validators
{
    public class AuthenticateRequestValidator : AbstractValidator<AuthenticateRequest>
    {
        public AuthenticateRequestValidator()
        {
            RuleFor(x => x.TxLogin)
                .NotEmpty()
                .MaximumLength(255)
                .EmailAddress().When(x => x.TxLogin.Contains('@'))
                .IsValidPhoneNumber().When(x => !x.TxLogin.Contains('@'));
        }
    }
}
