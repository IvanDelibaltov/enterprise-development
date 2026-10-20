using Scalar.AspNetCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Application.Services;
using VeterinaryClinic.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddOpenApi();
builder.Services.AddClinicRepositories();
builder.Services.AddScoped<BreedService>();
builder.Services.AddScoped<SpecializationService>();
builder.Services.AddScoped<OwnerService>();
builder.Services.AddScoped<VeterinarianService>();
builder.Services.AddScoped<PetService>();
builder.Services.AddScoped<AppointmentService>();
builder.Services.AddProblemDetails();

var app = builder.Build();
app.UseExceptionHandler(handler => handler.Run(async context =>
{
    var error = context.Features.Get<IExceptionHandlerFeature>()?.Error;
    var status = error switch
    {
        KeyNotFoundException => StatusCodes.Status404NotFound,
        ArgumentException => StatusCodes.Status400BadRequest,
        InvalidOperationException => StatusCodes.Status409Conflict,
        _ => StatusCodes.Status500InternalServerError
    };
    if (status == 500) app.Logger.LogError(error, "Unhandled API error");
    context.Response.StatusCode = status;
    await Results.Problem(statusCode: status, title: status == 500 ? "Internal server error" : error?.Message).ExecuteAsync(context);
}));
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.MapControllers();
app.Run();
public partial class Program;
