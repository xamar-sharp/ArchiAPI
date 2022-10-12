using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ArchiAPI.Models;
using System;
namespace ArchiAPI.Services
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(ent => ent.Id);
            builder.ToTable("Users");
            builder.HasAlternateKey(ent => ent.Login);
            builder.HasIndex(ent => new { ent.Login}).IsClustered(false);
            builder.Property(ent => ent.CreatedAt).HasDefaultValueSql("'GETUTCDATE()'");
            builder.Property(ent => ent.Description).IsRequired().HasMaxLength(1024 * 1024);
            builder.Property(ent => ent.Id).ValueGeneratedOnAdd();
        }
    }
}