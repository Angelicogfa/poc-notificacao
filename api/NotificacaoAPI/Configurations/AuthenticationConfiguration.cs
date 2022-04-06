using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NotificacaoAPI.Authentication;
using System.Text;

namespace NotificacaoAPI.Configurations
{
    public static class AuthenticationConfiguration
    {
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            //var key = Encoding.ASCII.GetBytes(configuration["Secret:Hash"]);

            //services.AddAuthentication(t =>
            //{
            //    t.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    t.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(x =>
            //{
            //    x.RequireHttpsMetadata = false;
            //    x.SaveToken = true;
            //    x.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(key),
            //        ValidateIssuer = false,
            //        ValidateAudience = false
            //    };

            //    x.Events = new JwtBearerEvents
            //    {
            //        OnMessageReceived = context =>
            //        {
            //            string? accessToken = context.Request.Headers.Authorization;
            //            string? token = accessToken?.Split(' ')[1];
            //            var path = context.HttpContext.Request.Path;
            //            if (!string.IsNullOrEmpty(token) && path.StartsWithSegments("/notifications"))
            //            {
            //                context.Token = token;
            //            }

            //            return Task.CompletedTask;
            //        }
            //    };
            //});

            services.AddAuthentication(options => 
            {
                options.DefaultScheme = SingleSignOnSchemaConstants.SingleSignOnAuthSchema;
            })
            .AddScheme<SingleSignOnAuthSchema, SingleSignOnAuthHandler>(SingleSignOnSchemaConstants.SingleSignOnAuthSchema, options => 
            {
                options.AuthGuid = Guid.Parse(configuration["SingleSignOn:ClientId"]);
                options.AuthSecret = configuration["SingleSignOn:Secret"];
                options.UrlBase = configuration["SingleSignOn:Url"];

                options.Events = new SingleSignOnEvents
                {
                    OnMessageReceived = context =>
                    {
                        string? token = context.Request.Headers[SingleSignOnSchemaConstants.SingleSignOnHeader];
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(token) && path.StartsWithSegments("/notifications"))
                        {
                            context.Token = token;
                        }

                        return Task.CompletedTask;
                    }
                };
            });

            services.AddMemoryCache();

            return services;
        }
    }
}
