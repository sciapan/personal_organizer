using Calendar.Application.Birthdays.Models;
using Calendar.Application.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Calendar.Application.Birthdays.Commands.DeleteBirthday
{
    public class DeleteBirthdayCommandHandler : IRequestHandler<DeleteBirthdayCommand, BirthdayVm?>
    {
        #region Fields

        private readonly ICalendarDbContext _dbContext;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the DeleteBirthdayCommandHandler class.
        /// </summary>
        /// <param name="dbContext"><see cref="ICalendarDbContext"/></param>
        public DeleteBirthdayCommandHandler(ICalendarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region Methods

        public async Task<BirthdayVm?> Handle(DeleteBirthdayCommand request, CancellationToken cancellationToken)
        {
            var birthday = await _dbContext.Birthdays.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (birthday == null)
            {
                return null;
            }

            _dbContext.Birthdays.Remove(birthday);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return birthday.Adapt<BirthdayVm>();

        }

        #endregion
    }
}