using Ardalis.SmartEnum;

namespace Template.Domain.Constants
{
    public sealed class UserProfileConstant : SmartEnum<UserProfileConstant, string>
    {
        public static readonly UserProfileConstant Admin = new("Admin", "ADMN");
        public static readonly UserProfileConstant User = new("User", "USER");
        private UserProfileConstant(string name, string role) : base(name, role)
        { }
    }
}
