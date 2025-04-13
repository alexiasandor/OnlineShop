using Azure.Storage.Blobs;
using Laroa.Api;
using Laroa.Application;
using Laroa.Domain;
using Laroa.Domain.Interfaces.Repositories;
using Laroa.Domain.Interfaces.Services;
using Laroa.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add CORS - pentru legarea cu frontend 
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader(); ;
                      });
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHostedService<BackgroundWorkerService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IImageStorageService, ImageStorageService>();
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();
builder.Services.AddTransient<IMailService, MailService>();
builder.Services.AddTransient<IReviewRepository, ReviewRepository>();
builder.Services.AddTransient<IReviewService, ReviewService>();
builder.Services.AddScoped<IReduceriRepository, ReduceriRepository>();
builder.Services.AddScoped<IReduceriService, ReduceriService>();

builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IAdminService, AdminService>();

builder.Services.AddScoped<TFIDFExtractor>();
builder.Services.AddScoped<IContentBasedRecommendationService, ContentBasedRecommendationService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// DataBase Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddAutoMapper(typeof(IAssemblyMarker));


// Blob Storage Settings
builder.Services.Configure<BlobStorageSettings>(builder.Configuration.GetSection(nameof(BlobStorageSettings)));
var blobStorageSettings = builder.Configuration.GetSection(nameof(BlobStorageSettings)).Get<BlobStorageSettings>();

// Blob Storage Service
builder.Services.AddSingleton(blobService => new BlobServiceClient(blobStorageSettings.ConnectionString));

// Mail Settings
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

// Authentication
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));
var jwtSettings = builder.Configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
          policy.RequireRole(UserRole.Admin));
    options.AddPolicy("ClientPolicy", policy =>
          policy.RequireRole(UserRole.Client));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthentication(); // ca sa permitem API ului sa autentifice utilizatorii
app.UseAuthorization();
app.UseCors(options => options.AllowAnyOrigin());

app.MapControllers();

app.Run();
