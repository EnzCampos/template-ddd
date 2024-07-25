using Ardalis.SmartEnum;

namespace Template.Domain.Constants
{
    public sealed class IdentityRoleConstant : SmartEnum<IdentityRoleConstant, string>
    {
        public static readonly IdentityRoleConstant Admin = new("Admin", "Admin");
        public static readonly IdentityRoleConstant User = new("User", "User");
        private IdentityRoleConstant(string name, string role) : base(name, role)
        { }
    }
}
