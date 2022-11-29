using Calendar.Application.Birthdays.Models;
using MediatR;

namespace Calendar.Application.Birthdays.Queries.GetBirthdays
{
    /// <summary>
    /// Query to get birthdays.
    /// </summary>
    public class GetBirthdaysQuery : IRequest<BirthdayVm[]>
    {
    }
}