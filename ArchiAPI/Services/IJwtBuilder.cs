using ArchiAPI.Models;
namespace ArchiAPI.Services
{
    public interface IJwtBuilder
    {
        string Build(string login,string iconUri, System.TimeSpan timeout);
    }
}
