#nullable disable
using Microsoft.AspNetCore.Identity;
using Template.Domain.Models;

namespace Template.Domain.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public int IdTemplateUser { get; set; }
    }
}
