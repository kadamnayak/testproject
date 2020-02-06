using Contrado.Services;
using ContradoSample.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ContradoSample.Controllers
{
    public class CommonController : ApiController
    {
        readonly IProductCategoryService _productCategoryService;
        readonly IProductAttributeLookupService _productAttributeLookupService;
        readonly IProductAttributeService _productAttributeService;

        public CommonController(IProductCategoryService productCategoryService, IProductAttributeLookupService productAttributeLookupService, IProductAttributeService productAttributeService)
        {
            _productCategoryService = productCategoryService;
            _productAttributeLookupService = productAttributeLookupService;
            _productAttributeService = productAttributeService;
        }
        [HttpGet]
        [Route("api/Common/Categories")]
        public List<CategoryModel> Categories()
        {
            return _productCategoryService.GetAllCategory().Select(p => new CategoryModel
            {
                CategoryName = p.CategoryName,
                CategoryId = p.ProdCatId
            }).ToList();
        }
        [HttpGet]
        [Route("GetAttributes")]
        public List<AttributeModel> GetAttributes(int categoryId)
        {
            return _productAttributeLookupService.GetByCategory(categoryId).Select(p => new AttributeModel
            {
                AttributeId = p.AttributeId,
                CategoryId = p.ProdCatId,
                AttributeName = p.AttributeName
            }).ToList();
        }
        [HttpGet]
        [ActionName("GetAllAttributes")]
        public List<AttributeModel> GetAllAttributes()
        {
            return _productAttributeLookupService.GetAll().Select(p => new AttributeModel
            {
                AttributeId = p.AttributeId,
                CategoryId = p.ProdCatId,
                AttributeName = p.AttributeName
            }).ToList();
        }
    }
}
