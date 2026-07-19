// Copyright © 2019-2026 Sergii Artemenko
// 
// This file is part of the Xtate project. <https://xtate.net/>
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published
// by the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using Serilog.Core;
using Serilog.Events;
using Xtate.IoC.Options;
using Xtate.Logging.Provider;

namespace Xtate.Logging.Serilog.Services;

[InstantiatedByIoC]
public class SerilogLogWriter(IOptions<SerilogLoggingOptions> options) : ILogProvider, IDisposable
{
    private readonly Logger _logger = options.Value.CreateLogger();

#region Interface IDisposable

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

#endregion

#region Interface ILogProvider

    public bool IsEnabled(Type source, Level level) => _logger.IsEnabled(GetLogEventLevel(level));

    public ValueTask Write(Type source,
                           Level level,
                           int eventId,
                           string? message,
                           IEnumerable<LoggingParameter>? parameters)
    {
        List<LoggingParameter>? prms = null;
        Exception? exception = null;

        if (parameters is not null)
        {
            foreach (var prm in parameters)
            {
                if (exception is null && prm is { Name: @"Exception", Value: Exception ex })
                {
                    exception = ex;
                }
                else
                {
                    prms ??= [];
                    prms.Add(prm);
                }
            }
        }

        var logger = _logger.ForContext(source);

        if (prms is not null)
        {
            logger = logger.ForContext(new ParametersLogEventEnricher(prms));
        }

        logger.Write(GetLogEventLevel(level), exception, message ?? string.Empty);

        return default;
    }

#endregion

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _logger.Dispose();
        }
    }

    private static LogEventLevel GetLogEventLevel(Level level) =>
        level switch
        {
            Level.Info    => LogEventLevel.Information,
            Level.Warning => LogEventLevel.Warning,
            Level.Error   => LogEventLevel.Error,
            Level.Debug   => LogEventLevel.Debug,
            Level.Trace   => LogEventLevel.Debug,
            Level.Verbose => LogEventLevel.Verbose,
            _             => throw new InvalidOperationException()
        };

    private class ParametersLogEventEnricher(IEnumerable<LoggingParameter> parameters) : ILogEventEnricher
    {
    #region Interface ILogEventEnricher

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            foreach (var parameter in parameters)
            {
                var name = string.IsNullOrEmpty(parameter.Namespace) ? parameter.Name : parameter.Namespace + @"_" + parameter.Name;

                logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty(name, parameter.Value, destructureObjects: true));
            }
        }

    #endregion
    }
}