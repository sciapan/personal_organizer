using Calendar.Application.Interfaces;
using Calendar.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Calendar.Persistence;

public class CalendarDbContext : DbContext, ICalendarDbContext
{
    public CalendarDbContext(DbContextOptions<CalendarDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public required DbSet<Birthday> Birthdays { get; set; }
}