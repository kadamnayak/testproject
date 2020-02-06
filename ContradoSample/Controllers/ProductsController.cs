using Contrado.DA;
using Contrado.Services;
using ContradoSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ContradoSample.Controllers
{
    public class ProductsController : ApiController
    {
        IProductService _productService;
        IProductAttributeService _productAttributeService;

        public ProductsController(IProductService productService, IProductAttributeService productAttributeService)
        {
            _productService = productService;
            _productAttributeService = productAttributeService;
        }

        public ProductListModel Get(int page, int pageSize)
        {
            var list = _productService.GetProducts(page, pageSize);

            ProductListModel model = new ProductListModel();
            model.List = list.Results.Select(p => new ProductModel
            {
                Category = p.ProductCategory.CategoryName,
                ProductId = p.ProductId,
                ProductName = p.ProdName
            }).ToList();
            model.TotalRecords = list.RowCount;
            return model;
        }

        // GET: api/Products/5
        public ProductModel Get(int id)
        {
            var product = _productService.Get(id);
            var model = new ProductModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProdName,
                Category = product.ProductCategory.CategoryName,
                CategoryId = product.ProdCatId,
                Description = product.ProdDescription
            };
            model.ProductAttributes = new List<ProductAttributeModel>();
            foreach (var attribute in product.ProductAttributes)
            {
                model.ProductAttributes.Add(new ProductAttributeModel
                {
                    AttributeId = attribute.AttributeId,
                    AttributeName = attribute.ProductAttributeLookup.AttributeName,
                    AttributeValue = attribute.AttributeValue
                });
            }
            return model;
        }

        // POST: api/Products
        public void Post([FromBody]ProductModel model)
        {
            var product = new Contrado.DA.Product
            {
                ProdDescription = model.Description,
                ProdName = model.ProductName,
                ProdCatId = model.CategoryId,
                ProductCategory = new Contrado.DA.ProductCategory { ProdCatId = model.CategoryId },
            };
            foreach (var attribute in model.ProductAttributes)
            {
                product.ProductAttributes.Add(new Contrado.DA.ProductAttribute
                {
                    AttributeId = attribute.AttributeId,
                    AttributeValue = attribute.AttributeValue
                });
            }
            _productService.Add(product);
        }

        // PUT: api/Products/5
        public void Put(int id, [FromBody]ProductModel model)
        {
            bool categoryChanged = false;
            var product = _productService.Get(id);
            product.ProdName = model.ProductName;
            product.ProdDescription = model.Description;
            if (product.ProdCatId != model.CategoryId)
            {
                categoryChanged = true;
                product.ProdCatId = model.CategoryId;
                product.ProductCategory = new Contrado.DA.ProductCategory
                {
                    ProdCatId = model.CategoryId
                };
            }
            _productService.Update(product);
            if (categoryChanged)
            {
                foreach (var attribute in product.ProductAttributes)
                {
                    _productAttributeService.Delete(product.ProductId, attribute.AttributeId);
                }
            }
            var attributeToDelete = product.ProductAttributes.Where(p => !model.ProductAttributes.Any(p2 => p2.AttributeId == p.AttributeId));
            foreach(var attribute in attributeToDelete)
            {
                _productAttributeService.Delete(product.ProductId, attribute.AttributeId);
            }
            foreach (var attribute in model.ProductAttributes)
            {
                var productAttribute = product.ProductAttributes.Where(p => p.AttributeId == attribute.AttributeId).FirstOrDefault();
                if (productAttribute == null)
                {
                    _productAttributeService.Add(new Contrado.DA.ProductAttribute
                    {
                        AttributeId = attribute.AttributeId,
                        AttributeValue = attribute.AttributeValue,
                        ProductId = model.ProductId,
                        ProductAttributeLookup = new ProductAttributeLookup { AttributeId = attribute.AttributeId },
                        Product = new Product { ProductId = model.ProductId }
                    });
                }
                else
                {
                    productAttribute.AttributeValue = attribute.AttributeValue;
                    _productAttributeService.Update(productAttribute);
                }
            }
        }

        // DELETE: api/Products/5
        public void Delete(int id)
        {
            _productService.Delete(id); 
        }

        [HttpPost]
        [Route("api/Products/DeleteProd")]
        public void DeleteProd([FromBody]int productId)
        {
            _productService.Delete(productId);
        }
    }
}
