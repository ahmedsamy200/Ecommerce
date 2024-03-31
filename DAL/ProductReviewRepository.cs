using BLL;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    interface IProductReviewRepository
    {
        IEnumerable<ProductReviewVM> GetProductReviews(int prodID);
        bool AddComment(ProductReview item);
        bool checkIfUserAddCommentBefor(int? userID , int? productID);

    }

    public class ProductReviewRepository: IProductReviewRepository
    {
        EcommerceEntities db = new EcommerceEntities();

        public IEnumerable<ProductReviewVM> GetProductReviews(int prodID)
        {
            try
            {
                List<ProductReviewVM> Reviews = new List<ProductReviewVM>();
                var result = db.ProductReviews.Where(x => x.productID == prodID && x.comment != null).ToList();
                int CountComments = result.Count();
                foreach (var item in result)
                {
                    ProductReviewVM obj = new ProductReviewVM();
                    obj.customerImage = item.Customer.image;
                    obj.customerName = item.Customer.firstName;
                    DateTime? myCommentDate = item.commentDate;
                    obj.commentDate = myCommentDate.Value.ToString("dd MMMM ,yyyy", new System.Globalization.CultureInfo("ar-EG"));
                    obj.comment = item.comment;
                    obj.avgStar = (double)item.rating;
                    obj.finalResult = (obj.avgStar / 5) * 100;
                    obj.countComments = CountComments;
                    Reviews.Add(obj);
                }

                return Reviews;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public bool AddComment(ProductReview item)
        {
            try
            {
                item.commentDate = DateTime.Now;
                db.ProductReviews.Add(item);
                db.SaveChanges();
                return true;
            }
            catch
            {

                return false;
            }

        }

        public bool checkIfUserAddCommentBefor(int? userID, int? productID)
        {
            try
            {
                var result = db.ProductReviews.FirstOrDefault(x => x.userID == userID && x.productID == productID);
                if (result != null)
                {
                    return true;
                }
                else
                    return false;
            }
            catch
            {

                return false;
            }

        }

       
    }
}
