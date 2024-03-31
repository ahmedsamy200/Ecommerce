using BLL;
using DAL;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class CartController : Controller
    {
        CartRepository CartRepo = new CartRepository();

        public JsonResult GetUserCart()
        {
            try
            {
                int? userID = (int?)Session["UserID"];
                var Products = CartRepo.GetUserCart(userID);
                return Json(Products, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public JsonResult GetUserFavorate()
        {
            try
            {
                int? userID = (int?)Session["UserID"];
                var Products = CartRepo.GetUserFavorate(userID);
                return Json(Products, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public JsonResult CountItemInCart()
        {
            try
            {
                int userID = (int)Session["UserID"];
                var count = CartRepo.CountItemInCart(userID);
                if (count == null)
                {
                    count = 0;
                    return Json(count, JsonRequestBehavior.AllowGet);
                }
                return Json(count, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }


        }

        public JsonResult CountItemInFavorate()
        {
            try
            {
                int userID = (int)Session["UserID"];
                var count = CartRepo.CountItemInFavorate(userID);
                if (count == null)
                {
                    count = 0;
                    return Json(count, JsonRequestBehavior.AllowGet);
                }
                return Json(count, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }


        }

        public JsonResult AddToCart(Cart item)
        {
            try
            {
                if (Session["UserID"] != null)
                {
                    item.userID = (int)Session["UserID"];                    
                    var IsAdded = CartRepo.CheackIfProductInCartAndUpdateQuantity(item);
                    if (IsAdded)
                    {
                        return Json("success", JsonRequestBehavior.AllowGet);
                    }
                    var result = CartRepo.AddToCart(item);
                    if (result)
                    {
                        return Json("success", JsonRequestBehavior.AllowGet);
                    }
                    return Json("failed", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("GoToLogin", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {

                throw;
            }
            

        }

        public JsonResult AddToFavorate(Cart item)
        {
            try
            {
                CartVM obj = new CartVM();
                if (Session["UserID"] != null)
                {

                    item.userID = (int)Session["UserID"];
                    var IsFound = CartRepo.CheackIfProductInFavorateAndDelete(item);
                    if (IsFound)
                    {
                        // تم حذف العمصر من المفضله
                        obj.savedInFavorate = false;
                        obj.productID = item.productID;
                        return Json(obj, JsonRequestBehavior.AllowGet);
                    }
                    var result = CartRepo.AddToCart(item);
                    if (result)
                    {
                        // تم الاضافه الي المفضله
                        obj.savedInFavorate = true;
                        obj.productID = item.productID;

                        return Json(obj, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(obj, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    obj.savedInFavorate = false;
                    obj.userID = 0;
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {

                throw;
            }
            

        }
      
        public JsonResult UpdateQuantity(int prodID, int newQuantity)
        {
            try
            {
                int userID = (int)Session["UserID"];
                CartRepo.UpdateQuantity(userID, prodID, newQuantity);
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }          
        }

        public JsonResult DeleteFromCart(int prodID)
        {
            try
            {
                int userID = (int)Session["UserID"];
                var result = CartRepo.DeleteFromCart(userID, prodID);
                if (result)
                {
                   return Json("success", JsonRequestBehavior.AllowGet);
                }
                return Json("failed", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }           
        }

        public JsonResult CalcTotalPrice()
        {
            try
            {
                int userID = (int)Session["UserID"];
                double TotalPrice = CartRepo.CalcTotalPrice(userID);
                return Json(TotalPrice, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }           
        }

        public JsonResult CheakFavorate()
        {
            try
            {
                int userID = (int)Session["UserID"];
                var result = CartRepo.GetUserFavorate(userID);
                return Json(result.ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
            

        }

     

    }
}