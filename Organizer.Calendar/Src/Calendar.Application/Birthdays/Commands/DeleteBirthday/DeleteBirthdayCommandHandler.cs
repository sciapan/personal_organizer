using Calendar.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;

namespace Calendar.Application.Birthdays.Commands.DeleteBirthday
{
    public class DeleteBirthdayCommandHandler : IRequestHandler<DeleteBirthdayCommand, OneOf<Unit, NotFound>>
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

        public async Task<OneOf<Unit, NotFound>> Handle(DeleteBirthdayCommand request, CancellationToken cancellationToken)
        {
            var birthday = await _dbContext.Birthdays.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (birthday == null)
            {
                return new NotFound();
            }

            _dbContext.Birthdays.Remove(birthday);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        #endregion
    }
}