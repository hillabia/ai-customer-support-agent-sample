using CustomerSupportAgent.Agent;
using CustomerSupportAgent.Classification;
using CustomerSupportAgent.Knowledge;
using CustomerSupportAgent.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IIntentClassifier, RuleBasedIntentClassifier>();
builder.Services.AddSingleton<IKnowledgeService, InMemoryKnowledgeService>();
builder.Services.AddSingleton<ISupportAgent, SupportAgent>();

var app = builder.Build();

app.MapGet("/", () => Results.Ok(new
{
    service = "AI Customer Support Agent Sample",
    endpoint = "POST /support"
}));

app.MapPost("/support", (SupportRequest request, ISupportAgent agent) =>
{
    var response = agent.Handle(request.Message ?? string.Empty);
    return Results.Ok(response);
});

app.Run();

public partial class Program;
