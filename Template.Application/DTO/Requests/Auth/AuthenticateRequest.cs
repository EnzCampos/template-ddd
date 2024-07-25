#nullable disable

namespace Template.Application.DTO.Requests.Auth
{
    public class AuthenticateRequest
    {
        public string TxLogin { get; set; }
        public string TxPassword { get; set; }
    }
}
