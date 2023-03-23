using Calendar.Application.Behaviors;
using Calendar.Application.Birthdays.Models;
using Calendar.Application.Contracts;
using Calendar.Application.Interfaces;
using Calendar.Domain.Entities;
using Mapster;
using MassTransit;
using MediatR;
using OneOf;

namespace Calendar.Application.Birthdays.Commands.CreateBirthday
{
    public class CreateBirthdayCommandHandler : IRequestHandler<CreateBirthdayCommand, OneOf<BirthdayVm, ValidationFailed>>
    {
        #region Fields

        private readonly ICalendarDbContext _dbContext;

        private readonly CreateBirthdayCommandValidator _validator;

        private readonly IBus _bus;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the CreateBirthdayCommandHandler class.
        /// </summary>
        /// <param name="dbContext"><see cref="ICalendarDbContext"/></param>
        /// <param name="validator"><see cref="CreateBirthdayCommandValidator"/></param>
        /// <param name="bus"><see cref="IBus"/></param>
        public CreateBirthdayCommandHandler(ICalendarDbContext dbContext, CreateBirthdayCommandValidator validator, IBus bus)
        {
            _dbContext = dbContext;
            _validator = validator;
            _bus = bus;
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

            await _bus.Publish(birthday.Adapt<BirthdayCreated>());

            return birthday.Adapt<BirthdayVm>();
        }

        #endregion
    }
}