using DaMid.Contexts;
using DaMid.Interfaces.Options;
using DaMid.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<IJwtOptions>(builder.Configuration.GetSection("Jwt"));
builder.Services.Configure<IDatabaseOptions>(builder.Configuration.GetSection("Database"));

builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddDbContext<ApplicationContext>(options => {
    var database = builder.Configuration.GetValue<string>("Database:ConnectionString");
    if (database != null) {
        options.UseMySQL(database);
    }
});
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapControllers();
app.UseSwaggerUI();
app.UseSwagger();

app.Run();
