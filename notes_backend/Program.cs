using Microsoft.EntityFrameworkCore;
using NotesBackend.Application.Constants;
using NotesBackend.Application.Services;
using NotesBackend.Infrastructure.Persistence;
using NotesBackend.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        // Keep default problem details for validation errors
    });

// OpenAPI/Swagger (NSwag)
builder.Services.AddOpenApiDocument(config =>
{
    config.Title = ApiMetadata.ApiTitle;
    config.Version = ApiMetadata.ApiVersion;
    config.Description = ApiMetadata.ApiDescription;
    config.DocumentName = "v1";

    // Use custom operation processor to tag operations by controller name
    config.OperationProcessors.Add(new NotesBackend.Infrastructure.OpenApi.TagByControllerNameProcessor());
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.SetIsOriginAllowed(_ => true)
              .AllowCredentials()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Database configuration
// Note: The connection string is read from environment variable NOTES_DB_CONNECTION,
// which should be provided by the 'notes_database' container orchestration.
// Instructions for future agent: ensure this env var is set in deployment configuration.
var connectionString = Environment.GetEnvironmentVariable("NOTES_DB_CONNECTION");

// If no external DB provided, fallback to local SQLite file for preview/dev
var useSqlite = string.IsNullOrWhiteSpace(connectionString);

if (useSqlite)
{
    builder.Services.AddDbContext<AppDbContext>(opt =>
        opt.UseSqlite("Data Source=notes.db"));
}
else
{
    builder.Services.AddDbContext<AppDbContext>(opt =>
        opt.UseNpgsql(connectionString)); // requires Npgsql package
}

// DI registrations
builder.Services.AddScoped<INoteRepository, NoteRepository>();
builder.Services.AddScoped<INoteService, NoteService>();

var app = builder.Build();

// Apply migrations automatically on startup (safe for dev/preview)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Middleware
app.UseCors("AllowAll");

app.UseOpenApi();
app.UseSwaggerUi(config =>
{
    config.Path = "/docs";
    config.DocumentTitle = $"{ApiMetadata.ApiTitle} - Docs";
});

// Map controllers
app.MapControllers();

// Root health
app.MapGet("/", () => Results.Json(new { message = "Healthy" }))
   .WithTags(ApiMetadata.Tags.Health);

app.Run();