using Microsoft.Extensions.Logging;
namespace ArchiAPI.Services
{
    public interface ILoggerWrapper
    {
        ILogger Unwrap();
    }
}
