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
    public class CommentController : Controller
    {
        ProductReviewRepository ProductReviewRepo = new ProductReviewRepository();

        public JsonResult GetProductComments(int prodID)
        {
            try
            {
                IEnumerable<ProductReviewVM> result = ProductReviewRepo.GetProductReviews(prodID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public JsonResult AddComment(ProductReview item)
        {

            if (Session["UserID"] != null)
            {
                item.userID = (int)Session["UserID"];               
                var check = ProductReviewRepo.checkIfUserAddCommentBefor(item.userID , item.productID);
                if (check)
                {
                    return Json("AddedBefor", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var result = ProductReviewRepo.AddComment(item);
                    if (result)
                    {
                        return Json("success", JsonRequestBehavior.AllowGet);
                    }
                    return Json("fail", JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json("GoToLogin", JsonRequestBehavior.AllowGet);
            }

        }
    }
}