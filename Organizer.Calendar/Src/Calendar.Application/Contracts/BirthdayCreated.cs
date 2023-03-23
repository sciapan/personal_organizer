namespace Calendar.Application.Contracts;

public record BirthdayCreated
{
    /// <summary>
    /// Unique id of record.
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Date of birth.
    /// </summary>
    public required DateTimeOffset Dob { get; init; }

    /// <summary>
    /// Person name.
    /// </summary>
    public required string Person { get; init; }
}