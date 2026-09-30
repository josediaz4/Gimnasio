using GymBackend.Data;
using GymBackend.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "GymBackend API",
        Version = "v1"
    });
});

builder.Services.AddDbContext<GimnasioContexto>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GimnasioDB")));

builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(Program)));

//Validaciones automaticas con ApiBehaviorOptions
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
        .Where(e => e.Value!.Errors.Count > 0)
        .Select(e => new
        {
            Campo = e.Key,
            Errores = e.Value!.Errors.Select(err => err.ErrorMessage)
        });

        var response = new
        {
            StatusCode = 400,
            Message = "El modelo enviado no es válido",
            Errors = errors
        };

        return new BadRequestObjectResult(response);
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GymBackend API v1");
        c.RoutePrefix = String.Empty;
    });
}
//Middleware global de excepciones
app.UseMiddleware<ExceptionMiddleware>();

//Resto de Middlewares
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
