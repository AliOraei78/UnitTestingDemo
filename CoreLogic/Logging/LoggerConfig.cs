using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.Seq;

namespace CoreLogic.Logging;

public static class LoggerConfig
{
    private static readonly LoggingLevelSwitch _levelSwitch = new LoggingLevelSwitch();
    public static void Configure(bool enableConsole = true, bool enableFile = true, bool enableSeq = true)
    {
        var configuration = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithThreadId();

        if (enableConsole)
        {
            configuration.WriteTo.Console(
                outputTemplate: "[{Timestamp:HH:mm:ss.fff} {Level:u3} {ThreadId}] {Message:lj}{NewLine}{Exception}"
            );
        }

        if (enableFile)
        {
            configuration.WriteTo.File(
                path: "logs/log-.txt",
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 31,           
                fileSizeLimitBytes: 50 * 1024 * 1024,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {MachineName} {ThreadId} {Message:lj}{NewLine}{Exception}"
            );
        }

        if (enableSeq)
        {
            configuration.WriteTo.Seq(
                serverUrl: "http://localhost:5341",   
                apiKey: null,                         
                restrictedToMinimumLevel: LogEventLevel.Debug,
                controlLevelSwitch: _levelSwitch
            );
        }

        Log.Logger = configuration.CreateLogger();
    }

    public static void CloseAndFlush()
    {
        Log.CloseAndFlush();
    }
}