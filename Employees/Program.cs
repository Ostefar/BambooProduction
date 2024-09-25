using Employees.Data;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Employees.Models;
using System.Threading.Tasks;
using Shared.Dto;
using Shared.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure Entity Framework with SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<EmployeeDbContext>(options =>
    options.UseSqlServer(connectionString));

// Register Converters for dependency injection
builder.Services.AddSingleton<IConverter<Employee, EmployeeDto>, EmployeeConverter>();
builder.Services.AddSingleton<IConverter<Address, AddressDto>, AddressConverter>();

// Configure Swagger to use cookie authentication
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Employee API", Version = "v1" });
});

var app = builder.Build();

// Apply migrations during startup
using (var scope = app.Services.CreateScope())
{
var dbContext = scope.ServiceProvider.GetRequiredService<EmployeeDbContext>();
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
