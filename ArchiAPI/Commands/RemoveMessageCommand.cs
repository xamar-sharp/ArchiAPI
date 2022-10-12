using ArchiAPI.Models;
using ArchiAPI.Services;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
namespace ArchiAPI.Commands
{
    public sealed class RemoveMessageCommand
    {
        private readonly Repository _repos;
        public RemoveMessageCommand(Repository repos)
        {
            _repos = repos;
        }
        public async Task ExecuteAsync(string messageText,string userName,DateTime createdAt)
        {
            var msg = await _repos.Messages.Include(msg => msg.User).OrderByDescending(ent=>ent.CreatedAt).FirstOrDefaultAsync(ent => ent.Text == messageText & ent.User.Login == userName & ent.CreatedAt == createdAt); ;
            _repos.Messages.Remove(msg);
            await _repos.SaveChangesAsync();
        }
    }
}
