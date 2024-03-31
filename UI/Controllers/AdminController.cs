using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using DAL;
using DataModel;
using System.IO;
using System.Net;

namespace UI.Controllers
{
    public class AdminController : Controller
    {
        ProductRepository prodrepo = new ProductRepository();
        CategoryRepository catrepo = new CategoryRepository();
        EcommerceEntities db = new EcommerceEntities();

        public ActionResult Index()
        {
            if (Session["AdminID"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Shop");
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Products()
        {
            try
            {
                if (Session["AdminID"] != null)
                {
                    ViewBag.categories = catrepo.GetAllCategories();
                    return View();
                }
                return RedirectToAction("Index", "Shop");
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public ActionResult Categories()
        {
            if (Session["AdminID"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Shop");
        }

        public ActionResult Orders()
        {
            if (Session["AdminID"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Shop");
        }

        public JsonResult AdminLogin(Customer data)
        {
            try
            {
                var customer = db.Customers.Where(x => x.email == data.email && x.password == data.password).FirstOrDefault();
                if (customer != null)
                {
                    Session["AdminID"] = customer.customerID;
                    return Json("Success", JsonRequestBehavior.AllowGet);
                }
                else
                    return Json("erorr", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public void AddProduct(ProductVM product, HttpPostedFileBase file)
        {
         
            try
            {

                if (file != null && file.ContentLength > 0)
                {

                    // save img name in databsae 
                    string imageName = Path.GetFileName(file.FileName);
                    product.img = imageName;
                    product.rank = 0;
                    prodrepo.AddProduct(product);

                    //save img in users folder
                    string path = Path.Combine(Server.MapPath("~/Images/Products"), Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                }
                else
                {
                    ViewBag.Message = "There Is No File";
                }
            }
            catch (System.Exception)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                HttpContext.Response.ContentType = "text/plain";
                HttpContext.Response.Write("لم يتم الاضافة");
            }
        }

        [HttpPost]
        public void UpdateProduct(Product product,int id, HttpPostedFileBase file)
        {

            try
            {
                Product item =db.Products.FirstOrDefault(x=>x.productID == id);
                if (file != null && file.ContentLength > 0)
                {

                    // save img name in databsae 
                    string imageName = Path.GetFileName(file.FileName);
                    item.img = imageName;
                   
                    //save img in users folder
                    string path = Path.Combine(Server.MapPath("~/Images/Products"), Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                }
                else
                {
                    ViewBag.Message = "There Is No File";
                }
                item.prodName = product.prodName;
                item.prodDescription = product.prodDescription;
                item.price = product.price;
                item.discount = product.discount;
                item.inStock = product.inStock;
                item.isFeature = product.isFeature;
                item.categoryID = product.categoryID;
                db.SaveChanges();
            }
            catch (System.Exception)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                HttpContext.Response.ContentType = "text/plain";
                HttpContext.Response.Write("لم يتم التعديل");
            }
        }


        ///// delete product
        [HttpPost]
        public void Delete(int id)
        {
            try
            {
                ProductVM item = prodrepo.GetProductByID(id);
                if (prodrepo.DeleteProduct(id))
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Images/Products" + item.img)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Images/Products" + item.img));
                    }

                }
              
            }
            catch (System.Exception)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                HttpContext.Response.ContentType = "text/plain";
                HttpContext.Response.Write("لم يتم الحذف");
            }
        }


        public JsonResult GetAllCategories()
        {
            try
            {
                IEnumerable<Category> cats = catrepo.GetAllCategories();
                return Json(cats.ToList().OrderByDescending(x => x.categoryID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public JsonResult GetCategoryByID(int id)
        {
            try
            {
                return Json(db.Categories.Select(x => new { categoryID = x.categoryID, categoryName = x.categoryName, categoryDescription = x.categoryDescription }).Where(x => x.categoryID == id), JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public void AddCategory(Category cat)
        {
          
            try
            {
                catrepo.AddCategory(cat);
            }
            catch (System.Exception)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                HttpContext.Response.ContentType = "text/plain";
                HttpContext.Response.Write("لم يتم الاضافة");
            }


        }

        [HttpPost]
        public void UpdateCategory(Category cat, int id)
        {

            try
            {
                cat.categoryID = id;
                catrepo.UpdateCategory(cat);
            }
            catch (System.Exception)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                HttpContext.Response.ContentType = "text/plain";
                HttpContext.Response.Write("لم يتم التعديل");
            }
        }

        [HttpPost]
        public void DeleteCategory(int id)
        {
            try
            {
                catrepo.DeletCategory(id);

            }
            catch (System.Exception)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                HttpContext.Response.ContentType = "text/plain";
                HttpContext.Response.Write("لم يتم الحذف");
            }
        }
    }
}