using FluentValidation;

namespace Calendar.Application.Birthdays.Commands.CreateBirthday
{
    public class CreateBirthdayCommandValidator : AbstractValidator<CreateBirthdayCommand>
    {
        public CreateBirthdayCommandValidator()
        {
            RuleFor(x => x.Dob).NotEmpty();
            RuleFor(x => x.Person).NotEmpty();
        }
    }
}