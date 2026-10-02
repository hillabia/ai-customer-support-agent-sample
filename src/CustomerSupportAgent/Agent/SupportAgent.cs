using CustomerSupportAgent.Models;
using CustomerSupportAgent.Services;

namespace CustomerSupportAgent.Agent;

public sealed class SupportAgent(
    IntentClassifier classifier,
    KnowledgeService knowledge)
{
    public AgentResponse Handle(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return Escalate(
                SupportIntent.Unknown,
                "I need a message before I can help.");
        }

        var intent = classifier.Classify(message);

        if (intent is SupportIntent.BillingIssue or SupportIntent.HumanRequest)
        {
            return Escalate(
                intent,
                "This request should be handled by a team member. I'll pass it along with the context you provided.");
        }

        if (knowledge.TryGetAnswer(intent, out var answer))
            return new AgentResponse(intent.ToString(), answer, false);

        return Escalate(
            intent,
            "I don't have enough approved information to answer that reliably, so I'll send it to a team member.");
    }

    private static AgentResponse Escalate(SupportIntent intent, string message) =>
        new(intent.ToString(), message, true);
}
