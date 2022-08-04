using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlyEatsApp.Models;
using FlyEatsApp.Providers;

namespace FlyEatsApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        [HttpPost]
        public object AddNewProduct([FromBody] Products product)
        {
            ProductsProvider productsProvider = new ProductsProvider();
            var results = productsProvider.AddNewProduct(product);
            return results;
        }

        [HttpPut]
        public object UpdateProduct([FromBody] Products product)
        {
            var results = new Object();
            ProductsProvider productsProvider = new ProductsProvider();
            var result = productsProvider.UpdateProduct(product);
            if(result != null)
            {
                ProductVariantsProvider productVariantsProvider = new ProductVariantsProvider();
                results = productVariantsProvider.UpdateProductVariant(product.productVariants);
            }
            return results;
        }

        [HttpGet]
        public IEnumerable<Products> GetProductsById(int productId)
        {
            ProductsProvider productsProvider = new ProductsProvider();
            var result = productsProvider.GetProductsById(productId);
            return result;
        }

        [HttpGet]
        public IEnumerable<Products> GetAllProducts(int businessId)
        {
            ProductsProvider productsProvider = new ProductsProvider();
            var result = productsProvider.GetAllProducts(businessId);
            return result;
        }

        [HttpPost]
        public object DeleteProductById([FromBody] int productId)
        {
            ProductsProvider productsProvider = new ProductsProvider();
            var result = productsProvider.DeleteProductById(productId);
            return result;
        }
    }
}
