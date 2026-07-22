# Xtate.Logging.Serilog repository guide

Use this guide as the first source of repository context. This integration is small; inspect the module, options, writer, destructuring policy, and matching test before changing behavior.

## Project purpose

Xtate.Logging.Serilog adapts Xtate.Core logging to Serilog.

| Path | Purpose |
| --- | --- |
| `src/Xtate.Logging.Serilog/Xtate.Logging.Serilog.csproj` | Multi-targeted library and NuGet package |
| `test/Xtate.Logging.Serilog.Test/Xtate.Logging.Serilog.Test.csproj` | MSTest integration coverage |
| `Xtate.Logging.Serilog.sln` | Repository solution |

The library targets `net11.0`, `net10.0`, `net9.0`, `net8.0`, `netstandard2.0`, and `net462`. Tests target modern frameworks plus `net462` unless `SkipNetFrameworkTests=true`.

## Architecture

- `DependencyInjection/SerilogLoggingModule.cs` adds the core Xtate logging module and registers one container-shared `SerilogLogWriter` as `ILogProvider`.
- `Options/SerilogLoggingOptions.cs` derives from Serilog `LoggerConfiguration` and installs Xtate destructuring behavior.
- `Services/SerilogLogWriter.cs` maps levels, separates exception parameters, enriches structured properties, adds source context, and owns the created logger.
- `Internal/DestructuringPolicy.cs` converts Xtate data-model values into Serilog structures.

The root container owns the writer and logger. Keep level mapping, exception extraction, property naming, destructuring, and disposal behavior explicit and tested.

## Code conventions and hazards

- Follow `.editorconfig`: tabs, nullable annotations, analyzer rules, and existing naming/style.
- Use Xtate.IoC registration and options abstractions rather than Microsoft DI.
- Preserve structured values; do not flatten parameters into rendered strings.
- Keep the parameter named `Exception` mapped to the Serilog exception argument.
- Preserve the source type as Serilog context and the namespace/name property-key convention.
- Dispose only the logger created from `SerilogLoggingOptions`.
- Treat `Directory.Build.props` and `Global.Packages.props` as generated; keep package versions in `Directory.Packages.props`.
- Ignore `bin`, `obj`, `TestResults`, and IDE metadata.

Path-specific rules in `.github/instructions` take precedence for matching files.

## Build and test

```powershell
dotnet restore
dotnet build Xtate.Logging.Serilog.sln
dotnet test Xtate.Logging.Serilog.sln
```

For a focused modern target:

```powershell
dotnet test test/Xtate.Logging.Serilog.Test/Xtate.Logging.Serilog.Test.csproj -f net10.0
```

Tests must not require a running external sink. Prefer an in-memory or controlled sink for assertions.

## Change checklist

1. Identify whether configuration, level mapping, enrichment, destructuring, or disposal changes.
2. Add focused assertions for the emitted Serilog event and exception when relevant.
3. Run the focused test and solution build/test command.
4. Keep generated files and unrelated existing work untouched.
5. Update documentation when registration or configuration usage changes.
