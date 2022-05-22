using CovidVaccinationSystem.Data;
using CovidVaccinationSystem.FluentValidations;
using CovidVaccinationSystem.Repository;

namespace CovidVaccinationSystem.UnitConfiguration
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DBContext _context;
        private readonly ILogger _logger;

        public IPatientRepository Patient { get; private set; }
        public IVaccinationRecordRepository VaccinationRecord { get; private set; }

        public PatientValidator patientValidator;
        public VaccinationRecordValidator vaccinationValidator;

        //constructor
        public UnitOfWork(DBContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");

            Patient = new PatientRepository(context, _logger);
            VaccinationRecord = new VaccinationRecordRepository(context, _logger);
        }

        //save changes of DBSet to database table 
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        //release unmanaged resources
        public void Dispose()
        {
            _context.Dispose();
        }
        
    }
}
