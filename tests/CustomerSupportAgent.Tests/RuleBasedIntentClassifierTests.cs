using CustomerSupportAgent.Classification;
using CustomerSupportAgent.Models;
using Xunit;

namespace CustomerSupportAgent.Tests;

public sealed class RuleBasedIntentClassifierTests
{
    private readonly RuleBasedIntentClassifier _classifier = new();

    [Theory]
    [InlineData("What time do you close?", SupportIntent.BusinessHours)]
    [InlineData("How much does it cost?", SupportIntent.Pricing)]
    [InlineData("I was charged twice.", SupportIntent.BillingIssue)]
    [InlineData("Can I speak with a person?", SupportIntent.HumanRequest)]
    [InlineData("Can you change the color of my order?", SupportIntent.Unknown)]
    public void Classify_ReturnsExpectedIntent(string message, SupportIntent expected)
    {
        var result = _classifier.Classify(message);

        Assert.Equal(expected, result);
    }
}
