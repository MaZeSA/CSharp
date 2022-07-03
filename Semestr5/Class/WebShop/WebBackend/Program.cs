using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using WebBackend.Data;
using WebBackend.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppEFContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//підключення документації
var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
builder.Services.AddSwaggerGen(c =>
{
    var fileDoc = Path.Combine(AppContext.BaseDirectory, $"{assemblyName}.xml");
    c.IncludeXmlComments(fileDoc);
});

builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(AppMapProfile));
builder.Services.AddFluentValidation(x =>
                            x.RegisterValidatorsFromAssemblyContaining<Program>());

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(option => 
            option.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

string folderImage = Path.Combine(Directory.GetCurrentDirectory(), "images");

if (!Directory.Exists(folderImage))
    Directory.CreateDirectory(folderImage);
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(folderImage),
    RequestPath = "/images"
});

app.UseAuthorization();

app.MapControllers();

app.Run();
