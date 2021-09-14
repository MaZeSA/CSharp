using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EntityForm.DAL.Repository
{
    public class EFRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbContext context;
        private readonly DbSet<TEntity> set;

        public EFRepository(DbContext _context)
        {
            context = _context;
            set = context.Set<TEntity>();
        }
      
        public void Create(TEntity entity)
        {
           // set.Add(entity);
            context.Entry(entity).State = EntityState.Added;
            context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
           // set.Remove(entity);
            context.Entry(entity).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return set.AsEnumerable();
        }

        public void Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
           
            context.SaveChanges();
        }
    }
}
