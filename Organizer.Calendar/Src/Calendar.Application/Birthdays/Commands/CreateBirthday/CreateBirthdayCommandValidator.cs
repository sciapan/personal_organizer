using Calendar.Application.Interfaces;
using FluentValidation;

namespace Calendar.Application.Birthdays.Commands.CreateBirthday
{
    public class CreateBirthdayCommandValidator : AbstractValidator<CreateBirthdayCommand>
    {
        public CreateBirthdayCommandValidator(IMachineTime machineTime)
        {
            RuleFor(x => x.Dob).LessThanOrEqualTo(machineTime.Now).NotEmpty();
            RuleFor(x => x.Person).NotEmpty();
        }
    }
}