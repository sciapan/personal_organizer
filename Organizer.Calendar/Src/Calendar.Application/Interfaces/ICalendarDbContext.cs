using Calendar.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Calendar.Application.Interfaces;

/// <summary>
/// Interface to work with calendar data storage.
/// </summary>
public interface ICalendarDbContext
{
    /// <summary>
    /// Birthdays data set.
    /// </summary>
    DbSet<Birthday> Birthdays { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}