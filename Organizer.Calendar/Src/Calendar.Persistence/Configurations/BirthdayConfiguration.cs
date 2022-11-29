using Calendar.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Calendar.Persistence.Configurations;

internal class BirthdayConfiguration : IEntityTypeConfiguration<Birthday>
{
    public void Configure(EntityTypeBuilder<Birthday> builder)
    {
        // table name
        builder.ToTable("birthdays");

        // primary key
        builder.HasKey(x => x.Id).HasName("pk_birthdays");

        // properties
        builder.Property(x => x.Person).HasMaxLength(64).IsRequired();

        builder.Property(x => x.Notes).HasMaxLength(128);
    }
}