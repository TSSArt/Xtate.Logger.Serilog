---
applyTo: "src/**/*.cs"
---

# C# source instructions

## Style and compatibility

- Follow `.editorconfig`: tabs, nullable annotations, analyzer rules, using order, and existing naming conventions.
- Match the AGPL header and current style in adjacent source files.
- Preserve every target framework and avoid modern-only APIs without a compatible implementation.
- Preserve the `ValueTask`-based Xtate logging contract.

## Architecture

- Use Xtate.IoC modules and options rather than Microsoft DI abstractions.
- Preserve container ownership of the Serilog writer and its created logger.
- Preserve structured parameters, exception extraction, source context, level mapping, and destructuring behavior.
- Extend configuration through normal Serilog APIs.

## Generated and dependency files

- Keep dependency versions in `Directory.Packages.props` and omit versions from `PackageReference` items.
- Do not edit generated build-property files or build output.

## Verification

- Assert emitted event data with a deterministic sink.
- Build a modern target and relevant compatibility targets.
