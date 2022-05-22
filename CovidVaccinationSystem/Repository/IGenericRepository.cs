namespace CovidVaccinationSystem.Repository
{
    //generalized repository pattern
    public interface IGenericRepository<T> where T : class
    {
        Task<IQueryable<T>> All();
        Task<T> GetById(int id);
        Task<bool> Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(int id);
    }
}
