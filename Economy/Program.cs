using Economy.Data;
using Economy.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Shared.Dto;
using Shared.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<EconomyDbContext>(options =>
    options.UseSqlServer(connectionString));

// Register Converters for dependency injection
builder.Services.AddSingleton<IConverter<ProjectEco, ProjectEcoDto>, ProjectEcoConverter>();
builder.Services.AddSingleton<IConverter<Hour, HourDto>, HourConverter>();
builder.Services.AddSingleton<IConverter<Material, MaterialDto>, MaterialConverter>();
builder.Services.AddSingleton<IConverter<EmployeeEco, EmployeeEcoDto>, EmployeeEcoConverter>();
builder.Services.AddSingleton<IConverter<Vacation, VacationDto>, VacationConverter>();
builder.Services.AddSingleton<IConverter<SickLeave, SickLeaveDto>, SickLeaveConverter>();

builder.Services.AddHttpClient("ProjectApi", client =>
{
    client.BaseAddress = new Uri("http://projectapi:8080/api/"); 
});

builder.Services.AddHttpClient("EmployeeApi", client =>
{
    client.BaseAddress = new Uri("http://employeeapi:8080/api/"); 
});

builder.Services.AddHttpClient("EconomyApi", client =>
{
    client.BaseAddress = new Uri("http://economyapi:8080/api/");
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Economy API", Version = "v1" });
});

var app = builder.Build();

/*
// udkommentering skal fjernes for at anvend migreringer under opstart - dette er kun midlertigt til sqlserveren ligger i cloud istedet for lokalt.
using (var scope = app.Services.CreateScope())
{
var dbContext = scope.ServiceProvider.GetRequiredService<EconomyDbContext>();
dbContext.Database.Migrate();
}
*/

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

//app.UseAuthorization();

app.MapControllers();

app.Run();
