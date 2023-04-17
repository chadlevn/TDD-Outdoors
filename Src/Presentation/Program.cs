using Application;
using Infrastructure;
using Infrastructure.Utilities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddApplication();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateAsyncScope();
var serviceProvider = scope.ServiceProvider;
await DatabaseMigrationsUtilities.RunDatabaseMigrationsAsync(serviceProvider, builder.Configuration);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();