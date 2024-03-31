using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using DAL;
using DataModel;
using System.IO;

namespace UI.Controllers
{
    public class AccountController : Controller
    {
        UserRepository UserRepo = new UserRepository();
        CartRepository CartRepo = new CartRepository();
        EcommerceEntities db = new EcommerceEntities();

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Profile()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Shop");
        }

        public ActionResult ChangePassword()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Shop");
        }

        public ActionResult Orders()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Shop");
            
        }

        public ActionResult Checkout(decimal totalOrderPrice)
        {
            if (Session["UserID"] != null)
            {
                Session["totalOrderPrice"] = totalOrderPrice;
                return View();
            }
            return RedirectToAction("Index", "Shop");
        }

        public ActionResult ForgetPassowrd()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Login");
        }

        public JsonResult CheckLogin(Customer data)
        {
            try
            {
                var customer = UserRepo.CheckLogin(data);
                if (customer != null)
                {
                    Session["UserID"] = customer.customerID;
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

        public JsonResult LoadUserData()
        {
            try
            {
                int userID = (int)Session["UserID"];
                var result = UserRepo.GetCustomerByID(userID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
           

        }

        [HttpPost]
        public JsonResult UpdateUserData(CustomerVM data)
        {
            try
            {
                data.customerID = (int)Session["UserID"];
                var IsValid = UserRepo.IsValidEmail(data.email, data.customerID);
                if (IsValid)
                {

                    var customer = UserRepo.UpdateCustomer(data);
                    if (customer)
                    {
                        return Json("Success", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("erorr", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("NotValid", JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception)
            {

                throw;
            }
            

        }

        [HttpPost]
        public JsonResult AddUser(CustomerVM data)
        {
            try
            {
                var customer = UserRepo.AddCustomer(data);
                if (customer)
                {
                    Session["UserID"] = db.Customers.Max(x => x.customerID);
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

        [HttpPost]
        public JsonResult ChangePassword(CustomerVM customer)
        {
            try
            {
                customer.customerID = (int)Session["UserID"];
                bool result = UserRepo.ChangePassword(customer);
                if (result)
                {
                    return Json("Success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("InCorrectCurresntPassword", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {

                throw;
            }

           

        }


        [HttpPost]
        public ActionResult Profile(HttpPostedFileBase file)
        {
            try
            {
                if (file != null && file.ContentLength > 0)
                {

                    // save img name in databsae 
                    string imageName = Path.GetFileName(file.FileName);
                    int customerID = (int)Session["UserID"];
                    UserRepo.UpdateUserImage(customerID, imageName);

                    //save img in users folder
                    string path = Path.Combine(Server.MapPath("~/Images/users"), Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                }
                else
                {
                    ViewBag.Message = "There Is No File";
                }
                return View();
            }
            catch (Exception)
            {

                throw;
            }
            
        }


        public JsonResult CheckOutProcess(Order data)
        {
            try
            {
                //1. add data in table orders
                data.userID = (int)Session["UserID"];
                data.orderDate = DateTime.Now;
                data.totalPrice = (decimal)Session["totalOrderPrice"];
                db.Orders.Add(data);
                db.SaveChanges();

                //2. add data in orderDetails
                OrderDetail obj = new OrderDetail();
                var userOrder = CartRepo.GetUserCart(data.userID);
                var orderID = db.Orders.Max(x => x.orderID);
                var neworderID = data.orderID;
                foreach (var item in userOrder)
                {
                    obj.orderID = orderID;
                    obj.ProductID = item.productID;
                    obj.price = item.price;
                    obj.quntity = item.quantity;
                    obj.total = item.price * item.quantity;
                    db.OrderDetails.Add(obj);
                    db.SaveChanges();
                }

                //3. delete items in user cart
                var userItems = db.Carts.Where(x => x.userID == data.userID && x.savedForLater == false).ToList();
                db.Carts.RemoveRange(userItems);
                db.SaveChanges();
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            catch
            {

                return Json("erorr", JsonRequestBehavior.AllowGet);
            }


        }

      
    }          
}