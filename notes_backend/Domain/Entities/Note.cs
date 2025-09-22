using System.ComponentModel.DataAnnotations;

namespace NotesBackend.Domain.Entities
{
    /// <summary>
    /// Represents a note entity persisted in the database.
    /// </summary>
    public class Note
    {
        /// <summary>
        /// Primary key identifier for the note.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Title of the note.
        /// </summary>
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Body/content of the note.
        /// </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// When the note was created (UTC).
        /// </summary>
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// When the note was last updated (UTC).
        /// </summary>
        public DateTime UpdatedAtUtc { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Optional color label (hex or palette name) to align with Ocean Professional accents.
        /// </summary>
        [MaxLength(32)]
        public string? Accent { get; set; }
    }
}
