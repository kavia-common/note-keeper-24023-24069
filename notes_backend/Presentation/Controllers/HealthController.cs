using Microsoft.AspNetCore.Mvc;
using NotesBackend.Application.Constants;

namespace NotesBackend.Presentation.Controllers
{
    /// <summary>
    /// Provides health and metadata endpoints.
    /// </summary>
    [ApiController]
    [Route("api/health")]
    public class HealthController : ControllerBase
    {
        // PUBLIC_INTERFACE
        /// <summary>
        /// Returns API health and metadata.
        /// </summary>
        /// <returns>Object with status and metadata.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok(new
            {
                status = "Healthy",
                name = ApiMetadata.ApiTitle,
                version = ApiMetadata.ApiVersion,
                theme = "Ocean Professional"
            });
        }
    }
}
