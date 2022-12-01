using Calendar.Application.Birthdays.Models;
using MediatR;

namespace Calendar.Application.Birthdays.Queries.GetBirthday
{
    /// <summary>
    /// Query to get birthday by id.
    /// </summary>
    public class GetBirthdayQuery : IRequest<BirthdayVm?>
    {
        /// <summary>
        /// Id.
        /// </summary>
        public int Id { get; set; }
    }
}