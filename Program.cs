using DaMid.Interfaces.Options;
using DaMid.Services;

var builder = WebApplication.CreateBuilder(args);

foreach (var file in Directory.EnumerateFiles(Environment.CurrentDirectory, "appsettings.*.json")) {
    builder.Configuration.AddJsonFile(file);
}

builder.Services.Configure<IJwtOptions>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapControllers();
app.UseSwaggerUI();
app.UseSwagger();

app.Run();
