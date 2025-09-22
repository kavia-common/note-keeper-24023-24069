using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace NotesBackend.Infrastructure.Persistence
{
    /// <summary>
    /// Design time factory to enable 'dotnet ef' tooling to create the DbContext.
    /// Uses SQLite by default to support preview builds.
    /// </summary>
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlite("Data Source=notes.db");
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
