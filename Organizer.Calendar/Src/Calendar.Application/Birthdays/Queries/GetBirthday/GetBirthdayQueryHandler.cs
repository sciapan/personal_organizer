using Calendar.Application.Birthdays.Models;
using Calendar.Application.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Calendar.Application.Birthdays.Queries.GetBirthday
{
    public class GetBirthdayQueryHandler : IRequestHandler<GetBirthdayQuery, BirthdayVm?>
    {
        #region Fields

        private readonly ICalendarDbContext _dbContext;

        #endregion

        #region Ctor

        public GetBirthdayQueryHandler(ICalendarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region Methods

        public async Task<BirthdayVm?> Handle(GetBirthdayQuery request, CancellationToken cancellationToken)
        {
            var birthday = await _dbContext.Birthdays.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            return birthday?.Adapt<BirthdayVm>();
        }

        #endregion
    }
}