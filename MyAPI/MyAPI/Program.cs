using Microsoft.EntityFrameworkCore;
using MyAPI.Data;
using MyAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);
//EF core :
builder.Services.AddDbContext<MyDbContext>(options =>options.UseSqlite("Data Source=books.db"));
// Add services to the container.
//builder.Services.AddSingleton<IBookService,BookServie>();
builder.Services.AddScoped<IBookService, BookServie>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "MyAPI", Version = "v1" });

    
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "TOken Bearer eyJhbGciOiJIUzI1NiIsInR5..."
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

//JTW Token:
var jtwIssur = builder.Configuration.GetSection("JwtSettings:Issuer").Get<string>();
var jtwKey = builder.Configuration.GetSection("JwtSettings:Key").Get<string>();
var jtwAudience = builder.Configuration.GetSection("JwtSettings:Audience").Get<string>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => { options.TokenValidationParameters = new TokenValidationParameters { ValidateIssuer = true, ValidateAudience = false, ValidateLifetime = true, ValidateIssuerSigningKey = true, ValidIssuer = jtwIssur, IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jtwKey)) }; });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
