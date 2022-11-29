namespace Calendar.Application.Birthdays.Models
{
    public class BirthdayVm
    {
        /// <summary>
        /// Unique id of record.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Date of birth.
        /// </summary>
        public DateTimeOffset Dob { get; set; }

        /// <summary>
        /// Person name.
        /// </summary>
        public string Person { get; set; }

        /// <summary>
        /// Additional notes.
        /// </summary>
        public string? Notes { get; set; }
    }
}