using FluentValidation;

namespace Template.Application.Validators.CustomValidators
{
    public static class SpecialCharValidator
    {
        private static readonly string SpecialCharacters = "@#$%^&*()_+{}:\"<>?|[];',./`~!";

        public static IRuleBuilderOptions<T, string> HasSpecialCharacter<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(str => str.Any(c => SpecialCharacters.Contains(c))).WithMessage("Password must contain at least one special character.");
        }
    }
}