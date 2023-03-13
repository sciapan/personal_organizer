namespace Calendar.Api.Tests.Integration;

public class BirthdaysTests : IClassFixture<CalendarApiFactory>
{
    private readonly CalendarApiFactory _factory;

    public BirthdaysTests(CalendarApiFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Get_ReturnsSuccess()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/birthdays");

        // Arrange
        response.EnsureSuccessStatusCode();
    }
}