using System.IO;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;
namespace ArchiAPI.Services
{
    public sealed class LoggerWrapper:ILoggerWrapper
    {
        private readonly ILoggerProvider _provider;
        public LoggerWrapper(ILoggerProvider provider)
        {
            _provider = provider;
        }
        public ILogger Unwrap()
        {
            return LoggerFactory.Create(builder => builder.AddProvider(_provider)).CreateLogger("LogController");
        }
    }
}
