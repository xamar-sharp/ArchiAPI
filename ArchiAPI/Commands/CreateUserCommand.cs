using Microsoft.EntityFrameworkCore;
using ArchiAPI.Services;
using System.Threading.Tasks;
using ArchiAPI.Models;
namespace ArchiAPI.Commands
{
    public sealed class CreateUserCommand
    {
        private readonly Repository _repos;
        public CreateUserCommand(Repository repos)
        {
            _repos = repos;
        }
        public async Task ExecuteAsync(User user)
        {
            await _repos.Users.AddAsync(user);
            await _repos.SaveChangesAsync();
        }
    }
}
