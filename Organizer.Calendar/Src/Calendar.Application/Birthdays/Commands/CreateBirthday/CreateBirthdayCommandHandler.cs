using Calendar.Application.Behaviors;
using Calendar.Application.Birthdays.Models;
using Calendar.Application.Interfaces;
using Calendar.Domain.Entities;
using Mapster;
using MediatR;
using OneOf;

namespace Calendar.Application.Birthdays.Commands.CreateBirthday
{
    public class CreateBirthdayCommandHandler : IRequestHandler<CreateBirthdayCommand, OneOf<BirthdayVm, ValidationFailed>>
    {
        #region Fields

        private readonly ICalendarDbContext _dbContext;

        private readonly CreateBirthdayCommandValidator _validator;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the CreateBirthdayCommandHandler class.
        /// </summary>
        /// <param name="dbContext"><see cref="ICalendarDbContext"/></param>
        /// <param name="validator"><see cref="CreateBirthdayCommandValidator"/></param>
        public CreateBirthdayCommandHandler(ICalendarDbContext dbContext, CreateBirthdayCommandValidator validator)
        {
            _dbContext = dbContext;
            _validator = validator;
        }

        #endregion

        #region Methods

        public async Task<OneOf<BirthdayVm, ValidationFailed>> Handle(CreateBirthdayCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new ValidationFailed(validationResult.ToDictionary());
            }

            var birthday = request.Adapt<Birthday>();

            birthday.Dob = birthday.Dob.ToUniversalTime();

            _dbContext.Birthdays.Add(birthday);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return birthday.Adapt<BirthdayVm>();
        }

        #endregion
    }
}