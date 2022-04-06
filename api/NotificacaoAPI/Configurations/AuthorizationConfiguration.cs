using NotificacaoAPI.Authentication;

namespace NotificacaoAPI.Configurations
{
    public static class AuthorizationConfiguration
    {
        public static IServiceCollection AddCustomAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(opt => 
            {
                opt.AddPolicy("Reader", builder => 
                {
                    opt.DefaultPolicy = builder
                    .RequireAuthenticatedUser()
                    .RequireClaim("user_integration", "nao")
                    .Build();
                });

                opt.AddPolicy("Writer", builder =>
                {
                    builder
                    .RequireAuthenticatedUser()
                    .RequireClaim("user_integration", "sim")
                    .Build();
                });
            });
            return services;
        }
    }
}
