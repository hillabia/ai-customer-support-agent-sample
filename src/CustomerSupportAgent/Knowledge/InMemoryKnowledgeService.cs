using CustomerSupportAgent.Constants;
using CustomerSupportAgent.Models;

namespace CustomerSupportAgent.Knowledge;

public sealed class InMemoryKnowledgeService : IKnowledgeService
{
    private static readonly IReadOnlyDictionary<SupportIntent, string> Answers =
        new Dictionary<SupportIntent, string>
        {
            [SupportIntent.BusinessHours] = KnowledgeMessages.BusinessHours,
            [SupportIntent.Pricing] = KnowledgeMessages.Pricing
        };

    public bool TryGetAnswer(SupportIntent intent, out string answer) =>
        Answers.TryGetValue(intent, out answer!);
}
