using NotesBackend.Contracts.DTOs;

namespace NotesBackend.Application.Services
{
    /// <summary>
    /// Application service for note-related business logic.
    /// </summary>
    public interface INoteService
    {
        // PUBLIC_INTERFACE
        /// <summary>
        /// Retrieve all notes.
        /// </summary>
        Task<IReadOnlyList<NoteResponse>> GetNotesAsync(CancellationToken ct = default);

        // PUBLIC_INTERFACE
        /// <summary>
        /// Retrieve a note by id.
        /// </summary>
        Task<NoteResponse?> GetNoteAsync(Guid id, CancellationToken ct = default);

        // PUBLIC_INTERFACE
        /// <summary>
        /// Create a new note.
        /// </summary>
        Task<NoteResponse> CreateNoteAsync(CreateNoteRequest request, CancellationToken ct = default);

        // PUBLIC_INTERFACE
        /// <summary>
        /// Update a note by id.
        /// </summary>
        Task<NoteResponse?> UpdateNoteAsync(Guid id, UpdateNoteRequest request, CancellationToken ct = default);

        // PUBLIC_INTERFACE
        /// <summary>
        /// Delete a note by id. Returns true if deleted.
        /// </summary>
        Task<bool> DeleteNoteAsync(Guid id, CancellationToken ct = default);
    }
}
