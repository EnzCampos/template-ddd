#nullable disable
using Template.Domain.Common;

namespace Template.Domain.Models;
public class UserProfile : BaseEntity
{   
    public string CoProfile { get; set; }
    public string TxProfile { get; set; }
}
