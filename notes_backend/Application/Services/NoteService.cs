using NotesBackend.Contracts.DTOs;
using NotesBackend.Domain.Entities;
using NotesBackend.Infrastructure.Repositories;

namespace NotesBackend.Application.Services
{
    /// <summary>
    /// Implementation of note business logic and mapping.
    /// </summary>
    public class NoteService : INoteService
    {
        private readonly INoteRepository _repo;

        public NoteService(INoteRepository repo)
        {
            _repo = repo;
        }

        public async Task<IReadOnlyList<NoteResponse>> GetNotesAsync(CancellationToken ct = default)
        {
            var notes = await _repo.GetAllAsync(ct);
            return notes.Select(ToResponse).ToList();
        }

        public async Task<NoteResponse?> GetNoteAsync(Guid id, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            return entity is null ? null : ToResponse(entity);
        }

        public async Task<NoteResponse> CreateNoteAsync(CreateNoteRequest request, CancellationToken ct = default)
        {
            // Normalize content to avoid accidental trailing spaces
            var note = new Note
            {
                Title = (request.Title ?? string.Empty).Trim(),
                Content = (request.Content ?? string.Empty).Trim(),
                Accent = string.IsNullOrWhiteSpace(request.Accent) ? null : request.Accent!.Trim(),
                CreatedAtUtc = DateTime.UtcNow,
                UpdatedAtUtc = DateTime.UtcNow
            };
            var saved = await _repo.AddAsync(note, ct);
            return ToResponse(saved);
        }

        public async Task<NoteResponse?> UpdateNoteAsync(Guid id, UpdateNoteRequest request, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity is null) return null;

            entity.Title = (request.Title ?? string.Empty).Trim();
            entity.Content = (request.Content ?? string.Empty).Trim();
            entity.Accent = string.IsNullOrWhiteSpace(request.Accent) ? null : request.Accent!.Trim();
            entity.UpdatedAtUtc = DateTime.UtcNow;

            var saved = await _repo.UpdateAsync(entity, ct);
            return ToResponse(saved);
        }

        public async Task<bool> DeleteNoteAsync(Guid id, CancellationToken ct = default)
        {
            return await _repo.DeleteAsync(id, ct);
        }

        private static NoteResponse ToResponse(Note n) => new NoteResponse
        {
            Id = n.Id,
            Title = n.Title,
            Content = n.Content,
            CreatedAtUtc = n.CreatedAtUtc,
            UpdatedAtUtc = n.UpdatedAtUtc,
            Accent = n.Accent
        };
    }
}
