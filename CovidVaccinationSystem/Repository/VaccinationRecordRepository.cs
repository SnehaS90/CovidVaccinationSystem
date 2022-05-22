using CovidVaccinationSystem.Data;
using CovidVaccinationSystem.Model;
using Microsoft.EntityFrameworkCore;

namespace CovidVaccinationSystem.Repository
{
    public class VaccinationRecordRepository : GenericRepository<VaccinationRecord>, IVaccinationRecordRepository
    {
        public VaccinationRecordRepository(DBContext context, ILogger logger) : base(context, logger)
        {
        }

        public override async Task<IQueryable<VaccinationRecord>> All()
        {
            try
            {
                return dbSet.Include(v => v.PatientDtl);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "All function error", typeof(VaccinationRecordRepository));
                return new List<VaccinationRecord>().AsQueryable(); ;
            }
        }

        public override async Task<VaccinationRecord> GetById(int id)
        {
            try
            {
                //return await dbSet.FindAsync(id);
                return dbSet.Include(v => v.PatientDtl).Where(v => v.VaccinationId == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetById function error", typeof(VaccinationRecordRepository));
                return new VaccinationRecord();
            }
        }

        public override async Task<bool> Add(VaccinationRecord entity)
        {
            try
            {
                var record = await dbSet.Include(v => v.PatientDtl).Where(v => v.VaccinationId == entity.VaccinationId)
                                                    .FirstOrDefaultAsync();


                if (record == null)
                    await dbSet.AddAsync(entity);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Add function error", typeof(VaccinationRecordRepository));
                return false;
            }
        }


        public override async Task<bool> Update(VaccinationRecord entity)
        {
            try
            {
                var record = await dbSet.Include(v => v.PatientDtl).Where(p => p.VaccinationId == entity.VaccinationId)
                                                    .FirstOrDefaultAsync();


                if (record == null)
                    return false;

                dbSet.Update(record);

                context.Entry(entity).State = EntityState.Modified;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Update function error", typeof(VaccinationRecordRepository));
                return false;
            }
        }

        public override async Task<bool> Delete(int id)
        {
            try
            {
                var VaccinationRecord = await dbSet.Where(p => p.VaccinationId == id)
                                        .FirstOrDefaultAsync();

                if (VaccinationRecord == null) return false;

                dbSet.Remove(VaccinationRecord);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "" +
                    "Delete function error", typeof(VaccinationRecordRepository));
                return false;
            }
        }
    }
}
