namespace MessagingService.Repository
{
    public interface IRepository<T> where T : class
    {
        Task <T> GetById(int? id);
        Task <T> GetByName(string? name);
        Task<IList<T>> GetList();
        Task Register(T entity);
        Task Delete(int id);
        Task Update(T updatedEntity, int id);
    }
}
