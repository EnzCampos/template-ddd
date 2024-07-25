using FluentValidation;
using Template.Application.DTO.Requests;
using Template.Application.Validators.CustomValidators;
using Template.Domain.Interfaces.Repositories;

namespace Template.Application.Validators
{
    public class UpdateTemplateUserRequestValidator : AbstractValidator<UpdateTemplateUserRequest> { 
    
        public UpdateTemplateUserRequestValidator(ITemplateUserRepository _templateUserRepository)
        {
            RuleFor(x => x.TxName)
                .NotEmpty();

            RuleFor(x => x.TxEmail)
                .EmailAddress()
                .NotEmpty();

            RuleFor(x => x.TxCpf)
                .IsCpf()
                .NotEmpty();

            RuleFor(x => x.TxPhone)
                .NotEmpty()
                .IsValidPhoneNumber();
        }
    }
}
