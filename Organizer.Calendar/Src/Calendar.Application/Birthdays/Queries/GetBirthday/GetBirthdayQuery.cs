using Calendar.Application.Birthdays.Models;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Calendar.Application.Birthdays.Queries.GetBirthday
{
    /// <summary>
    /// Query to get birthday by id.
    /// </summary>
    public class GetBirthdayQuery : IRequest<OneOf<BirthdayVm, NotFound>>
    {
        /// <summary>
        /// Id.
        /// </summary>
        public int Id { get; set; }
    }
}