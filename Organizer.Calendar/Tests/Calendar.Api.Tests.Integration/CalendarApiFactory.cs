using Calendar.Application.Interfaces;
using Calendar.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;

namespace Calendar.Api.Tests.Integration;

public class CalendarApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder().WithImage("postgres:latest")
        .WithUsername("integration_tester")
        .WithPassword("integration_tester_password")
        .WithDatabase("calendar")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<CalendarDbContext>))!;

            services.Remove(descriptor);

            services.AddDbContext<ICalendarDbContext, CalendarDbContext>(
                optionsBuilder => optionsBuilder.UseNpgsql(_postgreSqlContainer.GetConnectionString(),
                contextOptionsBuilder => contextOptionsBuilder.MigrationsAssembly("Calendar.Migrations")));

            var sp = services.BuildServiceProvider();

            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<CalendarDbContext>();

                db.Database.EnsureCreated();
            }
        });
    }

    public new Task DisposeAsync()
    {
        return _postgreSqlContainer.DisposeAsync().AsTask();
    }

    public Task InitializeAsync()
    {
        return _postgreSqlContainer.StartAsync();
    }
}