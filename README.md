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

## Project structure

```text
src/CustomerSupportAgent/
├── Agent/
│   └── SupportAgent.cs
├── Models/
│   └── AgentResponse.cs
├── Services/
│   ├── IntentClassifier.cs
│   └── KnowledgeService.cs
└── Program.cs

tests/CustomerSupportAgent.Tests/
└── SupportAgentTests.cs
```

## Run locally

Requirements: .NET 10 SDK.

```bash
dotnet restore
dotnet run --project src/CustomerSupportAgent
```

Then send a request to `POST /support`. The exact local port may differ; use the URL printed by `dotnet run`.

## Tests

```bash
dotnet test
```

The tests focus on behavior rather than implementation details: known business questions can be answered, sensitive issues are escalated, explicit requests for a human are respected, and unknown requests are not guessed.

## Why the classifier is rule-based

For this public sample, I intentionally kept intent classification deterministic so the agent's decision flow is easy to review.

In a production implementation, I could replace or complement the classifier with an LLM using structured outputs while keeping business rules, tool execution, and escalation deterministic where reliability matters.

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

The goal of this repository is not to simulate an entire production platform. It is to make the core agent architecture and its boundaries easy to understand and review.
