using FluentValidation;
using System.Text.RegularExpressions;

namespace Template.Application.Validators.CustomValidators
{
    public static partial class PhoneValidator
    {
        public static IRuleBuilderOptions<T, string> IsValidPhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(IsValidPhone).WithMessage("Phone number is invalid.");
        }

        private static bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            var cleanPhone = phone.Trim().Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            var regex = PhoneRegex();

            return regex.IsMatch(cleanPhone);
        }

        [GeneratedRegex(@"^\+?\d{0,3}\s?\(?\d{2,3}\)?[\s.-]?\d{4}[\s.-]?\d{4}$")]
        private static partial Regex PhoneRegex();
    }
}