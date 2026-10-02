namespace CustomerSupportAgent.Services;

public enum SupportIntent
{
    BusinessHours,
    Pricing,
    BillingIssue,
    HumanRequest,
    Unknown
}

public sealed class IntentClassifier
{
    public SupportIntent Classify(string message)
    {
        var normalized = message.Trim().ToLowerInvariant();

        if (ContainsAny(normalized, "charged", "charge", "refund", "billing", "payment"))
            return SupportIntent.BillingIssue;

        if (ContainsAny(normalized, "human", "person", "representative", "agent"))
            return SupportIntent.HumanRequest;

        if (ContainsAny(normalized, "open", "close", "hours", "schedule"))
            return SupportIntent.BusinessHours;

        if (ContainsAny(normalized, "price", "pricing", "cost", "how much"))
            return SupportIntent.Pricing;

        return SupportIntent.Unknown;
    }

    private static bool ContainsAny(string message, params string[] terms) =>
        terms.Any(message.Contains);
}
