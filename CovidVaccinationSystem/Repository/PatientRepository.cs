using CovidVaccinationSystem.Data;
using CovidVaccinationSystem.Model;
using Microsoft.EntityFrameworkCore;

namespace CovidVaccinationSystem.Repository
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        public PatientRepository(DBContext context, ILogger logger) : base(context, logger)
        {
        }

        public override async Task<IQueryable<Patient>> All()
        {
            try
            {
                return dbSet.Include(p => p.VaccinationRecords);                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "All function error", typeof(PatientRepository));
                return new List<Patient>().AsQueryable(); 
            }
        }

        public override async Task<Patient> GetById(int id)
        {
            try
            {
                return dbSet.Include(p => p.VaccinationRecords).Where(p => p.PatientId == id).FirstOrDefault();
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetById function error", typeof(PatientRepository));
                return new Patient();
            }
        }

        public override async Task<bool> Add(Patient entity)
        {
            try
            {
                var existingPatient = await dbSet.Where(x => x.PatientId == entity.PatientId)
                                                    .FirstOrDefaultAsync();

                if (existingPatient == null)
                    await dbSet.AddAsync(entity);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Add function error", typeof(PatientRepository));
                return false;
            }
        }


        public override async Task<bool> Update(Patient entity)
        {
            try
            {
                var existingPatient = await dbSet.Include(p => p.VaccinationRecords).Where(x => x.PatientId == entity.PatientId)
                                                    .FirstOrDefaultAsync();


                if (existingPatient == null)
                    return false;

                dbSet.Update(existingPatient);               

                context.Entry(entity).State = EntityState.Modified;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Update function error", typeof(PatientRepository));
                return false;
            }
        }

        public override async Task<bool> Delete(int id)
        {
            try
            {
                var patient = await dbSet.Include(p => p.VaccinationRecords).Where(p => p.PatientId == id)
                                        .FirstOrDefaultAsync();

                if (patient == null) return false;

                dbSet.Remove(patient);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "" +
                    "Delete function error", typeof(PatientRepository));
                return false;
            }
        }
    }
}
