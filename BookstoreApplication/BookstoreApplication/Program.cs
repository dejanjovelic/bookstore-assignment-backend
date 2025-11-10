using BookstoreApplication.Models;
using BookstoreApplication.Models.IRepositoies;
using BookstoreApplication.Repositories;
using BookstoreApplication.Services;
using BookstoreApplication.Services.IServices;
using BookstoreApplication.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Threading.RateLimiting;
using System.Text;
using System.Security.Claims;
using Microsoft.OpenApi.Models;
using BookstoreApplication.Data;
using Microsoft.AspNetCore.Authentication;
using BookstoreApplication.Infrastructure;

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
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IVolumeService, VolumeService>();
builder.Services.AddScoped<IComicVineConnection, ComicVineConnection>();
builder.Services.AddScoped<IIssueService, IssueService>();
builder.Services.AddScoped<IIssuesRepository, IssuesRepository>();

builder.Services.AddHttpClient<ComicVineConnection>();

builder.Services.AddAutoMapper(cfg =>                                                       // Dodavanje AutoMapera
{                                                       
    cfg.AddProfile<BookProfile>();
    cfg.AddProfile<IssueProfile>();
});

builder.Services.AddTransient<ExceptionHandlingMiddleware>();                               // Dodavanje Middleware


builder.Services.AddDbContext<BookstoreDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));         // Dodavanje DBContext klase

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()                               //Registracija Identity-ja
    .AddEntityFrameworkStores<BookstoreDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>                                      //Definisanje uslova koje lozinka mora da ispuni
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
}
);

// Konfigurisanje Autentifikacije
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddCookie()
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateLifetime = true,

            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],

            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),

            RoleClaimType = ClaimTypes.Role
        };
    })
    .AddGoogle("Google", options =>
    {
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];

        options.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "given_name");
        options.ClaimActions.MapJsonKey(ClaimTypes.Surname, "family_name");

        options.CallbackPath = "/api/Auth/signin-google";
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CreateBook",
        policy => policy.RequireRole("Editor", "Librarian"));
    options.AddPolicy("EditBook",
        policy => policy.RequireRole("Editor"));
    options.AddPolicy("DeleteBook",
        policy => policy.RequireRole("Editor"));
});


// Dodavanje Autentifikacije
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


// Podesavanje Swagger-a za Autentifikaciju
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Building Example API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insert JWT token"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


var logger = new LoggerConfiguration()                                                      // Postavljanje SeriLogera
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedData.InitializeAsync(services);
}

app.UseCors("AllowAllOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();                                               // Naredba da se Middleware koristi.

app.UseAuthentication();                                                                        // Ukljucivanje autentifikacije;
app.UseAuthorization();                                                                         // Uvodjenje autorizacije;

app.MapControllers();

app.Run();
