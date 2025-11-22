using System.IO.Compression;
using System.Linq;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.ResponseCompression;

using SahilyBalloons.Data;
using Microsoft.EntityFrameworkCore;
using SahilyBalloons.Website.Configuration;

var builder = WebApplication.CreateBuilder(args);

// --- Performance: response compression + caching ---------------------------
// Response compression (Brotli + Gzip)
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true; // compress responses over HTTPS
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();

    // Compress common text MIME types; add SVG if you use inline SVGs
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes
        .Concat(new[] { "image/svg+xml" });
});

// Configure compression levels (tune for CPU vs size)
builder.Services.Configure<BrotliCompressionProviderOptions>(opts =>
{
    opts.Level = CompressionLevel.Fastest; // or CompressionLevel.Optimal
});

builder.Services.Configure<GzipCompressionProviderOptions>(opts =>
{
    opts.Level = CompressionLevel.Optimal;
});

// Add server-side response caching
builder.Services.AddResponseCaching();
// -------------------------------------------------------------------------

// Keep your app's default registrations
builder.AddDefaultServices();

var app = builder.Build();

// --- Use middleware in the correct order -------------------------------
// Compression should come before static files so static files may be compressed
app.UseResponseCompression();

// Serve static files with a long cache TTL so browsers & CDN can cache them
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        // 7 days
        const int durationInSeconds = 60 * 60 * 24 * 7;
        ctx.Context.Response.Headers[HeaderNames.CacheControl] = "public,max-age=" + durationInSeconds;
    }
});

// Enable server-side response caching for dynamic responses where appropriate
app.UseResponseCaching();
// -------------------------------------------------------------------------

// Keep your app's default configuration (routing, endpoints, auth, etc.)
app.AddDefaultConfiguration();

app.Run();
