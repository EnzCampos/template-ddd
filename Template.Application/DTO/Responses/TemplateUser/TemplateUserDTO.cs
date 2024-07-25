namespace Template.Application.DTO
{
    public class TemplateUserDTO
    {
        public int IdUser { get; set; }
        public string TxName { get; set; } = string.Empty;
        public string? TxCpf { get; set; }
        public string TxEmail { get; set; } = string.Empty;
        public string TxPhone { get; set; } = string.Empty;
        public HashSet<string> Profiles { get; set; } = [];
    }
}
