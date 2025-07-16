using Microsoft.EntityFrameworkCore;
using MyAPI.Data;
using MyAPI.Services;
using System;
var builder = WebApplication.CreateBuilder(args);
//EF core :
builder.Services.AddDbContext<MyDbContext>(options =>options.UseSqlite("Data Source=books.db"));
// Add services to the container.
//builder.Services.AddSingleton<IBookService,BookServie>();
builder.Services.AddScoped<IBookService, BookServie>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
