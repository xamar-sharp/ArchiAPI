using Microsoft.Extensions.Logging;
namespace ArchiAPI.Services
{
    public sealed class LoggingWrapper : ILoggerWrapper
    {
        private readonly ILoggerProvider _provider;
        public LoggingWrapper(ILoggerProvider provider)
        {
            _provider = provider;
        }
        public ILogger Unwrap()
        {
            return LoggerFactory.Create(builder => builder.AddProvider(_provider)).CreateLogger("text");
        }
    }
}
