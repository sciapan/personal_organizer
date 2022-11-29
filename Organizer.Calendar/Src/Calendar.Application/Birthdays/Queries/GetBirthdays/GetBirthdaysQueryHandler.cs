using Calendar.Application.Birthdays.Models;
using Calendar.Application.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Calendar.Application.Birthdays.Queries.GetBirthdays
{
    internal class GetBirthdaysQueryHandler : IRequestHandler<GetBirthdaysQuery, BirthdayVm[]>
    {
        #region Fields

        private readonly ICalendarDbContext _dbContext;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the GetBirthdaysQueryHandler class.
        /// </summary>
        /// <param name="dbContext"><see cref="ICalendarDbContext"/></param>
        public GetBirthdaysQueryHandler(ICalendarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region Methods

        public Task<BirthdayVm[]> Handle(GetBirthdaysQuery request, CancellationToken cancellationToken)
        {
            return _dbContext.Birthdays.ProjectToType<BirthdayVm>().ToArrayAsync(cancellationToken);
        }

        #endregion
    }
}