using Contrado.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contrado.Services
{
    public class ProductAttributeLookupService : IProductAttributeLookupService
    {
        protected IProductAttributeLookupRepository _repository = null;
        
        public ProductAttributeLookupService(IProductAttributeLookupRepository repository)
        {
            _repository = repository;
        }
        public ProductAttributeLookup Get(int id)
        {
            return _repository.Get(id);
        }
        public List<ProductAttributeLookup> GetAll()
        {
            return _repository.GetAll().ToList();
        }
        public List<ProductAttributeLookup> GetByCategory(int productCategoryId)
        {
            return _repository.GetByCategory(productCategoryId);
        }
        public ProductAttributeLookup Add(ProductAttributeLookup productAttributeLookup)
        {
            return _repository.Add(productAttributeLookup);
        }
        public ProductAttributeLookup Update(ProductAttributeLookup productAttributeLookup)
        {
            return _repository.Update(productAttributeLookup);
        }
    }
    public interface IProductAttributeLookupService
    {
        ProductAttributeLookup Get(int id);
        List<ProductAttributeLookup> GetByCategory(int productCategoryId);
        ProductAttributeLookup Add(ProductAttributeLookup productAttributeLookup);
        ProductAttributeLookup Update(ProductAttributeLookup productAttributeLookup);
        List<ProductAttributeLookup> GetAll();
    }
}
