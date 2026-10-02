namespace CustomerSupportAgent.Services;

public sealed class KnowledgeService
{
    private static readonly IReadOnlyDictionary<SupportIntent, string> Answers =
        new Dictionary<SupportIntent, string>
        {
            [SupportIntent.BusinessHours] =
                "We are open Monday through Friday from 9:00 AM to 7:00 PM.",
            [SupportIntent.Pricing] =
                "Pricing depends on the service and quantity. I can share general information, but a custom quote should be confirmed by a team member."
        };

    public bool TryGetAnswer(SupportIntent intent, out string answer) =>
        Answers.TryGetValue(intent, out answer!);
}
