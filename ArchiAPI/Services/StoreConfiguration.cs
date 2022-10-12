using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ArchiAPI.Models;
using System;
namespace ArchiAPI.Services
{
    public sealed class StoreConfiguration:IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.HasKey(ent => ent.Id);
            builder.ToTable("Stores");
            builder.HasAlternateKey(ent => ent.Title);
            builder.HasIndex(ent => new { ent.Title }).IsClustered(false);
            builder.HasOne(ent => ent.User).WithMany(ent => ent.Stores).OnDelete(DeleteBehavior.Cascade);
            builder.Property(ent => ent.Code).IsRequired().HasMaxLength(8 * 1024 * 1024);
            builder.Property(ent => ent.CreatedAt).HasDefaultValueSql("'GETUTCDATE()'");
            builder.Property(ent => ent.Description).IsRequired().HasMaxLength(1024 * 1024);
            builder.Property(ent => ent.Id).ValueGeneratedOnAdd();
            builder.Property(ent => ent.IconURI).HasDefaultValue("https://cdn-icons-png.flaticon.com/512/3177/3177440.png");
            builder.Property(ent => ent.Type).IsRequired();
        }
    }
}
