using Calendar.Application.Contracts;
using Calendar.Application.Interfaces;
using Mapster;
using MassTransit;
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

        private readonly IBus _bus;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the DeleteBirthdayCommandHandler class.
        /// </summary>
        /// <param name="dbContext"><see cref="ICalendarDbContext"/></param>
        /// <param name="bus"><see cref="IBus"/></param>
        public DeleteBirthdayCommandHandler(ICalendarDbContext dbContext, IBus bus)
        {
            _dbContext = dbContext;
            _bus = bus;
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

            await _bus.Publish(birthday.Adapt<BirthdayDeleted>());

            return Unit.Value;
        }

        #endregion
    }
}