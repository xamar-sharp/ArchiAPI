using ArchiAPI.Services;
using Microsoft.EntityFrameworkCore;
using ArchiAPI.Models;
using System.Threading.Tasks;
using System;
namespace ArchiAPI.Commands
{
    public sealed class CreateStoreCommand
    {
        private readonly Repository _repos;
        public CreateStoreCommand(Repository repos)
        {
            _repos = repos;
        }
        public async Task ExecuteAsync(string userName,StoreDTO dto,string iconUri)
        {
            Store store = new Store() { Code = dto.Code, Description = dto.Description, CreatedAt = dto.CreatedAt, IconURI = iconUri, Title = dto.Title, Type = dto.Type, User = await _repos.Users.FirstOrDefaultAsync(ent => ent.Login == userName) };
            await _repos.Stores.AddAsync(store);
            await _repos.SaveChangesAsync();
        }
    }
}
