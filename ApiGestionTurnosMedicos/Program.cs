using ApiGestionTurnosMedicos;
using ApiGestionTurnosMedicos.Controllers;
using ApiGestionTurnosMedicos.Services;
using BusinessLogic.AppLogic;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IMessage, Message>();

//mapear la configuracion de el modelo emailsettings con el json de gmailsettings
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("GmailSettings"));

// Esto para evitar el error de CORS. También hay que añadir el paquete
// Microsoft.AspNetCore.Cors
// Basado en:
// https://learn.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-8.0#same-origin
var origenLocalHost = "_origenLocalHost";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: origenLocalHost,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000",
                                             "https://localhost:3000")
                                             .AllowAnyHeader()
                                             .AllowAnyMethod(); ;
                      });
});

builder.Services.AddDbContext<GestionTurnosContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.MapPost("/sendEmail", (SendEmailRequest request) =>
{
    var service = app.Services.GetService<IMessage>();
    service!.SendEmail(request.Subject, request.Body, request.To);
});

// Esto para evitar el error de CORS. También hay que añadir el paquete
// Microsoft.AspNetCore.Cors
// Basado en:
// https://learn.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-8.0#same-origin
app.UseCors(origenLocalHost);

app.Run();
