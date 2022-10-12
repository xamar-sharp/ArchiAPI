using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ArchiAPI.Models;
using System;
namespace ArchiAPI.Services
{
    public sealed class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(ent => ent.Id);
            builder.ToTable("Messages");
            builder.HasOne(ent => ent.User).WithMany(ent => ent.Messages).OnDelete(DeleteBehavior.Cascade);
            builder.Property(ent => ent.Text).IsRequired().HasMaxLength(5000);
            builder.Property(ent => ent.CreatedAt).HasDefaultValueSql("'GETUTCDATE()'");
            builder.Property(ent => ent.Id).ValueGeneratedOnAdd();
        }
    }
}
