using ASBPubSubService.Services;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string serviceBusConnectionString = builder.Configuration["AzureServiceBus:ConnectionString"]!;
builder.Services.AddSingleton(new PublishingService(serviceBusConnectionString));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/publish", async (string message, PublishingService service) =>
{
    await service.PublishAsync(message);
    return Results.Ok("Azure Service Bus published");
});
app.UseHttpsRedirection();
app.Run();

