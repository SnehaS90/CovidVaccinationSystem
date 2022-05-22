using CovidVaccinationSystem.Model;
using FluentValidation;

namespace CovidVaccinationSystem.FluentValidations
{
    public class PatientValidator : AbstractValidator<Patient>
    {
        public PatientValidator()
        {
            RuleFor(patient => patient.FirstName).NotNull().NotEmpty().Length(3, 50).WithMessage("FirstName Should not empty");            
            RuleFor(patient => patient.LastName).NotNull().NotEmpty().Length(3, 50).WithMessage("LastName should not empty");
            RuleFor(patient => patient.DOB).NotNull().Must(ValidAge).WithMessage("Invalid DOB");
            RuleFor(patient => patient.Gender).NotNull().NotEmpty().WithMessage("Gender should not empty");
            RuleFor(patient => patient.ContactNo).NotNull().Matches("^(?:\\+971|00971|0)(?:2|3|4|6|7|9|50|51|52|55|56)[0-9]{7}$");
            RuleFor(patient => patient.Email).EmailAddress().WithMessage("Email not Valid");
            RuleFor(patient => patient.Nationality).NotNull().WithMessage("Nationality should not empty");
            RuleFor(patient => patient.PassportNo).NotNull().WithMessage("PassportNo should not empty");
            RuleFor(patient => patient.EmiratesId).NotNull().Matches(@"^784-[0-9]{4}-[0-9]{7}-[0-9]{1}$").WithMessage("Invalid Emirates Id ");
            RuleFor(patient => patient.EmiratesId).NotNull().WithMessage("EmiratesId should not empty");

        }

        //Method to Check DOB Must not be greater than todays date
        protected bool ValidAge(DateTime date)
        {
            DateTime today = DateTime.Now;

            if (date < today)
            {
                return true;
            }

            return false;
        }
    }
}
