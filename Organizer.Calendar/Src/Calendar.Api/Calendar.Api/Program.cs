using Calendar.Application.Birthdays.Commands.CreateBirthday;
using Calendar.Application.Birthdays.Commands.DeleteBirthday;
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
builder.Services.AddDbContext<ICalendarDbContext, CalendarDbContext>(optionsBuilder =>
    optionsBuilder.UseNpgsql(@"Host=localhost;Username=postgres;Password=sa;Database=calendar",
        contextOptionsBuilder => contextOptionsBuilder.MigrationsAssembly("Calendar.Migrations")));
builder.Services.AddMediatR(typeof(CreateBirthdayCommand).Assembly);
builder.Services.AddCors(); // TODO set CORS

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(policyBuilder => policyBuilder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()); // TODO set CORS
}

app.UseHttpsRedirection();

// birthdays

app.MapGet("/birthday",
    async (IMediator mediator, CancellationToken cancellationToken) =>
        await mediator.Send(new GetBirthdaysQuery(), cancellationToken));

app.MapPost("/birthday",
    async (CreateBirthdayCommand command, IMediator mediator, CancellationToken cancellationToken) =>
        await mediator.Send(command, cancellationToken));

app.MapDelete("/birthday/{id}",
    async (int id, IMediator mediator, CancellationToken cancellationToken) =>
        await mediator.Send(new DeleteBirthdayCommand { Id = id }, cancellationToken));

app.Run();