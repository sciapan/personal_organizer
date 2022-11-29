using Calendar.Application.Birthdays.Models;
using MediatR;

namespace Calendar.Application.Birthdays.Commands.CreateBirthday
{
    /// <summary>
    /// Command to create birthday.
    /// </summary>
    public class CreateBirthdayCommand : IRequest<BirthdayVm>
    {
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