using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlyEatsApp.Models;
using FlyEatsApp.Providers;

namespace FlyEatsApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        [HttpPost]
        public long AddNewCategory([FromBody] Categories categories)
        {
            CategoriesProvider categoriesProvider = new CategoriesProvider();
            var result = categoriesProvider.AddNewCategory(categories);
            return result;
        }

       [HttpPut]
        public Boolean UpdateCategory([FromBody] Categories categories)
        {
            CategoriesProvider categoriesProvider = new CategoriesProvider();
            var result = categoriesProvider.UpdateCategory(categories);
            return result;
        }

        [HttpGet]
       public IEnumerable<Categories> GetCategoryById(int categoryId)
        {
            CategoriesProvider categoriesProvider = new CategoriesProvider();
            var result = categoriesProvider.GetCategoryById(categoryId);
           return result;
       }

        [HttpGet]
        public IEnumerable<Categories> GetAllCategories(int businessId)
        {
            CategoriesProvider categoriesProvider = new CategoriesProvider();
            var result = categoriesProvider.GetAllCategories(businessId);
            return result;
        }

        [HttpPost]
        public Boolean DeleteCategoryBy([FromBody] long categoryId)
        {
            CategoriesProvider categoriesProvider = new CategoriesProvider();
            var result = categoriesProvider.DeleteCategoryBy(categoryId);
           return result;
       }
    }
}
