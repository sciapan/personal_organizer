namespace Calendar.Application.Contracts;

public record BirthdayDeleted
{
    /// <summary>
    /// Unique id of record.
    /// </summary>
    public required int Id { get; init; }
}