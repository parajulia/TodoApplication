namespace TodoApplication.Repository
{
    public interface IRepositoryBase<T>
    {
        Task<List<T>> FindAll();

        T GetById(object id);

        void Create(T entity);
        void Update(T entity);
        void Delete(object id);
    }
}
