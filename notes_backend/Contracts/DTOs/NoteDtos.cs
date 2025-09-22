using System.ComponentModel.DataAnnotations;

namespace NotesBackend.Contracts.DTOs
{
    /// <summary>
    /// API response shape for a Note.
    /// </summary>
    public class NoteResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAtUtc { get; set; }
        public DateTime UpdatedAtUtc { get; set; }
        public string? Accent { get; set; }
    }

    /// <summary>
    /// Request payload to create a new note.
    /// </summary>
    public class CreateNoteRequest
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        [MaxLength(32)]
        public string? Accent { get; set; }
    }

    /// <summary>
    /// Request payload to update an existing note.
    /// </summary>
    public class UpdateNoteRequest
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        [MaxLength(32)]
        public string? Accent { get; set; }
    }
}
