namespace Calendar.Application.Interfaces;

public interface IMachineTime
{
    DateTimeOffset Now { get; }
}

public class MachineTime : IMachineTime
{
    public DateTimeOffset Now => DateTimeOffset.UtcNow;
}