using Footlink.Infrastructure.Data;
using Footlink.Domain.Interfaces;
using Footlink.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add configuration per ambienti multipli
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurazione dinamica del DbContext
var dbProvider = builder.Configuration["Database:Provider"];

if (dbProvider == "InMemory")
{
    builder.Services.AddDbContext<FootlinkDbContext>(options =>
        options.UseInMemoryDatabase("FootlinkTestDb"));
}
else if (dbProvider == "Postgres")
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<FootlinkDbContext>(options =>
        options.UseNpgsql(connectionString));
}
else
{
    throw new Exception("Unsupported DB provider: " + dbProvider);
}

// Dependency injection dei repository
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();