using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using BookstoreApplication.Services;
using BookstoreApplication.Settings;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
        });
});

builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IAuthorReadService, AuthorService>();
builder.Services.AddScoped<IAuthorsRepository, AuthorsRepository>();
builder.Services.AddScoped<IAwardService, AwardService>();
builder.Services.AddScoped<IAwardsRepository, AwardsRepository>();
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddScoped<IPublisherReadService, PublisherService>();
builder.Services.AddScoped<IPublishersRepository, PublishersRepository>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBooksRepository, BooksRepository>();

builder.Services.AddAutoMapper(cfg =>                                                       // Dodavanje AutoMapera
cfg.AddProfile<BookProfile>()
);

builder.Services.AddTransient<ExceptionHandlingMiddleware>();                               // Dodavanje Middleware


builder.Services.AddDbContext<BookstoreDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));         // Dodavanje DBContext klase


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var logger = new LoggerConfiguration()                                                      // Postavljanje SeriLogera
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);


var app = builder.Build();
app.UseCors("AllowAllOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();                                               // Naredba da se Middleware koristi.

app.UseAuthorization();

app.MapControllers();

app.Run();
