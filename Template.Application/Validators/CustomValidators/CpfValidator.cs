using FluentValidation;

namespace Template.Application.Validators.CustomValidators
{
    public static class CpfValidator
    {
        public static IRuleBuilderOptions<T, string> IsCpf<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(IsValidCpf).WithMessage("O CPF informado é inválido.");
        }

        private static bool IsValidCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            var cleanCpf = cpf.Trim().Replace(".", "").Replace("-", "");

            if (cleanCpf.Length != 11 || !cleanCpf.All(char.IsDigit))
                return false;

            if (cleanCpf.All(c => c == cleanCpf[0]))
                return false;

            return IsValidSequence(cleanCpf, [10, 9, 8, 7, 6, 5, 4, 3, 2]) &&
                   IsValidSequence(cleanCpf, [11, 10, 9, 8, 7, 6, 5, 4, 3, 2], 1);
        }

        private static bool IsValidSequence(string cpf, int[] multipliers, int offset = 0)
        {
            var sum = cpf.Take(9 + offset)
                         .Select((digit, index) => (digit - '0') * multipliers[index])
                         .Sum();

            var remainder = sum * 10 % 11;
            if (remainder == 10) remainder = 0;

            return cpf[9 + offset] == remainder + '0';
        }
    }
}