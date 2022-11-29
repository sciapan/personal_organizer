using Calendar.Application.Birthdays.Models;
using Calendar.Application.Interfaces;
using Calendar.Domain.Entities;
using Mapster;
using MediatR;

namespace Calendar.Application.Birthdays.Commands.CreateBirthday
{
    public class CreateBirthdayCommandHandler : IRequestHandler<CreateBirthdayCommand, BirthdayVm>
    {
        #region Fields

        private readonly ICalendarDbContext _dbContext;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the CreateBirthdayCommandHandler class.
        /// </summary>
        /// <param name="dbContext"><see cref="ICalendarDbContext"/></param>
        public CreateBirthdayCommandHandler(ICalendarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region Methods

        public async Task<BirthdayVm> Handle(CreateBirthdayCommand request, CancellationToken cancellationToken)
        {
            var birthday = request.Adapt<Birthday>();

            birthday.Dob = birthday.Dob.ToUniversalTime();

            _dbContext.Birthdays.Add(birthday);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return birthday.Adapt<BirthdayVm>();
        }

        #endregion
    }
}