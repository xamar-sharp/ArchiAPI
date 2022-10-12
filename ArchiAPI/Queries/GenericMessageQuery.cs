using Microsoft.EntityFrameworkCore;
using System.Linq;
using ArchiAPI.Services;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using ArchiAPI.Models;
namespace ArchiAPI.Queries
{
    public sealed class GenericMessageQuery
    {
        private readonly Repository _repos;
        public GenericMessageQuery(Repository repos)
        {
            _repos = repos;
        }
        public async Task<IList<MessageDTO>> GetMessages(int last) => await _repos.Messages.OrderByDescending(msg => msg.CreatedAt).Take(last).Select(ent=>new MessageDTO() { UserIconUri=ent.User.IconURI,UserName=ent.User.Login,CreatedAt=ent.CreatedAt,Text=ent.Text}).ToListAsync();
    }
}
