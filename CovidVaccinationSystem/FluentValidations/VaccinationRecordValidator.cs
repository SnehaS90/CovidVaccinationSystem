using CovidVaccinationSystem.Model;
using FluentValidation;

namespace CovidVaccinationSystem.FluentValidations
{
    public class VaccinationRecordValidator : AbstractValidator<VaccinationRecord>
    {
        public VaccinationRecordValidator()
        {
            RuleFor(vaccination => vaccination.VaccinationName).NotNull().NotEmpty().Length(3, 50);
            RuleFor(vaccination => vaccination.VaccinationCentre).NotNull().NotEmpty().Length(3, 50);
            RuleFor(vaccination => vaccination.Dose).NotNull().NotEmpty();            
            RuleFor(patient => patient.VaccinationDate).NotNull().Must(ValidAge);

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
