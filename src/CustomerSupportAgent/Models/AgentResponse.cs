namespace CustomerSupportAgent.Models;

public sealed record AgentResponse(
    string Intent,
    string Message,
    bool RequiresHuman);
