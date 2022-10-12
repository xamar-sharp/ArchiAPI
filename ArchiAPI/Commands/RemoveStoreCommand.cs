using ArchiAPI.Models;
using ArchiAPI.Services;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
namespace ArchiAPI.Commands
{
    public sealed class RemoveStoreCommand
    {
        private readonly Repository _repos;
        public RemoveStoreCommand(Repository repos)
        {
            _repos = repos;
        }
        public async Task ExecuteAsync(string storeTitle)
        {
            var store = await _repos.Stores.FirstOrDefaultAsync(ent => ent.Title == storeTitle);
            _repos.Stores.Remove(store);
            await _repos.SaveChangesAsync();
        }
    }
}
