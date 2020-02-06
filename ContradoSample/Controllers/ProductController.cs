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
    public class ProductController : ApiController
    {
        IProductService _productService;
        IProductAttributeService _productAttributeService;

        public ProductController(IProductService productService, IProductAttributeService productAttributeService)
        {
            _productService = productService;
            _productAttributeService = productAttributeService;
        }
        [HttpGet]
        [Route("api/Product/GetAll")]
        public ProductListModel GetAll(int page, int pageSize)
        {
            var list = _productService.GetProducts(page, pageSize);

            ProductListModel model = new ProductListModel();
            model.List = list.Results.Select(p => new ProductModel
            {
                Category = p.ProductCategory.CategoryName,
                ProductId = p.ProductId,
                ProductName = p.ProdName
            }).ToList();
            model.TotalRecords = 10;
            return model;
        }
        [HttpPost]
        [Route("api/Product/Save")]
        public void Save([FromBody]ProductModel model)
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
            if (model.ProductId == 0)
            {
                _productService.Add(product);
            }
            else
            {
                _productService.Update(product);
            }
        }
        [HttpPost]
        [Route("api/Product/Remove")]
        public void Remove([FromBody] int? productId,int? id)
        {
            _productService.Delete(productId.Value);
        }
    }
}
