using DaMid.Contexts;
using DaMid.Interfaces.Options;
using DaMid.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<IJwtOptions>(builder.Configuration.GetSection("Jwt"));
builder.Services.Configure<IDatabaseOptions>(builder.Configuration.GetSection("Database"));
builder.Services.Configure<ICookieOptions>(builder.Configuration.GetSection("Cookie"));

builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IAudienceService, AudienceService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IClassService, ClassService>();

builder.Services.AddDbContext<ApplicationContext>(options => {
    var database = builder.Configuration.GetValue<string>("Database:ConnectionString");
    if (database != null) {
        options.UseMySql(database, new MySqlServerVersion(new Version()));
    }
});
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddControllers();

var app = builder.Build();

app.UseCors(options => {
    options.AllowAnyHeader();
    options.AllowAnyMethod();
    options.AllowCredentials();
    var origins = builder.Configuration.GetSection("Cors:Origins").Get<string[]>();
    if (origins != null) {
        options.WithOrigins(origins);
    }
});
app.MapControllers();
app.UseSwaggerUI();
app.UseSwagger();

app.Run();
