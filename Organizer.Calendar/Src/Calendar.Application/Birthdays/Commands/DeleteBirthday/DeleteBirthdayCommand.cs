using MediatR;
using OneOf;
using OneOf.Types;

namespace Calendar.Application.Birthdays.Commands.DeleteBirthday
{
    public class DeleteBirthdayCommand : IRequest<OneOf<Unit, NotFound>>
    {
        /// <summary>
        /// Birthday Id.
        /// </summary>
        public int Id { get; set; }
    }
}