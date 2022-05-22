using CovidVaccinationSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace CovidVaccinationSystem.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected DBContext context;
        internal DbSet<T> dbSet;
        protected readonly ILogger _logger;

        public GenericRepository(DBContext context, ILogger logger)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
            this._logger = logger;
        }

        public virtual Task<IQueryable<T>> All()
        {
            throw new NotImplementedException();
        }

        public virtual async Task<T> GetById(int id)
        {            
            throw new NotImplementedException();
        }

        public virtual async Task<bool> Add(T entity)
        {           
            throw new NotImplementedException();
        }

        public virtual async Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<bool> Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
