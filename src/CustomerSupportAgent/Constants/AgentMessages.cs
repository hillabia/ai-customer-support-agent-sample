namespace CustomerSupportAgent.Constants;

public static class AgentMessages
{
    public const string EmptyMessage =
        "I need a message before I can help.";

    public const string HumanHandoff =
        "This request should be handled by a team member. I'll pass it along with the context you provided.";

    public const string UnsupportedRequest =
        "I don't have enough approved information to answer that reliably, so I'll send it to a team member.";
}
