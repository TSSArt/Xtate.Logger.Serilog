---
applyTo: "src/Xtate.Logging.Serilog/**/*.cs"
---

# Serilog integration instructions

## Registration and ownership

- Register through `SerilogLoggingModule` and Xtate.IoC's options infrastructure.
- Keep `SerilogLogWriter` shared within the root container and responsible for disposing only its created logger.
- Keep `LoggingModule` as the module dependency and publish the writer as `ILogProvider`.

## Event mapping

- Preserve the explicit Xtate-to-Serilog level mapping.
- Pass the first `Exception` parameter as the Serilog exception argument.
- Preserve all other values as structured properties and retain source-type context.
- Keep namespaced property keys in the existing `Namespace_Name` form.
- Preserve Xtate data-model destructuring rather than flattening values to strings.

## Verification

- Capture emitted events with a deterministic sink and assert structured fields.
- Cover configuration and disposal changes without relying on an external sink.
