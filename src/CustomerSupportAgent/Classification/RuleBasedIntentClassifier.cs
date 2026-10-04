using CustomerSupportAgent.Constants;
using CustomerSupportAgent.Models;

namespace CustomerSupportAgent.Classification;

public sealed class RuleBasedIntentClassifier : IIntentClassifier
{
    public SupportIntent Classify(string message)
    {
        var normalized = message.Trim().ToLowerInvariant();

        if (ContainsAny(normalized, IntentTerms.Billing))
            return SupportIntent.BillingIssue;

        if (ContainsAny(normalized, IntentTerms.HumanRequest))
            return SupportIntent.HumanRequest;

        if (ContainsAny(normalized, IntentTerms.BusinessHours))
            return SupportIntent.BusinessHours;

        if (ContainsAny(normalized, IntentTerms.Pricing))
            return SupportIntent.Pricing;

        return SupportIntent.Unknown;
    }

    private static bool ContainsAny(string message, IEnumerable<string> terms) =>
        terms.Any(message.Contains);
}
