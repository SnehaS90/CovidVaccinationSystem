using CovidVaccinationSystem.Repository;

namespace CovidVaccinationSystem.UnitConfiguration
{
    //wrap operations into one unit as transcation
    public interface IUnitOfWork
    {
        IPatientRepository Patient { get; }
        IVaccinationRecordRepository VaccinationRecord { get; }
        Task CompleteAsync();
        void Dispose();      
    }
}
