using Calendar.Application.Birthdays.Commands.CreateBirthday;
using Calendar.Application.Birthdays.Commands.DeleteBirthday;
using Calendar.Application.Birthdays.Models;
using Calendar.Application.Birthdays.Queries.GetBirthday;
using Calendar.Application.Birthdays.Queries.GetBirthdays;
using Calendar.Application.Interfaces;
using Calendar.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(CreateBirthdayCommand).Assembly);
builder.Services.AddCors(); // TODO set CORS

var cs = builder.Environment.IsDevelopment()
    ? builder.Configuration["ConnectionStrings:CalendarDb"]
    : builder.Configuration["ConnectionStrings:CalendarDbDocker"];

builder.Services.AddDbContext<ICalendarDbContext, CalendarDbContext>(optionsBuilder =>
    optionsBuilder.UseNpgsql(cs,
        contextOptionsBuilder => contextOptionsBuilder.MigrationsAssembly("Calendar.Migrations")));

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(policyBuilder => policyBuilder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()); // TODO set CORS
}

// TODO
//app.UseHttpsRedirection();

// birthdays
app.MapGet("/birthdays", async (IMediator mediator, CancellationToken cancellationToken) =>
    {
        var result = await mediator.Send(new GetBirthdaysQuery(), cancellationToken);
        return Results.Ok(result);
    })
    .Produces<BirthdayVm[]>()
    .WithName("GetBirthdays")
    .WithTags("birthdays");

app.MapGet("/birthdays/{id}", async (int id, IMediator mediator, CancellationToken cancellationToken) =>
    {
        var result = await mediator.Send(new GetBirthdayQuery { Id = id }, cancellationToken);
        return result == null ? Results.NotFound() : Results.Ok(result);
    })
    .Produces<BirthdayVm>()
    .Produces(StatusCodes.Status404NotFound)
    .WithName("GetBirthday")
    .WithTags("birthdays");

app.MapPost("/birthdays",
        async (CreateBirthdayCommand command, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(command, cancellationToken);
            return Results.Created($"/birthdays/{result.Id}", result);
        })
    .Produces<BirthdayVm>(StatusCodes.Status201Created)
    .WithName("CreateBirthday")
    .WithTags("birthdays");

app.MapDelete("/birthday/{id}", async (int id, IMediator mediator, CancellationToken cancellationToken) =>
    {
        var result = await mediator.Send(new DeleteBirthdayCommand { Id = id }, cancellationToken);
        return result == null ? Results.NotFound() : Results.Ok(result);
    })
    .Produces<BirthdayVm>()
    .Produces(StatusCodes.Status404NotFound)
    .WithName("DeleteBirthday")
    .WithTags("birthdays");

app.Run();