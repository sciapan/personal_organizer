using System.Net.Http.Json;
using Calendar.Application.Birthdays.Commands.CreateBirthday;

namespace Calendar.Api.Tests.Integration;

public class BirthdaysTests : IClassFixture<CalendarApiFactory>
{
    private readonly CalendarApiFactory _factory;

    public BirthdaysTests(CalendarApiFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Post_ShouldCreateBirthday()
    {
        // Arrange
        var client = _factory.CreateClient();

        var createBirthdayCommand = new CreateBirthdayCommand
        {
            Person = "test",
            Dob = DateTimeOffset.UtcNow.AddDays(-1),
            Notes = "notes"
        };

        // Act
        var response = await client.PostAsJsonAsync("/birthdays", createBirthdayCommand);

        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Get_ShouldReturnZeroBirthdays()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/birthdays");

        // Arrange
        response.EnsureSuccessStatusCode();
    }
}