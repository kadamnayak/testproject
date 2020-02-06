using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contrado.DA
{
    public class ProductCategoryRepository : BaseRepository<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(ECommerceDemoEntities context) : base()
        {
        }
        public ProductCategory Get(int id)
        {
            return _dbContext.ProductCategories.SingleOrDefault(p => p.ProdCatId== id);
        }
        public List<ProductCategory> GetAllCategory()
        {
            return _dbContext.ProductCategories.ToList();
        }
    }
    public interface IProductCategoryRepository:IBaseRepository<ProductCategory>
    {
        List<ProductCategory> GetAllCategory();
        ProductCategory Get(int id);
    }
}
