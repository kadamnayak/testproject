using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contrado.DA
{
    public class ProductAttributeLookupRepository : BaseRepository<ProductAttributeLookup>, IProductAttributeLookupRepository
    {
        public ProductAttributeLookupRepository(ECommerceDemoEntities context) : base()
        {
        }
        public ProductAttributeLookup Get(int id)
        {
            return _dbContext.ProductAttributeLookups.SingleOrDefault(p => p.AttributeId == id);
        }
        public List<ProductAttributeLookup> GetByCategory(int productCategoryId)
        {
            return _dbContext.ProductAttributeLookups.Where(p=>p.ProdCatId== productCategoryId).ToList();
        }
        public ProductAttributeLookup Add(ProductAttributeLookup productAttributeLookup)
        {
            productAttributeLookup = _dbContext.ProductAttributeLookups.Add(productAttributeLookup);
            _dbContext.SaveChanges();
            return productAttributeLookup;
        }
        public ProductAttributeLookup Update(ProductAttributeLookup productAttributeLookup)
        {
            _dbContext.Entry(productAttributeLookup).State = System.Data.Entity.EntityState.Modified;
            _dbContext.SaveChanges();
            return productAttributeLookup;
        }
    }
    public interface IProductAttributeLookupRepository : IBaseRepository<ProductAttributeLookup>
    {
        ProductAttributeLookup Get(int id);
        List<ProductAttributeLookup> GetByCategory(int productCategoryId);
        ProductAttributeLookup Update(ProductAttributeLookup productAttributeLookup);
        ProductAttributeLookup Add(ProductAttributeLookup productAttributeLookup);
    }
}
