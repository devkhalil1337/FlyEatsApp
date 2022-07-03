using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlyEatsApp.Models;
using FlyEatsApp.Providers;

namespace FlyEatsApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductVariantsController : ControllerBase
    {

        [HttpPost]
        public long AddNewProductVariants([FromBody] ProductVariants productVariants)
        {
            ProductVariantsProvider productVariantsProvider = new ProductVariantsProvider();
            var result = productVariantsProvider.AddNewProductVariants(productVariants);
            return result;
        }

        [HttpPut]
        public Boolean UpdateProductVariant([FromBody] ProductVariants productVariants) 
        {
            ProductVariantsProvider productVariantsProvider = new ProductVariantsProvider();
            var result = productVariantsProvider.UpdateProductVariant(productVariants);
            return result;
        }

        [HttpGet]
        public IEnumerable<ProductVariants> GetAllProductVariants (int productId)
        {
            ProductVariantsProvider productVariantsProvider = new ProductVariantsProvider();
            var result = productVariantsProvider.GetAllProductVariants(productId);
            return result;
        }

        [HttpPost]
        public Boolean DeleteProductVariantById([FromBody] int variantId)
        {
            ProductVariantsProvider productVariantsProvider = new ProductVariantsProvider();
            var result = productVariantsProvider.DeleteProductVariantById(variantId);
            return result;
        }
    }
}
