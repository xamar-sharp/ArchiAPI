using Microsoft.EntityFrameworkCore;
using System.Linq;
using ArchiAPI.Services;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using ArchiAPI.Models;
namespace ArchiAPI.Queries
{
    public sealed class GenericUserQuery
    {
        private readonly Repository _repos;
        public GenericUserQuery(Repository repos)
        {
            _repos = repos;
        }
        public enum UserQueryIntent
        {
            UserByLogin,UserByRefresh
        }
        public async Task<User> ExecuteAsync(string param,UserQueryIntent intent)
        {
            switch (intent)
            {
                case UserQueryIntent.UserByRefresh:
                    return (await _repos.RefreshTokens.Include(ent=>ent.User).FirstOrDefaultAsync(ent => ent.Value == param && ent.IsAlive))?.User;
                default:
                    return await _repos.Users.FirstOrDefaultAsync(ent => ent.Login == param);
            }
        }
    }
}
