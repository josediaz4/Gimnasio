using Microsoft.EntityFrameworkCore;
using GymBackend.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<GimnasioContexto>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("GimnasioDB")));

builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(Program)));


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
