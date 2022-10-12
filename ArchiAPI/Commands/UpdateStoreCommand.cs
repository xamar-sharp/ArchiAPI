using ArchiAPI.Services;
using ArchiAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
namespace ArchiAPI.Commands
{
    public sealed class UpdateStoreCommand
    {
        private readonly Repository _repos;
        public UpdateStoreCommand(Repository repos)
        {
            _repos = repos;
        }
        public async Task ExecuteAsync(StoreDTO storeDto,string iconUri)
        {
            Store store = await _repos.Stores.FirstOrDefaultAsync(ent => ent.Title == storeDto.Title);
            store.Description = storeDto.Description;
            store.Code = storeDto.Code;
            store.IconURI = iconUri;
            store.Type = storeDto.Type;
            await _repos.SaveChangesAsync();
        }
    }
}
