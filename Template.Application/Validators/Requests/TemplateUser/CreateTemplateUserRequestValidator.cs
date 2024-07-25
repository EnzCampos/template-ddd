using FluentValidation;
using Template.Application.DTO.Requests;
using Template.Application.Validators.CustomValidators;
using Template.Domain.Interfaces.Repositories;
using Template.Domain.Specs;

namespace Template.Application.Validators
{
    public class CreateTemplateUserRequestValidator : AbstractValidator<CreateTemplateUserRequest> {
        public CreateTemplateUserRequestValidator(ITemplateUserRepository templateUserRepository)
        {
            var templateUserRepository1 = templateUserRepository;

            RuleFor(x => x.TxName)
                .NotEmpty();

            RuleFor(x => x.TxEmail)
                .EmailAddress()
                .NotEmpty()
                .MustAsync(async (email, cancellation) =>
                {
                    var userByEmailSpec = new TemplateUserSpec.GetByEmail(email);
                    return !await templateUserRepository1.AnyWithSpecAsync(userByEmailSpec, cancellation);
                }).WithMessage("Email already in use.");

            RuleFor(x => x.TxCpf)
                .IsCpf()
                .NotEmpty()
                .MustAsync(async (cpf, cancellation) =>
                {
                    var userByCpfSpec = new TemplateUserSpec.GetByCpf(cpf);
                    return !await templateUserRepository1.AnyWithSpecAsync(userByCpfSpec, cancellation);
                }).WithMessage("CPF já cadastrado.");

            RuleFor(x => x.TxPhone)
                .NotEmpty()
                .IsValidPhoneNumber()
                .MustAsync(async (phone, cancellation) =>
                {
                    var userByPhoneSpec = new TemplateUserSpec.GetByPhone(phone);
                    return !await templateUserRepository1.AnyWithSpecAsync(userByPhoneSpec, cancellation);
                }).WithMessage("Phone number already in use.");

            RuleFor(x => x.TxPassword)
                .NotEmpty()
                .Must(x => x.Any(char.IsUpper)).WithMessage("Password must contain at least one uppercase letter.")
                .Must(x => x.Any(char.IsLower)).WithMessage("Password must contain at least one lowercase letter.")
                .Must(x => x.Any(char.IsDigit)).WithMessage("Password must contain at least one digit.")
                .HasSpecialCharacter();
        }
    }
}
