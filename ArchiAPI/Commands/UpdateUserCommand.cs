using ArchiAPI.Services;
using Microsoft.EntityFrameworkCore;
using ArchiAPI.Models;
using System.Linq;
using System.Threading.Tasks;
namespace ArchiAPI.Commands
{
    public sealed class UpdateUserCommand
    {
        private readonly Repository _repos;
        public UpdateUserCommand(Repository repos)
        {
            _repos = repos;
        }
        public async Task ExecuteAsync(UserProfile profile,string iconUri)
        {
            User real = await _repos.Users.FirstOrDefaultAsync(ent => ent.Login == profile.Login);
            real.IconURI = iconUri;
            real.Description = profile.Description;
            await _repos.SaveChangesAsync();
        }
    }
}
