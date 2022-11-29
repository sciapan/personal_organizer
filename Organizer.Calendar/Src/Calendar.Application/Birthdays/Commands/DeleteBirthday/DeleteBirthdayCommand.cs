using Calendar.Application.Birthdays.Models;
using MediatR;

namespace Calendar.Application.Birthdays.Commands.DeleteBirthday
{
    public class DeleteBirthdayCommand : IRequest<BirthdayVm?>
    {
        /// <summary>
        /// Birthday Id.
        /// </summary>
        public int Id { get; set; }
    }
}