using NotificacaoAPI.Configurations;
using NotificacaoAPI.Hub;
using NotificacaoAPI.Jobs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddDatabaseConfigurations(builder.Configuration);
builder.Services.AddServiceBusConfiguration(builder.Configuration);
builder.Services.AddSignalRConfiguration(builder.Configuration);
builder.Services.AddCustomAuthentication(builder.Configuration);
builder.Services.AddCustomAuthorization();
builder.Services.AddHostedService<ApplyMigrationsJob>();
builder.Services.AddHostedService<NotifyUserToMessageJob>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseCors();
app.UseRouting();
app.UseFileServer();
app.UseAuthentication();
app.UseAuthorization();
app.UseAzureSignalR(endpoints => endpoints.MapHub<NotificationHub>("/notifications"));
app.MapControllers();

app.Run();
