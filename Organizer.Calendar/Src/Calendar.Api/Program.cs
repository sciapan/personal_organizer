using Calendar.Application.Behaviors;
using Calendar.Application.Birthdays.Commands.CreateBirthday;
using Calendar.Application.Birthdays.Commands.DeleteBirthday;
using Calendar.Application.Birthdays.Models;
using Calendar.Application.Birthdays.Queries.GetBirthday;
using Calendar.Application.Birthdays.Queries.GetBirthdays;
using Calendar.Application.Interfaces;
using Calendar.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

// try / catch to ensure correct logging of all configuration issues
try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // add mediatR
    builder.Services.AddMediatR(typeof(CreateBirthdayCommand).Assembly);
    builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

    // add fluent validation
    builder.Services.AddValidatorsFromAssemblyContaining<CreateBirthdayCommandValidator>();

    // add time machine to easy retrive & moq time
    builder.Services.AddSingleton<IMachineTime, MachineTime>();
    
    builder.Services.AddCors(); // TODO set CORS

    var cs = builder.Configuration["ConnectionStrings:CalendarDbDocker"]!;

    builder.Services.AddDbContext<ICalendarDbContext, CalendarDbContext>(optionsBuilder =>
        optionsBuilder.UseNpgsql(cs,
            contextOptionsBuilder => contextOptionsBuilder.MigrationsAssembly("Calendar.Migrations")));

    // logging
    builder.Host.UseSerilog();

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
            return result.Match(result => Results.Ok(result), notFound => Results.NotFound());
        })
        .Produces<BirthdayVm>()
        .Produces(StatusCodes.Status404NotFound)
        .WithName("GetBirthday")
        .WithTags("birthdays");

    app.MapPost("/birthdays",
            async (CreateBirthdayCommand command, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(command, cancellationToken);
                return result.Match(result => Results.Created($"/birthdays/{result.Id}", result), vaildationFailed => Results.BadRequest(vaildationFailed.Errors));
            })
        .Produces<BirthdayVm>(StatusCodes.Status201Created)
        .WithName("CreateBirthday")
        .WithTags("birthdays");

    app.MapDelete("/birthdays/{id}", async (int id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new DeleteBirthdayCommand { Id = id }, cancellationToken);
            result.Match(result => Results.Ok(), notFound => Results.NotFound());
        })
        .Produces<BirthdayVm>()
        .Produces(StatusCodes.Status404NotFound)
        .WithName("DeleteBirthday")
        .WithTags("birthdays");

    app.Run();
}
catch (HostAbortedException) // rethrow for tools (dotnet-ef e.g.)
{
    throw;
}
catch (Exception e)
{
    string type = e.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal))
    {
        throw; // rethrow for tools (dotnet-ef e.g.)
    }

    Log.Fatal(e, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}