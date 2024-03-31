using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Helper;

namespace UI.Controllers
{
    public class ProductController : Controller
    {
        ProductRepository ProductRepo = new ProductRepository();

        public JsonResult GetAllProducts()
        {
            try
            {
                IEnumerable<ProductVM> Products = ProductRepo.GetAllProducts();
                return Json(Products.ToList().OrderByDescending(x => x.productID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public JsonResult GetPaggedData(int pageNumber, int pageSize)
        {
            try
            {
                //db.Configuration.ProxyCreationEnabled = false;
                IEnumerable<ProductVM> Products = ProductRepo.GetAllProducts();
                var pagedData = Pagination.PagedResult(Products, pageNumber, pageSize);
                return Json(pagedData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public JsonResult loadFilteredProducts(int? CategoryID, int minPrice, int maxPrice, int rate, int pageNumber, int pageSize)
        {
            try
            {
                IEnumerable<ProductVM> prods = ProductRepo.GetProductsByRating(CategoryID, minPrice, maxPrice, rate);
                var pagedData = Pagination.PagedResult(prods, pageNumber, pageSize);
                return Json(pagedData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
          
        }

        public JsonResult GetFeaturedProducts()
        {
            try
            {
                IEnumerable<ProductVM> FeaturedProducts = ProductRepo.GetFeaturedProducts().Take(6);
                return Json(FeaturedProducts.ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public JsonResult GetTopSaleProducts()
        {
            try
            {
                IEnumerable<ProductVM> TopSaleProducts = ProductRepo.GetTopSaleProducts().Take(8);
                return Json(TopSaleProducts.ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
          
        }

        public JsonResult GetProductByID(int id)
        {
            try
            {
                ProductVM Product = ProductRepo.GetProductByID(id);
                Product.avgStar = Math.Round(Product.avgStar, 1);
                return Json(Product, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
           
        }


    }
}