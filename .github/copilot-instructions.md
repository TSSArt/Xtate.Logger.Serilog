# Xtate.Logging.Serilog Copilot instructions

## Repository at a glance

Xtate.Logging.Serilog adapts Xtate.Core logging events to Serilog. The production project is `src/Xtate.Logging.Serilog/Xtate.Logging.Serilog.csproj`; tests are in `test/Xtate.Logging.Serilog.Test`.

Read [`.agents/AGENTS.md`](../.agents/AGENTS.md) for architecture and hazards. Apply every matching file in [`.github/instructions`](instructions); those rules are more specific than this guide.

## Working approach

1. Inspect `SerilogLoggingModule`, `SerilogLoggingOptions`, `SerilogLogWriter`, and `DestructuringPolicy` together.
2. Identify whether the task affects configuration, level mapping, exception extraction, enrichment, destructuring, or disposal.
3. Preserve the Xtate.IoC ownership model and Serilog structured-event semantics.
4. Add focused event assertions using a controlled sink.
5. Run the narrowest useful test before solution-wide validation.

## Build and test

```powershell
dotnet restore
dotnet build Xtate.Logging.Serilog.sln
dotnet test Xtate.Logging.Serilog.sln
```

Focused example:

```powershell
dotnet test test/Xtate.Logging.Serilog.Test/Xtate.Logging.Serilog.Test.csproj -f net10.0
```

The library targets `net11.0`, `net10.0`, `net9.0`, `net8.0`, `netstandard2.0`, and `net462`. Tests target modern frameworks plus optional `net462`.

## Shared coding rules

- Follow `.editorconfig`; C# uses tabs, nullable annotations, analyzers, and preview language features.
- Match the AGPL header and current style in adjacent source files.
- Use Xtate.IoC modules and options, not Microsoft DI abstractions.
- Preserve `ValueTask`-based `ILogProvider` behavior.
- Keep package versions in `Directory.Packages.props`.
- Treat `Directory.Build.props` and `Global.Packages.props` as generated.
- Ignore `bin`, `obj`, `TestResults`, and IDE metadata.

## Architecture guardrails

- `SerilogLogWriter` is container-shared and owns the logger it creates.
- Preserve all Xtate-to-Serilog level mappings, including the current trace mapping.
- The first logging parameter named `Exception` is passed as the Serilog exception; other values remain structured properties.
- Keep source-type context and the namespace/name property-key convention.
- Extend `SerilogLoggingOptions` through normal Serilog configuration APIs.
- Do not require a network sink or local service in automated tests.

## Tests and documentation

- Use MSTest and a deterministic sink or event collector.
- Assert level, message, exception, source context, structured properties, and disposal where relevant.
- Update README or repository guidance when registration, configuration, supported targets, or commands change.

## Before finishing

Confirm structured-event fidelity, ownership/disposal, focused tests, generated-file safety, compatibility targets, and task-scoped changes.
