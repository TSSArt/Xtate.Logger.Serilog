# Xtate.Logging.Serilog

[![NuGet](https://img.shields.io/nuget/v/Xtate.Logging.Serilog.svg)](https://www.nuget.org/packages/Xtate.Logging.Serilog)
[![CodeQL](https://github.com/TSSArt/Xtate.Logging.Serilog/actions/workflows/codeql.yml/badge.svg)](https://github.com/TSSArt/Xtate.Logging.Serilog/actions/workflows/codeql.yml)
[![License: AGPL-3.0-or-later](https://img.shields.io/badge/license-AGPL--3.0--or--later-blue.svg)](LICENSE)

Xtate.Logging.Serilog connects the logging abstractions in [Xtate.Core](https://www.nuget.org/packages/Xtate.Core/) to [Serilog](https://serilog.net/). It maps Xtate log levels, exceptions, sources, and structured parameters to Serilog events.

## Features

- Registers a Serilog-backed `ILogProvider` through `Xtate.IoC`.
- Maps Xtate log levels to Serilog event levels.
- Preserves structured logging parameters and exception values.
- Adds the originating Xtate type as Serilog context.
- Supports the standard Serilog configuration and sink ecosystem.
- Disposes the created Serilog logger with the root container.

## Installation

```shell
dotnet add package Xtate.Logging.Serilog
```

Add any Serilog sinks required by the application separately.

## Usage

Configure `SerilogLoggingOptions`, then register the logging module:

```csharp
using Serilog;
using Xtate.IoC;
using Xtate.IoC.Options.DependencyInjection;
using Xtate.Logging.Serilog;
using Xtate.Logging.Serilog.DependencyInjection;

var services = new ServiceCollection();

services.Configure<SerilogLoggingOptions>(options => options
    .MinimumLevel.Information()
    .WriteTo.Console());

services.AddModule<SerilogLoggingModule>();
```

Register the other Xtate modules required by the application in the same service collection. The module adds a container-shared Serilog log provider to the Xtate logging pipeline.

## Supported frameworks

The library targets .NET 11, .NET 10, .NET 9, .NET 8, .NET Standard 2.0, and .NET Framework 4.6.2.

## Building from source

```shell
git clone https://github.com/TSSArt/Xtate.Logging.Serilog.git
cd Xtate.Logging.Serilog
dotnet restore
dotnet build Xtate.Logging.Serilog.sln
dotnet test Xtate.Logging.Serilog.sln
```

## Repository layout

| Path | Description |
| --- | --- |
| `src/Xtate.Logging.Serilog` | Module, options, log writer, and destructuring support |
| `test/Xtate.Logging.Serilog.Test` | MSTest integration coverage |
| `.github/instructions` | Path-specific guidance for coding agents |
| `.github/workflows` | Security analysis and publishing workflows |
| `.agents` | Repository guide for maintainers and coding agents |

## Contributing

Contributions are welcome. Read the [repository guide](.agents/AGENTS.md), follow `.editorconfig`, and add tests for level mapping, structured values, exceptions, configuration, or disposal changes.

Use [GitHub Issues](https://github.com/TSSArt/Xtate.Logging.Serilog/issues) for bug reports and feature requests.

## License

Xtate.Logging.Serilog is licensed under the [GNU Affero General Public License v3.0 or later](LICENSE).
