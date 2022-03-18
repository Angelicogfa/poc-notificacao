using NotificacaoAPI.Configurations;
using NotificacaoAPI.Hub;
using NotificacaoAPI.Jobs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDatabaseConfigurations(builder.Configuration);
builder.Services.AddServiceBusConfiguration(builder.Configuration);
builder.Services.AddBus(builder.Configuration);
builder.Services.AddSignalRConfiguration(builder.Configuration);
builder.Services.AddCustomSecurity(builder.Configuration);
builder.Services.AddHostedService<ApplyMigrationsJob>();


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
