using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlyEatsApp.Models;
using FlyEatsApp.Providers;
using System.Net.Http.Headers;
using FlyEatsApp.Functions;

namespace FlyEatsApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        BusinessUnitsFunctions businessUnitsFunctions = new BusinessUnitsFunctions();
        [HttpPost]
        public object AddNewProduct([FromBody] Products product)
        {
            ProductsProvider productsProvider = new ProductsProvider();
            product.BusinessId = businessUnitsFunctions.GetBusinessIdFromHeaders(Request);
            var results = productsProvider.AddNewProduct(product);
            return results;
        }

        [HttpPut]
        public object UpdateProduct([FromBody] Products product)
        {
            ProductsProvider productsProvider = new ProductsProvider();
            product.BusinessId = businessUnitsFunctions.GetBusinessIdFromHeaders(Request);
            var result = productsProvider.UpdateProduct(product);
            return result;
        }

        [HttpGet]
        public IEnumerable<Products> GetProductsById(int productId)
        {
            ProductsProvider productsProvider = new ProductsProvider();
            var result = productsProvider.GetProductsById(productId);
            return result;
        }

        [HttpGet]
        public IEnumerable<Products> GetAllProducts()
        {
            BusinessUnitsFunctions businessUnitsFunctions = new BusinessUnitsFunctions();
            int businessId = businessUnitsFunctions.GetBusinessIdFromHeaders(Request);
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

        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("wwwroot", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
