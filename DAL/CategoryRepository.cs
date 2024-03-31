using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using DataModel;

namespace DAL
{

    interface ICategoryRepository
    {
        bool AddCategory(Category category);
        bool UpdateCategory(Category Newcategory);
        bool DeletCategory(int id);
        Category GetCategoryByID(int id);
        IEnumerable<Category> GetAllCategories();
    }

    public class CategoryRepository:ICategoryRepository
    {
        EcommerceEntities db = new EcommerceEntities();

        public bool AddCategory(Category category)
        {
            try
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool DeletCategory(int id)
        {
            try
            {
                var result = db.Categories.FirstOrDefault(x => x.categoryID == id);
                if (result != null)
                {
                    db.Categories.Remove(result);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {

                return false;
            }


        }

        public IEnumerable<Category> GetAllCategories()
        {
            try
            {
                List<Category> categories = new List<Category>();
                foreach (var item in db.Categories)
                {
                    Category obj = new Category();
                    obj.categoryID = item.categoryID;
                    obj.categoryName = item.categoryName;
                    obj.categoryDescription = item.categoryDescription;
                    obj.categoryImg = item.categoryImg;
                    categories.Add(obj);
                }
                return categories;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public Category GetCategoryByID(int id)
        {
            try
            {
                var result = db.Categories.FirstOrDefault(x => x.categoryID == id);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public bool UpdateCategory(Category Newcategory)
        {
            try
            {
                Category OldCategory = db.Categories.FirstOrDefault(x => x.categoryID == Newcategory.categoryID);
                if (OldCategory != null)
                {
                    OldCategory.categoryName = Newcategory.categoryName;
                    OldCategory.categoryDescription = Newcategory.categoryDescription;
                    OldCategory.categoryImg = Newcategory.categoryImg;
                    db.SaveChanges();
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
