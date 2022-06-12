
using System.Net;
using API.Errores;
using API.Helpers;
using API.Middleware;
using Core.Interfaces;
using Infraestructura.Datos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// configurar Heroku

var port = Environment.GetEnvironmentVariable("PORT") ?? "3000";

builder.WebHost.UseKestrel()
            .ConfigureKestrel((context, options) => {
                options.Listen(IPAddress.Any, Int32.Parse(port), listenOptions => {

                });
            });

Console.WriteLine("Puerto Heroku: "+ port);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
                                options.UseMySql(connectionString,
                                ServerVersion.AutoDetect(connectionString)));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<ILugarRepositorio, LugarRepositorio>();

builder.Services.AddScoped(typeof(IRepositorio<>),(typeof(Repositorio<>)));
builder.Services.AddAutoMapper(typeof(MappingProfiles));

builder.Services.AddCors();


builder.Services.Configure<ApiBehaviorOptions>(options =>
    {
        options.InvalidModelStateResponseFactory = ActionContext =>
        {
            var errors = ActionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(x=>x.Value.Errors)
                    .Select(x=>x.ErrorMessage).ToArray();
            var errorResponse = new ApiValidationErrorResponse
            {
                Errors = errors
            };

            return new BadRequestObjectResult(errorResponse);
        };
    });

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

//Aplicar las nuevas migraciones al ejecutar la aplicación y cargar información en BD
using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        await context.Database.MigrateAsync();
        await BaseDatosSeed.SeedAsync(context, loggerFactory);
    }
    catch (System.Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "Un error ocurrió durante la migración");
    }
}

//////////////////////////////////////////////////


// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/


app.UseSwagger();
app.UseSwaggerUI();


app.UseStatusCodePagesWithReExecute("/errors/{0}");


app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader());

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
