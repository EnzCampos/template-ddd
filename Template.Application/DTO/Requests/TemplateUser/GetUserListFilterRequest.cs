namespace Template.Application.DTO.Requests
{
    public class GetUserListFilterRequest
    {
        public string TxName { get; set; } = string.Empty;
        public string TxEmail { get; set; } = string.Empty;
        public string TxPhone { get; set; } = string.Empty;
        public HashSet<string> CoProfile { get; set; } = [];
    }
}
