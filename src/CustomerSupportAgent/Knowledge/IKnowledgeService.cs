using CustomerSupportAgent.Models;

namespace CustomerSupportAgent.Knowledge;

public interface IKnowledgeService
{
    bool TryGetAnswer(SupportIntent intent, out string answer);
}
