using Microsoft.AspNetCore.Authentication;

namespace NotificacaoAPI.Authentication
{
    public class SingleSignOnAuthSchema : AuthenticationSchemeOptions
    {
        public string? UrlBase { get; set; }
        public Guid AuthGuid { get; set; }
        public string? AuthSecret { get; set; }
        public new SingleSignOnEvents? Events { get; set; }
    }
}
