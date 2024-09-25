using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Projects.Data;
using Projects.Models;
using Shared.Dto;
using Shared.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Configure Entity Framework with SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ProjectDbContext>(options =>
    options.UseSqlServer(connectionString));

// Register Converters for dependency injection
builder.Services.AddSingleton<IConverter<Project, ProjectDto>, ProjectConverter>();
builder.Services.AddSingleton<IConverter<Address, AddressDto>, AddressConverter>();
builder.Services.AddSingleton<IConverter<Customer, CustomerDto>, CustomerConverter>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Project API", Version = "v1" });
});

var app = builder.Build();

// Apply migrations during startup
using (var scope = app.Services.CreateScope())
{
var dbContext = scope.ServiceProvider.GetRequiredService<ProjectDbContext>();
// Check if the database is created, and if not, migrate
if (dbContext.Database.EnsureCreated())
{
// Database was created, apply migrations if any
dbContext.Database.Migrate();
}
else
{
// Database exists, just run migrations
dbContext.Database.Migrate();
}
}

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(config => config
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
);


//app.UseHttpsRedirection();
//app.UseAuthentication(); 

app.MapControllers();

app.Run();
