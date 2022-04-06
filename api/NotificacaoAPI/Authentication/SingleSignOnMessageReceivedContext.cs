using Microsoft.AspNetCore.Authentication;

namespace NotificacaoAPI.Authentication
{
    public class SingleSignOnMessageReceivedContext : ResultContext<SingleSignOnAuthSchema>
    {
        public string? Token { get; set; }

        public SingleSignOnMessageReceivedContext(HttpContext context, AuthenticationScheme scheme, SingleSignOnAuthSchema options) : base(context, scheme, options)
        {
        }
    }
}
