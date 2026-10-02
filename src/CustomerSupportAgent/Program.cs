using CustomerSupportAgent.Agent;
using CustomerSupportAgent.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IntentClassifier>();
builder.Services.AddSingleton<KnowledgeService>();
builder.Services.AddSingleton<SupportAgent>();

var app = builder.Build();

app.MapGet("/", () => Results.Ok(new
{
    service = "AI Customer Support Agent Sample",
    endpoint = "POST /support"
}));

app.MapPost("/support", (SupportRequest request, SupportAgent agent) =>
{
    var response = agent.Handle(request.Message ?? string.Empty);
    return Results.Ok(response);
});

app.Run();

public sealed record SupportRequest(string? Message);

public partial class Program;
