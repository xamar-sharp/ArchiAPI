using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ArchiAPI.Services;
using ArchiAPI.Models;
namespace ArchiAPI.Commands
{
    public sealed class UpdateTokenCommand
    {
        private readonly Repository _repos;
        public UpdateTokenCommand(Repository repository)
        {
            _repos = repository;
        }
        public async Task ExecuteAsync(string token)
        {
            var refresh = await _repos.RefreshTokens.FirstOrDefaultAsync(ent => ent.Value == token);
            refresh.IsAlive = false;
            await _repos.SaveChangesAsync();
        }
    }
}
