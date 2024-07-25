#nullable disable
using Template.Domain.Common;

namespace Template.Domain.Models
{
    public class TemplateUser : BaseEntity
    {
        public int IdUser { get; set; }
        public string TxName { get; set; }
        public string TxCpf { get; set; } // This is a Brazilian document number, remove it if you don't need it
        public string TxEmail { get; set; }
        public string TxPhone { get; set; }
        public DateTime? DtLastLoginDate { get; set; }
        public bool IsActive { get; set; }

        public ICollection<UserProfile> Profiles { get; set; }
    }
}
