namespace Template.Application.DTO.Requests
{
    public class CreateTemplateUserRequest
    {
        public string TxName { get; set; } = string.Empty;
        public string TxEmail { get; set; } = string.Empty;
        public string TxCpf { get; set; } = string.Empty;
        public string TxPhone { get; set; } = string.Empty;
        public string TxPassword { get; set; } = string.Empty;
        public string? CoProfile { get; set; }
    }
}
