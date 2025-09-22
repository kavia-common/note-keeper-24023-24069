using Microsoft.AspNetCore.Mvc;
using NotesBackend.Application.Services;
using NotesBackend.Contracts.DTOs;

namespace NotesBackend.Presentation.Controllers
{
    /// <summary>
    /// Notes CRUD API endpoints.
    /// </summary>
    [ApiController]
    [Route("api/notes")]
    [Produces("application/json")]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _service;
        private readonly ILogger<NotesController> _logger;

        public NotesController(INoteService service, ILogger<NotesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // PUBLIC_INTERFACE
        /// <summary>
        /// List all notes ordered by last updated date descending.
        /// </summary>
        /// <returns>Array of notes.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<NoteResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync(CancellationToken ct)
        {
            var result = await _service.GetNotesAsync(ct);
            return Ok(result);
        }

        // PUBLIC_INTERFACE
        /// <summary>
        /// Get a note by its id.
        /// </summary>
        /// <param name="id">Note id (Guid).</param>
        /// <returns>Note if found.</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(NoteResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken ct)
        {
            var note = await _service.GetNoteAsync(id, ct);
            if (note is null) return NotFound();
            return Ok(note);
        }

        // PUBLIC_INTERFACE
        /// <summary>
        /// Create a new note.
        /// </summary>
        /// <param name="request">Payload containing title, content, and optional accent.</param>
        /// <returns>Created note with id.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(NoteResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateNoteRequest request, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var created = await _service.CreateNoteAsync(request, ct);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = created.Id }, created);
        }

        // PUBLIC_INTERFACE
        /// <summary>
        /// Update an existing note by id.
        /// </summary>
        /// <param name="id">Note id (Guid).</param>
        /// <param name="request">Updated title, content, and accent.</param>
        /// <returns>Updated note.</returns>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(NoteResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateNoteRequest request, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var updated = await _service.UpdateNoteAsync(id, request, ct);
            if (updated is null) return NotFound();
            return Ok(updated);
        }

        // PUBLIC_INTERFACE
        /// <summary>
        /// Delete a note by id.
        /// </summary>
        /// <param name="id">Note id (Guid).</param>
        /// <returns>No content if deleted, 404 otherwise.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken ct)
        {
            var deleted = await _service.DeleteNoteAsync(id, ct);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
