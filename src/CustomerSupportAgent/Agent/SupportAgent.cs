using CustomerSupportAgent.Classification;
using CustomerSupportAgent.Constants;
using CustomerSupportAgent.Knowledge;
using CustomerSupportAgent.Models;

namespace CustomerSupportAgent.Agent;

public sealed class SupportAgent(
    IIntentClassifier classifier,
    IKnowledgeService knowledge) : ISupportAgent
{
    public AgentResponse Handle(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            return Escalate(SupportIntent.Unknown, AgentMessages.EmptyMessage);

        var intent = classifier.Classify(message);

        if (RequiresHuman(intent))
            return Escalate(intent, AgentMessages.HumanHandoff);

        if (knowledge.TryGetAnswer(intent, out var answer))
            return new AgentResponse(intent.ToString(), answer, false);

        return Escalate(intent, AgentMessages.UnsupportedRequest);
    }

    private static bool RequiresHuman(SupportIntent intent) =>
        intent is SupportIntent.BillingIssue or SupportIntent.HumanRequest;

    private static AgentResponse Escalate(SupportIntent intent, string message) =>
        new(intent.ToString(), message, true);
}
