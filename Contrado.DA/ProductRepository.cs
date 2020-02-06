using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contrado.DA
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ECommerceDemoEntities context) : base()
        {
        }
        public Product Get(long id)
        {
            return _dbContext.Products.SingleOrDefault(p => p.ProductId == id);
        }
        public PagedResult<Product> GetAllProducts(int page, int pageSize)
        {
            return _dbContext.Products.OrderBy(p=>p.ProdName).GetPaged(page, pageSize);
        }
        public Product Add(Product product)
        {
            product = _dbContext.Products.Add(product);
            _dbContext.Entry(product.ProductCategory).State = EntityState.Unchanged;
            _dbContext.SaveChanges();
            return product;
        }
        public override void Update(Product product)
        {
            _dbContext.Entry(product).State = System.Data.Entity.EntityState.Modified;
            _dbContext.Entry(product.ProductCategory).State = EntityState.Unchanged;
            _dbContext.SaveChanges();
        }
    }
    public interface IProductRepository : IBaseRepository<Product>
    {
        PagedResult<Product> GetAllProducts(int page, int pageSize);
        Product Get(long id);
        Product Add(Product product);
        void Delete(Product product);
    }
}
