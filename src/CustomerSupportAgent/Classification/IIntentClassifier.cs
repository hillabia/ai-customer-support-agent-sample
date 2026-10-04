using CustomerSupportAgent.Models;

namespace CustomerSupportAgent.Classification;

public interface IIntentClassifier
{
    SupportIntent Classify(string message);
}
