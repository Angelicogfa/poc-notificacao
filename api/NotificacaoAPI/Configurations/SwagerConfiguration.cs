using Microsoft.OpenApi.Models;
using NotificacaoAPI.Authentication;

namespace NotificacaoAPI.Configurations
{
    public static class SwagerConfiguration
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiNotificação", Version = "v1" });

                //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                //{
                //    Name = "Authorization",
                //    Type = SecuritySchemeType.ApiKey,
                //    Scheme = "Bearer",
                //    BearerFormat = "JWT",
                //    In = ParameterLocation.Header,
                //    Description = @"JWT Authorization header using the Bearer scheme.
                //   \r\n\r\n Enter 'Bearer'[space] and then your token in the text input below.
                //    \r\n\r\nExample: 'Bearer 12345abcdef'",
                //});
                //c.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    {
                //          new OpenApiSecurityScheme
                //          {
                //              Reference = new OpenApiReference
                //              {
                //                  Type = ReferenceType.SecurityScheme,
                //                  Id = "Bearer"
                //              }
                //          },
                //         new string[] {}
                //    }
                //});

                c.AddSecurityDefinition("SSOOntime", new OpenApiSecurityScheme 
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Name = SingleSignOnSchemaConstants.SingleSignOnHeader,
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "SSOOntime"
                              }
                          },
                         new string[] {}
                    }
                });

            });
            return services;
        }
    }
}
