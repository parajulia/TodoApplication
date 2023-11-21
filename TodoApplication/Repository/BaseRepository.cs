using Microsoft.EntityFrameworkCore;
using TodoApplication.Infrastructure;

namespace TodoApplication.Repository
{
    public class BaseRepository<T> : IRepositoryBase<T> where T : class
    {
        internal DataContext dataContext;
        internal DbSet<T> dbSet;
        public BaseRepository(DataContext dataContext)
        {
            this.dataContext = dataContext; 
            this.dbSet = dataContext.Set<T>();
        }

        public  void Create(T entity)
        {
            this.dbSet.Add(entity);
             dataContext.SaveChangesAsync();
        }

        public   void Delete(object id)
        {
            T dataToDelete = dbSet.Find(id);
             Delete(dataToDelete);
        }

        public async Task<T> Delete(T entity) { 
            if(dataContext.Entry(entity).State == EntityState.Detached) {
                dbSet.Attach(entity);
            }

            dbSet.Remove(entity);
            await dataContext.SaveChangesAsync();
            return entity;
        }

        public async Task<List<T>> FindAll()
        {
            IQueryable<T> query = dbSet;
           return await query.ToListAsync();
        }

        public  T GetById(object id)
        {
            IQueryable<T> query= dbSet;

            return dbSet.Find(id);
        }

        public void Update(T entity)
        {

            dbSet.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
             dataContext.SaveChangesAsync();

        }
    }
    
}
