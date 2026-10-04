# AI Customer Support Agent — Public Code Sample

A small .NET 10 API that demonstrates the core decision flow I use when designing AI-assisted customer support systems.

> This repository is a simplified public implementation based on patterns from a private customer-service project. Client-specific data, credentials, messaging integrations, and production infrastructure have intentionally been removed.

## What it demonstrates

The sample keeps the responsibilities deliberately small:

1. Receive a customer message.
2. Classify the intent.
3. Answer safe, known business-information questions from a controlled knowledge source.
4. Escalate sensitive or unsupported requests to a human.
5. Return a structured response that another channel, such as web chat or WhatsApp, could consume.

The main idea is simple: an agent should not guess when a business rule or human decision is required.

## Example

Request:

```json
{
  "message": "What time do you close?"
}
```

Response:

```json
{
  "intent": "BusinessHours",
  "message": "We are open Monday through Friday from 9:00 AM to 7:00 PM.",
  "requiresHuman": false
}
```

A message such as `I was charged twice.` is classified as a billing issue and escalated instead of attempting to resolve a sensitive account problem automatically.

## Architecture and design

The project remains intentionally small, but responsibilities are separated so individual parts can evolve without changing the core decision flow.

```text
HTTP request
     ↓
SupportAgent
     ↓
IIntentClassifier ──→ RuleBasedIntentClassifier
     ↓
IKnowledgeService ──→ InMemoryKnowledgeService
     ↓
Answer or human handoff
```

The application uses dependency injection and small abstractions at boundaries that are likely to change. For example, the rule-based classifier can later be replaced by an LLM-backed classifier, and the in-memory knowledge source can be replaced by a database, API, or retrieval system without rewriting the agent orchestration.

This keeps the sample aligned with SOLID principles without introducing additional projects or patterns that its current size does not require.

## Project structure

```text
src/CustomerSupportAgent/
├── Agent/
│   ├── ISupportAgent.cs
│   └── SupportAgent.cs
├── Classification/
│   ├── IIntentClassifier.cs
│   └── RuleBasedIntentClassifier.cs
├── Constants/
│   ├── AgentMessages.cs
│   ├── IntentTerms.cs
│   └── KnowledgeMessages.cs
├── Knowledge/
│   ├── IKnowledgeService.cs
│   └── InMemoryKnowledgeService.cs
├── Models/
│   ├── AgentResponse.cs
│   ├── SupportIntent.cs
│   └── SupportRequest.cs
└── Program.cs

tests/CustomerSupportAgent.Tests/
├── RuleBasedIntentClassifierTests.cs
└── SupportAgentTests.cs
```

## Run locally

Requirements: .NET 10 SDK.

```bash
dotnet restore
dotnet run --project src/CustomerSupportAgent
```

Then send a request to `POST /support`. The exact local port may differ; use the URL printed by `dotnet run`.

Example with curl:

```bash
curl -X POST "<LOCAL_URL>/support" \
  -H "Content-Type: application/json" \
  -d '{"message":"What time do you close?"}'
```

Try changing the message to `I was charged twice.` or `Can I speak with a person?` to see the human-handoff behavior.

## Tests

```bash
dotnet test
```

The tests focus on behavior rather than implementation details: known business questions can be answered, sensitive issues are escalated, explicit requests for a human are respected, and unknown requests are not guessed.

Intent classification is also tested independently so the classifier can evolve without coupling its tests to the agent orchestration.

## Why the classifier is rule-based

For this public sample, I intentionally kept intent classification deterministic so the agent's decision flow is easy to review.

In a production implementation, I could replace or complement the classifier with an LLM using structured outputs while keeping business rules, tool execution, and escalation deterministic where reliability matters.

The `IIntentClassifier` abstraction makes that change possible without requiring `SupportAgent` to know how classification is implemented.

## How to extend the sample

The current implementations are deliberately simple extension points.

- Replace `RuleBasedIntentClassifier` with an LLM-backed implementation of `IIntentClassifier`.
- Replace `InMemoryKnowledgeService` with a database, API, or retrieval-backed implementation of `IKnowledgeService`.
- Add new intents to `SupportIntent` and define how the agent should handle them.
- Connect the structured response to a web chat, WhatsApp integration, or another messaging channel.
- Add tools for operations that require deterministic business logic.

The key constraint should remain the same: changing an external implementation should not require rewriting the core decision flow.

## How I would extend it in production

For a production customer-support agent, I would keep the same decision layer while adding:

- LLM-based natural-language understanding.
- Retrieval from approved company knowledge.
- Explicit tools for operations such as order lookup or appointment availability.
- Authentication and authorization.
- Logging and observability.
- Evaluation datasets.
- Rate limiting.
- Real human-handoff integration.

Business information such as schedules, pricing, and approved responses would also move out of compiled constants and into an appropriate configuration or knowledge source.

The goal of this repository is not to simulate an entire production platform. It is to make the core agent architecture, engineering decisions, and safety boundaries easy to understand and review.
