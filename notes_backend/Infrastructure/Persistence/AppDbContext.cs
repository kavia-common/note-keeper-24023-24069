using Microsoft.EntityFrameworkCore;
using NotesBackend.Domain.Entities;

namespace NotesBackend.Infrastructure.Persistence
{
    /// <summary>
    /// EF Core application database context for Notes Backend.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public DbSet<Note> Notes => Set<Note>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var note = modelBuilder.Entity<Note>();
            note.HasKey(n => n.Id);
            note.Property(n => n.Title).HasMaxLength(200);
            note.Property(n => n.Accent).HasMaxLength(32);
            note.Property(n => n.CreatedAtUtc).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            note.Property(n => n.UpdatedAtUtc).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            // Index to speed up title searches in future
            note.HasIndex(n => n.Title);

            base.OnModelCreating(modelBuilder);
        }
    }
}
