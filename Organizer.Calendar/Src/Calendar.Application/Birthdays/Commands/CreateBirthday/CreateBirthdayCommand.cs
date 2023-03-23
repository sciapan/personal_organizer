using Calendar.Application.Behaviors;
using Calendar.Application.Birthdays.Models;
using MediatR;
using OneOf;

namespace Calendar.Application.Birthdays.Commands.CreateBirthday
{
    /// <summary>
    /// Command to create birthday.
    /// </summary>
    public class CreateBirthdayCommand : IRequest<OneOf<BirthdayVm, ValidationFailed>>
    {
        /// <summary>
        /// Date of birth.
        /// </summary>
        public required DateTimeOffset Dob { get; set; }

        /// <summary>
        /// Person name.
        /// </summary>
        public required string Person { get; set; }

        /// <summary>
        /// Additional notes.
        /// </summary>
        public string? Notes { get; set; }
    }
}