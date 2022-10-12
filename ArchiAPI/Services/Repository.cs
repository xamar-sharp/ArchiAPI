using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System;
using ArchiAPI.Models;
namespace ArchiAPI.Services
{
    public sealed class Repository:DbContext
    {
        public DbSet<Store> Stores { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Message> Messages { get; set; }
        public Repository(DbContextOptions<Repository> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.Entity<RefreshToken>().HasOne(token => token.User).WithMany(user => user.RefreshTokens).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<RefreshToken>().Property(ent => ent.IsAlive).HasDefaultValue(true);
        }
    }
}
