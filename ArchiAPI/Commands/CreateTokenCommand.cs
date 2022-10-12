using ArchiAPI.Services;
using Microsoft.EntityFrameworkCore;
using ArchiAPI.Models;
using System.Threading.Tasks;
using System;
namespace ArchiAPI.Commands
{
    public sealed class CreateTokenCommand
    {
        private readonly Repository _repos;
        public CreateTokenCommand(Repository repos)
        {
            _repos = repos;
        }
        public async Task ExecuteAsync(string login,string refresh)
        {
            RefreshToken token = new RefreshToken() { IsAlive = true, Value = refresh, User = await _repos.Users.FirstOrDefaultAsync(ent => ent.Login == login) };
            await _repos.RefreshTokens.AddAsync(token);
            await _repos.SaveChangesAsync();
        }
    }
}
