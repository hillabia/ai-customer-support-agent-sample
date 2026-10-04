using CustomerSupportAgent.Models;

namespace CustomerSupportAgent.Agent;

public interface ISupportAgent
{
    AgentResponse Handle(string message);
}
