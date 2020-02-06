using Contrado.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contrado.Services
{
    public class ProductCategoryService: IProductCategoryService
    {
        private ProductCategoryRepository _repository = null;

        public ProductCategoryService(ProductCategoryRepository repository)
        {
            _repository = repository;
        }
        public List<ProductCategory> GetAllCategory()
        {
            return _repository.GetAllCategory();
        }
        public ProductCategory Get(int id)
        {
            return _repository.Get(id);
        }
    }
    public interface IProductCategoryService
    {
        List<ProductCategory> GetAllCategory();
        ProductCategory Get(int id);
    }
}
