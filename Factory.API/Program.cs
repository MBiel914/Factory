using Factory.API.Core.Configurations;
using Factory.API.Core.Contracts;
using Factory.API.Core.Middlewares;
using Factory.API.Core.Repositories;
using Factory.API.Data.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("FactoryDbConnectionString");

// Add services to the container.
builder.Services.AddDbContext<FactoryDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

//Authentications
builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>(builder.Configuration["JwtSettings:Issuer"])
    .AddEntityFrameworkStores<FactoryDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", b => b.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
});

//Mappers
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
builder.Services.AddScoped(typeof(IGeneralRepository<>), typeof(GeneralRepository<>));
builder.Services.AddScoped<IToolTypeRepository, ToolTypeRepository>();
builder.Services.AddScoped<IAuthManager, ManagerRepository>();

//Loggers
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//OData
builder.Services.AddControllers().AddOData(options =>
{
    options.Select().Filter().OrderBy();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

//Authentications
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//Middlewares
app.UseMiddleware<ExceptionMiddleware>();

app.Run();
