using Microsoft.Extensions.Logging;
using System.IO;
using System;
namespace ArchiAPI.Services
{
    public sealed class TextFileLogger : ILogger
    {
        public string Path { get; }
        public TextFileLogger(string path)
        {
            Path = path;
        }
        public bool IsEnabled(LogLevel level)
        {
            return level != LogLevel.Debug;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
        public void Log<TState>(LogLevel level, EventId id, TState message, Exception ex, Func<TState, Exception, string> formatter)
        {
            using (FileStream stream = new FileStream(Path, FileMode.Append, FileAccess.Write, FileShare.None))
            {
                using (TextWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine($"{Environment.NewLine}({level})[{id}]:{message} $ with {ex?.Message}");
                }
            }
        }
    }
}
