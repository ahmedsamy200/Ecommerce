using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class OrdersController : Controller
    {
        OrderRepository OrderRepo = new OrderRepository();
        OrderDetailsRepository OrderDetailsRepo = new OrderDetailsRepository();

        public ActionResult OrderDetails(int orderID)
        {
            if (Session["AdminID"] != null)
            {
                var orderDetailsVM = OrderDetailsRepo.GetOrderDetails(orderID);
                return View(orderDetailsVM);
            }
            return RedirectToAction("Index", "Shop");
        }

        public JsonResult GetAllOreders()
        {           
            IEnumerable<OrderVM> ordersVM = OrderRepo.GetAllOreders();
            return Json(ordersVM.ToList(), JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetUserOreders()
        {
            var userID = (int)Session["UserID"];
            IEnumerable<OrderVM> ordersVM = OrderRepo.GetUserOrders(userID);
            return Json(ordersVM.ToList(), JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult ConfirmShipping(int orderID)
        {
            try
            {
                var result = OrderRepo.ConfirmShipping(orderID);
                if (result)
                {
                    return Json("Success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("failed", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {

                throw;
            }


        }

        [HttpPost]
        public JsonResult ConfirmDelivered(int orderID)
        {
            try
            {
                var result = OrderRepo.ConfirmDelivered(orderID);
                if (result)
                {
                    return Json("Success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("failed", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {

                throw;
            }


        }

        [HttpPost]
        public JsonResult DeleteOrder(int orderID)
        {
            try
            {
                var result = OrderRepo.DeleteOrder(orderID);
                if (result)
                {
                    return Json("Success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("failed", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {

                throw;
            }
            

        }
    }
}