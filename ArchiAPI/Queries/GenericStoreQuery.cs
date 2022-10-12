using Microsoft.EntityFrameworkCore;
using System.Linq;
using ArchiAPI.Services;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using ArchiAPI.Models;
namespace ArchiAPI.Queries
{
    public sealed class GenericStoreQuery
    {
        private readonly Repository _repos;
        public GenericStoreQuery(Repository repos)
        {
            _repos = repos;
        }
        public async Task<Store> GetStore(string title) => await _repos.Stores.FirstOrDefaultAsync(store => store.Title == title);
        public async Task<IList<Store>> GetStores(int top, int offset) => await _repos.Stores.Skip(offset).Take(top).ToListAsync();
    }
}
