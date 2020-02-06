using Contrado.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contrado.Services
{
    public class ProductAttributeService : IProductAttributeService
    {
        protected IProductAttributeRepository _repository = null;
        
        public ProductAttributeService(IProductAttributeRepository repository)
        {
            _repository = repository;
        }
        public ProductAttribute Get(long productId, int attributeId)
        {
            return _repository.Get(productId, attributeId);
        }
        public List<ProductAttribute> GetByProduct(int productId)
        {
            return _repository.GetByProduct(productId);
        }
        public List<ProductAttribute> GetByAttribute(int attributeId)
        {
            return _repository.GetByAttribute(attributeId);
        }
        public void Delete(long productId, int attributeId)
        {
            _repository.Delete(productId, attributeId);
        }
        public void Add(ProductAttribute productAttribute)
        {
            _repository.Add(productAttribute);
        }
        public void Update(ProductAttribute productAttribute)
        {
            _repository.Update(productAttribute);
        }
    }
    public interface IProductAttributeService
    {
        ProductAttribute Get(long productId, int attributeId);
        List<ProductAttribute> GetByProduct(int productId);
        List<ProductAttribute> GetByAttribute(int attributeId);
        void Delete(long productId, int attributeId);
        void Add(ProductAttribute productAttribute);
        void Update(ProductAttribute productAttribute);
    }
}
