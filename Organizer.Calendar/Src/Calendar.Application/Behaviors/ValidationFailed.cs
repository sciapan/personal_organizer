namespace Calendar.Application.Behaviors
{
    public record ValidationFailed(IDictionary<string, string[]> Errors)
    {
    }
}