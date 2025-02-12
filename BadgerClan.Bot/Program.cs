using BadgerClan.Bot.Services;
using BadgerClan.Shared;
using ProtoBuf.Grpc.Server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCodeFirstGrpc();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton<StrategyService>();
var app = builder.Build();


app.MapGrpcService<ChangeService>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/", () => "Hello world!");

// Sneaky line of code
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public class ChangeService(StrategyService service) : IChangeService
{
    public Task ChangeStrategy(ChangeRequest request)
    {
         service.ChangeStrategy(request.StratType);
         return Task.CompletedTask;

    }
}
