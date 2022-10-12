using System;
using Microsoft.Extensions.Logging;
namespace ArchiAPI.Services
{
    public sealed class TextFileLoggerProvider:ILoggerProvider
    {
        public string Path { get; }
        public TextFileLoggerProvider(string path)
        {
            Path = path;
        }
        public void Dispose()
        {

        }
        public ILogger CreateLogger(string category)
        {
            return new TextFileLogger(Path);
        }
    }
}
