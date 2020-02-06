using Contrado.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContradoSample.Controllers
{
    public class HomeController : Controller
    {
        //IProductCategoryService _productCategoryService;
        //IProductService _productService;
        public HomeController(IProductCategoryService productCategoryService, IProductService productService)
        {
            //_productCategoryService = productCategoryService;
            //_productService = productService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(int id = 0)
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}