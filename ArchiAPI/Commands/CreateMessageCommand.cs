using ArchiAPI.Services;
using Microsoft.EntityFrameworkCore;
using ArchiAPI.Models;
using System.Threading.Tasks;
using System;
namespace ArchiAPI.Commands
{
    public sealed class CreateMessageCommand
    {
        private readonly Repository _repos;
        public CreateMessageCommand(Repository repos)
        {
            _repos = repos;
        }
        public async Task ExecuteAsync(string userName,MessageDTO dto)
        {
            await _repos.AddAsync(new Message() { CreatedAt = dto.CreatedAt, Text = dto.Text, User = await _repos.Users.FirstOrDefaultAsync(user => user.Login == userName) });
            await _repos.SaveChangesAsync();
        }
    }
}
