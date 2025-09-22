using NotesBackend.Domain.Entities;

namespace NotesBackend.Infrastructure.Repositories
{
    /// <summary>
    /// Repository abstraction for CRUD operations on Note entities.
    /// </summary>
    public interface INoteRepository
    {
        // PUBLIC_INTERFACE
        /// <summary>
        /// Get all notes.
        /// </summary>
        Task<IReadOnlyList<Note>> GetAllAsync(CancellationToken ct = default);

        // PUBLIC_INTERFACE
        /// <summary>
        /// Get a note by its id.
        /// </summary>
        Task<Note?> GetByIdAsync(Guid id, CancellationToken ct = default);

        // PUBLIC_INTERFACE
        /// <summary>
        /// Add a new note.
        /// </summary>
        Task<Note> AddAsync(Note note, CancellationToken ct = default);

        // PUBLIC_INTERFACE
        /// <summary>
        /// Update an existing note.
        /// </summary>
        Task<Note> UpdateAsync(Note note, CancellationToken ct = default);

        // PUBLIC_INTERFACE
        /// <summary>
        /// Delete a note by id. Returns true if deleted.
        /// </summary>
        Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);

        // PUBLIC_INTERFACE
        /// <summary>
        /// Check whether a note exists for the supplied id.
        /// </summary>
        Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
    }
}
