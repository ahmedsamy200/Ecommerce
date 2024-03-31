using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using DataModel;

namespace DAL
{
    interface IProductRepository
    {
        bool AddProduct(ProductVM prod);
        bool UpdateProduct(ProductVM NewProd);
        bool DeleteProduct(int id);
        ProductVM GetProductByID(int id);
        IEnumerable<ProductVM> GetProductsByCategory(int? categoryID);
        IEnumerable<ProductVM> GetProductsByPrice(int? CategoryID, int minPrice, int maxPrice);
        IEnumerable<ProductVM> GetAllProducts();
        IEnumerable<ProductVM> GetTopSaleProducts();
        IEnumerable<ProductVM> GetFeaturedProducts();
    }

    public class ProductRepository : IProductRepository
    {
        EcommerceEntities db = new EcommerceEntities();

        public bool AddProduct(ProductVM prod)
        {
            try
            {
                Product obj = ConvertProductVMToProduct(prod);
                db.Products.Add(obj);
                db.SaveChanges();
                return true;
            }
            catch
            {

                return false;
            }

        }

        public bool UpdateProduct(ProductVM NewProd)
        {
            try
            {
                Product product = db.Products.FirstOrDefault(x => x.productID == NewProd.productID);
                if (product != null)
                {
                    ChangeOldProductValues(NewProd);
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

        public bool DeleteProduct(int id)
        {
            try
            {

                Product product = db.Products.FirstOrDefault(x => x.productID == id);
                if (product != null)
                {
                    db.Products.Remove(product);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }

        }

        public IEnumerable<ProductVM> GetAllProducts()
        {
            try
            {
                IEnumerable<Product> AllProds = db.Products;
                IEnumerable<ProductVM> result = ConvertAllProductsToProductsVM(AllProds);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public ProductVM GetProductByID(int id)
        {
            try
            {
                Product prod = db.Products.FirstOrDefault(x => x.productID == id);
                ProductVM prodVM = ConvertProductToProductVM(prod);
                return prodVM;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private ProductVM ConvertProductToProductVM(Product prod)
        {
            try
            {
                ProductVM obj = new ProductVM();
                var results = CalcAverageRating(prod.productID);
                obj.productID = prod.productID;
                obj.prodName = prod.prodName;
                obj.prodDescription = prod.prodDescription;
                obj.price = prod.price;
                obj.inStock = prod.inStock;
                obj.discount = prod.discount;
                obj.img = prod.img;
                obj.isFeature = prod.isFeature;
                obj.categoryID = prod.categoryID;
                obj.CategoryName = prod.Category.categoryName;
                obj.rank = prod.rank;
                obj.totalStars = results.finalResult;
                obj.oneTotal = results.oneTotal;
                obj.twoTotal = results.twoTotal;
                obj.threeTotal = results.threeTotal;
                obj.fourTotal = results.fourTotal;
                obj.fiveTotal = results.fiveTotal;
                obj.starOneAvg = results.starOneAvg;
                obj.starTwoAvg = results.starTwoAvg;
                obj.starThreeAvg = results.starThreeAvg;
                obj.starFourAvg = results.starFourAvg;
                obj.starFiveAvg = results.starFiveAvg;
                obj.avgStar = results.avgStar;
                obj.numberOfReviews = CountOfReviews(obj.productID);
                obj.countComments = CountOfComments(obj.productID);

                return obj;
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        private Product ConvertProductVMToProduct(ProductVM prod)
        {
            try
            {
                Product obj = new Product();
                obj.prodName = prod.prodName;
                obj.prodDescription = prod.prodDescription;
                obj.price = prod.price;
                obj.inStock = prod.inStock;
                obj.discount = prod.discount;
                obj.img = prod.img;
                obj.isFeature = prod.isFeature;
                obj.categoryID = prod.categoryID;
                obj.rank = prod.rank;
                return obj;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private IEnumerable<ProductVM> ConvertAllProductsToProductsVM()
        {
            try
            {
                List<ProductVM> prods = new List<ProductVM>();
                foreach (var item in db.Products)
                {
                    ProductVM obj = new ProductVM();
                    obj.prodName = item.prodName;
                    obj.prodDescription = item.prodDescription;
                    obj.price = item.price;
                    obj.inStock = item.inStock;
                    obj.discount = item.discount;
                    obj.img = item.img;
                    obj.isFeature = item.isFeature;
                    obj.categoryID = item.categoryID;
                    obj.rank = item.rank;
                    prods.Add(obj);
                }
                return prods;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private IEnumerable<ProductVM> ConvertAllProductsToProductsVM(IEnumerable<Product> prods)
        {
            try
            {
                List<ProductVM> MyProds = new List<ProductVM>();
                foreach (var item in prods)
                {
                    ProductVM obj = new ProductVM();
                    obj.productID = item.productID;
                    obj.prodName = item.prodName;
                    obj.CategoryName = item.Category.categoryName;
                    obj.prodDescription = item.prodDescription;
                    obj.price = item.price;
                    obj.inStock = item.inStock;
                    obj.discount = item.discount;
                    obj.img = item.img;
                    obj.isFeature = item.isFeature;
                    obj.categoryID = item.categoryID;
                    obj.rank = item.rank;
                    obj.totalStars = CalcAverageRating(obj.productID).finalResult;
                    obj.oneTotal = CalcAverageRating(obj.productID).oneTotal;
                    obj.twoTotal = CalcAverageRating(obj.productID).twoTotal;
                    obj.threeTotal = CalcAverageRating(obj.productID).threeTotal;
                    obj.fourTotal = CalcAverageRating(obj.productID).fourTotal;
                    obj.fiveTotal = CalcAverageRating(obj.productID).fiveTotal;
                    obj.avgStar = CalcAverageRating(obj.productID).avgStar;
                    obj.numberOfReviews = CountOfReviews(obj.productID);
                    MyProds.Add(obj);
                }
                return MyProds;
            }
            catch (Exception)
            {

                throw;
            }

          
        }

        private void ChangeOldProductValues(ProductVM NewProd)
        {
            try
            {
                Product product = db.Products.FirstOrDefault(x => x.productID == NewProd.productID);
                product.prodName = NewProd.prodName;
                product.prodDescription = NewProd.prodDescription;
                product.price = NewProd.price;
                product.inStock = NewProd.inStock;
                product.discount = NewProd.discount;
                product.img = NewProd.img;
                product.isFeature = NewProd.isFeature;
                product.categoryID = NewProd.categoryID;
                product.rank = NewProd.rank;
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public IEnumerable<ProductVM> GetTopSaleProducts()
        {
            try
            {
                IEnumerable<Product> topSale = db.Products.OrderByDescending(x => x.rank).Take(8);
                IEnumerable<ProductVM> topSaleVM = ConvertAllProductsToProductsVM(topSale);
                return topSaleVM;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public IEnumerable<ProductVM> GetFeaturedProducts()
        {
            try
            {
                IEnumerable<Product> featuredProds = db.Products.Where(x => x.isFeature == true).OrderByDescending(x=>x.productID);
                IEnumerable<ProductVM> featuredProdsVM = ConvertAllProductsToProductsVM(featuredProds);

                return featuredProdsVM;
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        private int CountOfComments(int prodID)
        {
            try
            {
                var result = db.ProductReviews.Where(x => x.productID == prodID && x.comment != null).ToList();
                int CountComments = result.Count();
                if (CountComments > 0)
                    return CountComments;
                else
                    return 0;
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        private int? CountOfReviews(int prodID)
        {
            try
            {
                var result = db.ProductReviews.Where(x => x.productID == prodID);
                int count = result.Count();
                return count;
            }
            catch (Exception)
            {

                throw;
            }
            

        }

        public ProductReviewVM CalcAverageRating(int prodID)
        {
            try
            {
                var result = db.ProductReviews.Where(x => x.productID == prodID);
                List<byte?> mylist = new List<byte?>();
                int count = result.Count();
                int? overallRating = 0;
                double? percPerOne = 0;
                double? allUsersReviews = 0;
                ProductReviewVM obj = new ProductReviewVM();
                if (count > 0)
                {
                    foreach (var item in result.OrderBy(x => x.rating))
                    {

                        obj.rating = item.rating;
                        if (mylist.Contains(item.rating))
                        {
                            continue;
                        }
                        else
                        {
                            overallRating += obj.rating * result.Where(x => x.rating == obj.rating).Count();
                            mylist.Add(obj.rating);
                        }
                    }
                    obj.oneTotal = (byte)result.Where(x => x.rating == 1).Count();
                    obj.twoTotal = (byte)result.Where(x => x.rating == 2).Count();
                    obj.threeTotal = (byte)result.Where(x => x.rating == 3).Count();
                    obj.fourTotal = (byte)result.Where(x => x.rating == 4).Count();
                    obj.fiveTotal = (byte)result.Where(x => x.rating == 5).Count();
                    allUsersReviews = obj.oneTotal + obj.twoTotal + obj.threeTotal + obj.fourTotal + obj.fiveTotal;
                    obj.avgStar = (double)(overallRating) / count;
                    obj.finalResult = (obj.avgStar / 5) * 100;

                    //  calculate the percentage of each number
                    percPerOne = (100 / allUsersReviews);
                    obj.starOneAvg = (percPerOne * obj.oneTotal);
                    obj.starTwoAvg = (percPerOne * obj.twoTotal);
                    obj.starThreeAvg = (percPerOne * obj.threeTotal);
                    obj.starFourAvg = (percPerOne * obj.fourTotal);
                    obj.starFiveAvg = (percPerOne * obj.fiveTotal);

                    return obj;
                }
                else
                {
                    obj.oneTotal = 0;
                    obj.twoTotal = 0;
                    obj.threeTotal = 0;
                    obj.fourTotal = 0;
                    obj.fiveTotal = 0;
                    obj.avgStar = 0;
                    obj.finalResult = 0;
                    obj.starOneAvg = 0;
                    obj.starTwoAvg = 0;
                    obj.starThreeAvg = 0;
                    obj.starFourAvg = 0;
                    obj.starFiveAvg = 0;

                    return obj;
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }
                          
        public IEnumerable<ProductVM> GetProductsByCategory(int? categoryID)
        {
            try
            {
                IEnumerable<Product> AllProds = db.Products.Where(x => x.categoryID == categoryID);
                IEnumerable<ProductVM> result = ConvertAllProductsToProductsVM(AllProds);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public IEnumerable<ProductVM> GetProductsByPrice(int? CategoryID, int minPrice, int maxPrice)
        {
            try
            {
                if (CategoryID == 0)
                {
                    IEnumerable<Product> AllProds = db.Products.Where(x => x.price - (x.discount / 100 * x.price) >= minPrice && x.price - (x.discount / 100 * x.price) <= maxPrice);
                    IEnumerable<ProductVM> result = ConvertAllProductsToProductsVM(AllProds);
                    return result;
                }
                else
                {
                    IEnumerable<Product> AllProds = db.Products.Where(x => x.categoryID == CategoryID && x.price - (x.discount / 100 * x.price) >= minPrice && x.price - (x.discount / 100 * x.price) <= maxPrice);
                    IEnumerable<ProductVM> result = ConvertAllProductsToProductsVM(AllProds);
                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
            

        }

        public IEnumerable<ProductVM> GetProductsByRating(int? CategoryID, int minPrice, int maxPrice , int rate)
        {
            try
            {
                if (CategoryID == 0)
                {
                    IEnumerable<Product> AllProds = db.Products.Where(x => x.price - (x.discount / 100 * x.price) >= minPrice && x.price - (x.discount / 100 * x.price) <= maxPrice);
                    List<ProductVM> MyProds = new List<ProductVM>();
                    foreach (var item in AllProds)
                    {
                        ProductVM obj = new ProductVM();
                        obj.productID = item.productID;
                        obj.prodName = item.prodName;
                        obj.CategoryName = item.Category.categoryName;
                        obj.prodDescription = item.prodDescription;
                        obj.price = item.price;
                        obj.inStock = item.inStock;
                        obj.discount = item.discount;
                        obj.img = item.img;
                        obj.isFeature = item.isFeature;
                        obj.categoryID = item.categoryID;
                        obj.rank = item.rank;
                        obj.totalStars = CalcAverageRating(obj.productID).finalResult;
                        obj.oneTotal = CalcAverageRating(obj.productID).oneTotal;
                        obj.twoTotal = CalcAverageRating(obj.productID).twoTotal;
                        obj.threeTotal = CalcAverageRating(obj.productID).threeTotal;
                        obj.fourTotal = CalcAverageRating(obj.productID).fourTotal;
                        obj.fiveTotal = CalcAverageRating(obj.productID).fiveTotal;
                        obj.avgStar = CalcAverageRating(obj.productID).avgStar;
                        obj.numberOfReviews = CountOfReviews(obj.productID);
                        if (obj.avgStar >= rate)
                        {
                            MyProds.Add(obj);
                        }

                    }
                    return MyProds;
                }
                else
                {
                    IEnumerable<Product> AllProds = db.Products.Where(x => x.categoryID == CategoryID && x.price - (x.discount / 100 * x.price) >= minPrice && x.price - (x.discount / 100 * x.price) <= maxPrice);
                    List<ProductVM> MyProds = new List<ProductVM>();
                    foreach (var item in AllProds)
                    {
                        ProductVM obj = new ProductVM();
                        obj.productID = item.productID;
                        obj.prodName = item.prodName;
                        obj.CategoryName = item.Category.categoryName;
                        obj.prodDescription = item.prodDescription;
                        obj.price = item.price;
                        obj.inStock = item.inStock;
                        obj.discount = item.discount;
                        obj.img = item.img;
                        obj.isFeature = item.isFeature;
                        obj.categoryID = item.categoryID;
                        obj.rank = item.rank;
                        obj.totalStars = CalcAverageRating(obj.productID).finalResult;
                        obj.oneTotal = CalcAverageRating(obj.productID).oneTotal;
                        obj.twoTotal = CalcAverageRating(obj.productID).twoTotal;
                        obj.threeTotal = CalcAverageRating(obj.productID).threeTotal;
                        obj.fourTotal = CalcAverageRating(obj.productID).fourTotal;
                        obj.fiveTotal = CalcAverageRating(obj.productID).fiveTotal;
                        obj.avgStar = CalcAverageRating(obj.productID).avgStar;
                        obj.numberOfReviews = CountOfReviews(obj.productID);
                        if (obj.avgStar >= rate)
                        {
                            MyProds.Add(obj);
                        }

                    }
                    return MyProds;
                }
            }
            catch (Exception)
            {

                throw;
            }
          

        }
    }
}
