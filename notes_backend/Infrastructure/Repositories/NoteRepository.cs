using Microsoft.EntityFrameworkCore;
using NotesBackend.Domain.Entities;
using NotesBackend.Infrastructure.Persistence;

namespace NotesBackend.Infrastructure.Repositories
{
    /// <summary>
    /// EF Core-based repository for notes.
    /// </summary>
    public class NoteRepository : INoteRepository
    {
        private readonly AppDbContext _db;

        public NoteRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IReadOnlyList<Note>> GetAllAsync(CancellationToken ct = default)
        {
            return await _db.Notes.AsNoTracking().OrderByDescending(n => n.UpdatedAtUtc).ToListAsync(ct);
        }

        public async Task<Note?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _db.Notes.FindAsync(new object[] { id }, ct);
        }

        public async Task<Note> AddAsync(Note note, CancellationToken ct = default)
        {
            await _db.Notes.AddAsync(note, ct);
            await _db.SaveChangesAsync(ct);
            return note;
        }

        public async Task<Note> UpdateAsync(Note note, CancellationToken ct = default)
        {
            _db.Notes.Update(note);
            await _db.SaveChangesAsync(ct);
            return note;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var entity = await _db.Notes.FindAsync(new object[] { id }, ct);
            if (entity == null) return false;
            _db.Notes.Remove(entity);
            await _db.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
        {
            return await _db.Notes.AnyAsync(n => n.Id == id, ct);
        }
    }
}
