using Microsoft.EntityFrameworkCore;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Infrastructure;

internal class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    internal DbSet<Post> Posts { get; set; }

    internal DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.HasOne(p => p.Author)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.AuthorId);
            entity.Property(p => p.Content)
                .IsRequired()
                .HasMaxLength(1000);
            entity.Property(p => p.AuthorId)
                .IsRequired();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.GivenName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(u => u.FamilyName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(u => u.Image)
                .HasMaxLength(1000);
        });
    }
}