using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using DAL;
using DataModel;
using UI.Helper;

namespace UI.Controllers
{
    public class ShopController : Controller
    {
      
        CategoryRepository CategoryRepo = new CategoryRepository();       

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Favorate()
        {

            return View();
        }
       
        public JsonResult LoadAllCategories()
        {
            IEnumerable<Category> Categories = CategoryRepo.GetAllCategories();
            return Json(Categories.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AllProducts()
        {
            return View();
        }

        public ActionResult Product(int? id)
        {
            return View();
        }
         
        public ActionResult Cart()
        {
            return View();
        }
   
    }
}