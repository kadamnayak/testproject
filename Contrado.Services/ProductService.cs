using Contrado.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contrado.Services
{
    public class ProductService : IProductService
    {
        protected IProductRepository _repository;
        protected IProductAttributeRepository _productAttributeRepository;
        protected IProductAttributeLookupRepository _productAttributeLookupRepository;
        
        public ProductService(IProductRepository repository, IProductAttributeRepository productAttributeRepository, IProductAttributeLookupRepository productAttributeLookupRepository)
        {
            _repository = repository;
            _productAttributeRepository = productAttributeRepository;
            _productAttributeLookupRepository = productAttributeLookupRepository;
        }
        public void GetAll()
        {
            var data = _repository.GetAll();
        }
        public Product Get(int id)
        {
            return _repository.Get(id);
        }
        public PagedResult<Product> GetProducts(int page, int pageSize)
        {
            return _repository.GetAllProducts(page, pageSize);
        }
        public void Add(Product product)
        {
            var prod = new Product
            {
                ProdCatId = product.ProdCatId,
                ProdDescription = product.ProdDescription,
                ProdName = product.ProdName,
                ProductCategory = new ProductCategory
                {
                    ProdCatId = product.ProdCatId
                }
            };
            _repository.Add(prod);

            foreach (var attribute in product.ProductAttributes)
            {
                attribute.ProductId = prod.ProductId;
                attribute.ProductAttributeLookup = new ProductAttributeLookup { AttributeId = attribute.AttributeId };
                attribute.Product = new Product{ ProductId= attribute.ProductId};
                _productAttributeRepository.Add(attribute);
            }
        }
        public void Update(Product product)
        {
            _repository.Update(product);
        }
        public void Delete(long id)
        {
            var attributes = _productAttributeRepository.GetByProduct(id);
            foreach(var attr in attributes)
            {
                _productAttributeRepository.Delete(attr);
            }
            var product = _repository.Get(id);
            _repository.Delete(product);
        }
    }
    public interface IProductService
    {
        void GetAll();
        PagedResult<Product> GetProducts(int page, int pageSize);
        Product Get(int id);
        void Add(Product product);
        void Update(Product product);
        void Delete(long id);
    }
}
