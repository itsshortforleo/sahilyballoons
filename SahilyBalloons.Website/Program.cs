using SahilyBalloons.Data;
using Microsoft.EntityFrameworkCore;
using SahilyBalloons.Website.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddDefaultServices();

var app = builder.Build();

app.AddDefaultConfiguration();

app.Run();
