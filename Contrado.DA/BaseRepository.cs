using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contrado.DA
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected ECommerceDemoEntities _dbContext;
        protected DbSet<T> entities;
        public BaseRepository()
        {
            _dbContext = new ECommerceDemoEntities();
            entities = _dbContext.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }
        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            _dbContext.SaveChanges();
        }
        public virtual void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _dbContext.SaveChanges();
        }
        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            _dbContext.SaveChanges();
        }
    }
    public interface IBaseRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
