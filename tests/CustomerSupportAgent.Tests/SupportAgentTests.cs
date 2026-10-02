using CustomerSupportAgent.Agent;
using CustomerSupportAgent.Services;
using Xunit;

namespace CustomerSupportAgent.Tests;

public sealed class SupportAgentTests
{
    private readonly SupportAgent _agent =
        new(new IntentClassifier(), new KnowledgeService());

    [Fact]
    public void KnownBusinessQuestion_IsAnsweredWithoutEscalation()
    {
        var result = _agent.Handle("What time do you close?");

        Assert.Equal("BusinessHours", result.Intent);
        Assert.False(result.RequiresHuman);
        Assert.Contains("7:00 PM", result.Message);
    }

    [Fact]
    public void BillingIssue_IsEscalated()
    {
        var result = _agent.Handle("I was charged twice.");

        Assert.Equal("BillingIssue", result.Intent);
        Assert.True(result.RequiresHuman);
    }

    [Fact]
    public void ExplicitHumanRequest_IsEscalated()
    {
        var result = _agent.Handle("Can I speak with a person?");

        Assert.Equal("HumanRequest", result.Intent);
        Assert.True(result.RequiresHuman);
    }

    [Fact]
    public void UnknownQuestion_IsNotGuessed()
    {
        var result = _agent.Handle("Can you change the color of my order?");

        Assert.Equal("Unknown", result.Intent);
        Assert.True(result.RequiresHuman);
    }
}
