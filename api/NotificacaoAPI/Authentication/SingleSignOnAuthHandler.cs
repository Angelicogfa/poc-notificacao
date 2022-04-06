using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;

namespace NotificacaoAPI.Authentication
{
    public class SingleSignOnAuthHandler : AuthenticationHandler<SingleSignOnAuthSchema>
    {
        private readonly IMemoryCache cache;
        public SingleSignOnAuthHandler(IOptionsMonitor<SingleSignOnAuthSchema> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IMemoryCache cache) : base(options, logger, encoder, clock)
        {
            this.cache = cache;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(SingleSignOnSchemaConstants.SingleSignOnHeader))
                return AuthenticateResult.Fail("Header Not Found.");

            var header = Request.Headers[SingleSignOnSchemaConstants.SingleSignOnHeader].ToString();
            if(string.IsNullOrEmpty(header))
                return AuthenticateResult.Fail("Model is Empty");

            var token = await GetTokenFromHashAsync(header);

            if (token == null) return AuthenticateResult.Fail("Token not found for hash");
            else if (!token.IsAuthenticated) return AuthenticateResult.Fail(token.ErrorMessage!);

            if (Options.Events is not null)
                await Options.Events.MessageReceived(new SingleSignOnMessageReceivedContext(Context, Scheme, Options));

            var handler = new JwtSecurityTokenHandler();
            var json = handler.ReadJwtToken(token.Token!);
            PayloadData payload = JsonConvert.DeserializeObject<PayloadData>(json.Payload.SerializeToJson())!;

            if (!PayloadIsValid(payload))
                return AuthenticateResult.Fail("Payload is invalid to application app");

            return AuthenticateResult.Success(GetPrincipal(payload.Payload));
        }

        private AuthenticationTicket GetPrincipal(PayloadToken payload)
        {
            IIdentity identity = new GenericIdentity(payload.Sbn);
            var app = payload.Apps.FirstOrDefault(t => t.Guid == Options.AuthGuid);
            var claims = app?.Claims.Select(t => new Claim(t.Type, t.Value)) ?? Enumerable.Empty<Claim>();

            var claimsIdentity = new ClaimsIdentity(identity, claims, nameof(SingleSignOnAuthHandler), null, null);

            return new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity), Scheme.Name);
        }

        private bool PayloadIsValid(PayloadData payload)
        {
            return true;
        }

        private async Task<HashTokenResult?> GetTokenFromHashAsync(string hash)
        {
            return await cache.GetOrCreateAsync(hash, async (cache) => {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(Options.UrlBase!)
                };
                client.DefaultRequestHeaders.Add("HB-AUTH-GUID", Options.AuthGuid.ToString());
                client.DefaultRequestHeaders.Add("HB-AUTH-SECRET", Options.AuthSecret);

                var result = await client.GetAsync($"/api/Token/GetTokenFromHash/{hash}");
                if (result == null || result.StatusCode != System.Net.HttpStatusCode.OK)
                    return null;

                var payload = JsonConvert.DeserializeObject<HashTokenResult>(await result.Content.ReadAsStringAsync());

                if(payload is not null)
                    cache.SetValue(payload);

                return payload;
            });
        }

        class PayloadData
        {
            public PayloadHeader Header { get; set; }
            public PayloadToken Payload { get; set; }
        }

        class PayloadHeader
        {
            public Guid Tid { get; set; }
            public string Iss { get; set; } = string.Empty;
            public Guid Ist { get; set; }
            public string Rfr { get; set; } = string.Empty;
        }

        class PayloadToken
        {
            public string Sub { get; set; } = string.Empty;
            public string Eml { get; set; } = string.Empty;
            public string Sbn { get; set; } = string.Empty;
            public string Ccr { get; set; } = string.Empty;
            public string Cen { get; set; } = string.Empty;
            public bool Mad { get; set; }
            public bool Emp { get; set; }
            public int Mex { get; set; }
            public DateTime Iat { get; set; }
            public DateTime Exp { get; set; }

            public IEnumerable<PayloadApp> Apps { get; set; } = Enumerable.Empty<PayloadApp>();
        }

        class PayloadApp
        {
            public string Name { get; set; } = string.Empty;
            public string FriendlyName { get; set; } = string.Empty;
            public string Issuer { get; set; } = string.Empty;
            public Guid Guid { get; set; }
            public bool AppIsLocked { get; set; }
            public bool IsLockedForUser { get; set; }
            public IEnumerable<PayloadAppClaim> Claims { get; set; } = Enumerable.Empty<PayloadAppClaim>();
        }

        class PayloadAppClaim
        {
            public string Type { get; set; } = string.Empty;
            public string Value { get; set; } = string.Empty;
        }

        class HashTokenResult
        {
            public Guid? ApplicationGuid { get; set; }
            public DateTime IssueAt { get; set; }
            public DateTime ExpiresOn { get; set; }
            public bool IsAuthenticated { get; set; }
            public bool LoginFromEmployeeArea { get; set; }
            public string? UserKey { get; set; }
            public string? Token { get; set; }
            public Guid? TokenHash { get; set; }
            public string? ErrorMessage { get; set; }
        }
    }
}
