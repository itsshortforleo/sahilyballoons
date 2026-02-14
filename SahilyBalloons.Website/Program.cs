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

// --- Security headers (Lighthouse Best Practices) -------------------------
app.Use(async (context, next) =>
{
    var headers = context.Response.Headers;

    // Strict-Transport-Security (HSTS) – enforce HTTPS for 1 year + subdomains
    headers["Strict-Transport-Security"] = "max-age=31536000; includeSubDomains";

    // X-Frame-Options – prevent clickjacking
    headers["X-Frame-Options"] = "SAMEORIGIN";

    // X-Content-Type-Options – prevent MIME sniffing
    headers["X-Content-Type-Options"] = "nosniff";

    // Referrer-Policy – limit referrer info leakage
    headers["Referrer-Policy"] = "strict-origin-when-cross-origin";

    // Permissions-Policy – disable unused browser features
    headers["Permissions-Policy"] = "camera=(), microphone=(), geolocation=(), payment=()";

    // Cross-Origin-Opener-Policy – proper origin isolation
    headers["Cross-Origin-Opener-Policy"] = "same-origin-allow-popups";

    // Content-Security-Policy – mitigate XSS
    headers["Content-Security-Policy"] = string.Join("; ",
        "default-src 'self'",
        "script-src 'self' 'unsafe-inline' 'unsafe-eval' https://*.googletagmanager.com https://*.google-analytics.com https://*.google.com https://*.googleadservices.com https://googleads.g.doubleclick.net https://*.doubleclick.net https://bat.bing.com https://*.bing.com https://*.instagram.com https://*.clarity.ms https://*.facebook.com https://*.facebook.net https://cdninstagram.com https://*.cdninstagram.com",
        "style-src 'self' 'unsafe-inline' https://fonts.googleapis.com https://*.instagram.com",
        "img-src 'self' data: https: blob:",
        "font-src 'self' https://fonts.gstatic.com data:",
        "connect-src 'self' https://*.google-analytics.com https://*.googletagmanager.com https://*.google.com https://*.doubleclick.net https://bat.bing.com https://*.bing.com https://*.clarity.ms https://*.instagram.com https://*.facebook.com https://*.facebook.net",
        "frame-src https://*.instagram.com https://*.google.com https://*.doubleclick.net https://*.facebook.com https://*.googletagmanager.com",
        "media-src 'self' https://*.cdninstagram.com https://*.instagram.com https://*.fbcdn.net blob:",
        "worker-src 'self' blob:",
        "child-src 'self' blob: https://*.instagram.com",
        "object-src 'none'",
        "base-uri 'self'",
        "form-action 'self'"
    );

    await next();
});
// --------------------------------------------------------------------------

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
