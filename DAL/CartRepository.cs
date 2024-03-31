using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using DataModel;

namespace DAL
{
    interface ICartRepository
    {
        bool AddToCart(Cart item);        
        IEnumerable<CartVM> GetUserCart(int? userID);
        IEnumerable<CartVM> GetUserFavorate(int? userID);
        bool CheackIfProductInCartAndUpdateQuantity(Cart item);
        bool CheackIfProductInFavorateAndDelete(Cart item);
        bool UpdateQuantity(int userID ,int prodID ,int newQuantity);
        bool DeleteFromCart(int userID, int prodID);
        double CalcTotalPrice(int userID);
        int? CountItemInCart(int userID);
        int? CountItemInFavorate(int userID);
    }
    public class CartRepository : ICartRepository
    {
        EcommerceEntities db = new EcommerceEntities();

        public bool AddToCart(Cart item)
        {
            try
            {
                item.timeAdded = DateTime.Now;
                db.Carts.Add(item);
                db.SaveChanges();
                return true;
            }
            catch
            {

                return false;
            }
        }

        public bool CheackIfProductInCartAndUpdateQuantity(Cart item)
        {
            try
            {
                var result = db.Carts.Where(x => x.userID == item.userID && x.productID == item.productID && x.savedForLater == false).FirstOrDefault();
                if (result != null)
                {
                    result.quantity = result.quantity + item.quantity;
                    db.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public bool CheackIfProductInFavorateAndDelete(Cart item)
        {
            try
            {
                var result = db.Carts.Where(x => x.userID == item.userID && x.productID == item.productID && x.savedForLater == true).FirstOrDefault();
                if (result != null)
                {
                    db.Carts.Remove(result);
                    db.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public IEnumerable<CartVM> GetUserCart(int? userID)
        {
            try
            {
                var prods = db.Carts.Where(x => x.userID == userID && x.savedForLater == false).ToList();
                List<CartVM> cart = new List<CartVM>();
                foreach (var item in prods)
                {
                    CartVM obj = new CartVM();
                    obj.productID = item.productID;
                    obj.prodImg = item.Product.img;
                    obj.prodName = item.Product.prodName;
                    obj.CategoryName = item.Product.Category.categoryName;
                    obj.quantity = item.quantity;
                    obj.discount = item.Product.discount;
                    obj.price = (item.Product.price - (obj.discount / 100 * item.Product.price));
                    obj.TotalPrice = (item.Product.price - (obj.discount / 100 * item.Product.price)) * item.quantity;
                    cart.Add(obj);
                }
                return cart;
            }
            catch (Exception)
            {

                throw;
            }
          
        }

        public IEnumerable<CartVM> GetUserFavorate(int? userID)
        {
            try
            {
                var prods = db.Carts.Where(x => x.userID == userID && x.savedForLater == true).ToList();
                List<CartVM> cart = new List<CartVM>();
                foreach (var item in prods)
                {
                    CartVM obj = new CartVM();
                    obj.productID = item.productID;
                    obj.prodImg = item.Product.img;
                    obj.prodName = item.Product.prodName;
                    obj.discount = item.Product.discount;
                    obj.price = (item.Product.price - (obj.discount / 100 * item.Product.price));

                    obj.CategoryName = item.Product.Category.categoryName;
                    cart.Add(obj);
                }
                return cart;
            }
            catch (Exception)
            {

                throw;
            }
          
        }

        public bool UpdateQuantity(int userID ,int prodID , int newQuantity)
        {
            try
            {
                Cart obj = db.Carts.FirstOrDefault(x => x.userID == userID && x.productID == prodID && x.savedForLater == false);
                obj.quantity = newQuantity;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public bool DeleteFromCart(int userID, int prodID)
        {
            try
            {
                Cart obj = db.Carts.FirstOrDefault(x => x.userID == userID && x.productID == prodID && x.savedForLater == false);
                db.Carts.Remove(obj);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
           
        }


        public double CalcTotalPrice(int userID)
        {
            try
            {
                var prods = db.Carts.Where(x => x.userID == userID).ToList();
                double TotalPrice = 0;
                foreach (var item in prods)
                {
                    TotalPrice += (double)(item.Product.price * item.quantity);
                }
                return TotalPrice;
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        private Cart ConvertCartVMToCart(CartVM item)
        {
            Cart obj = new Cart();
            obj.quantity = item.quantity;
            obj.productID = item.productID;
            obj.savedForLater = item.savedForLater;
            obj.timeAdded = DateTime.Now;
            obj.userID = item.userID;
            return obj;
        }

        public int? CountItemInCart(int userID)
        {
            try
            {
                var count = db.Carts.Where(x => x.userID == userID && x.savedForLater == false).Sum(x => x.quantity);
                return count;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int? CountItemInFavorate(int userID)
        {
            try
            {
                var count = db.Carts.Where(x => x.userID == userID && x.savedForLater == true).Sum(x => x.quantity);
                return count;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
