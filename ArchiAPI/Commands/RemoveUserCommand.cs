using ArchiAPI.Models;
using ArchiAPI.Services;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace ArchiAPI.Commands
{
    public sealed class RemoveUserCommand
    {
        private readonly Repository _repos;
        public RemoveUserCommand(Repository repos)
        {
            _repos = repos;
        }
        public async Task ExecuteAsync(string name)
        {
            _repos.Users.Remove(await _repos.Users.FirstOrDefaultAsync(ent => ent.Login == name));
            await _repos.SaveChangesAsync();
        }
    }
}
