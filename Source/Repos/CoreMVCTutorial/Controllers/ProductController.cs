using CoreMVCTutorial.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoreMVCTutorial.Controllers
{
    public class ProductController : Controller
    {
        private List<Product> products = new List<Product>();

        public ProductController()
        {
            products = new List<Product>()
            {
                new Product { ProductID =1, Name ="Product 1", Category = "Category 1", Description ="Description 1", Price = 10m},
                new Product { ProductID =2, Name ="Product 2", Category = "Category 1", Description ="Description 2", Price = 20m},
                new Product { ProductID =3, Name ="Product 3", Category = "Category 1", Description ="Description 3", Price = 30m},
                new Product { ProductID =4, Name ="Product 4", Category = "Category 2", Description ="Description 4", Price = 40m},
                new Product { ProductID =5, Name ="Product 5", Category = "Category 2", Description ="Description 5", Price = 50m},
                new Product { ProductID =6, Name ="Product 6", Category = "Category 2", Description ="Description 6", Price = 50m}
            };
        }

        //[HttpGet("Product/Index")]
        public IActionResult Index() 
        {
            return View(products);
        }

        [HttpGet("Product/Details/{id:int}")]
        public async Task<ActionResult> Details(int id)
        {
            await Show();
            var productDetails = products.FirstOrDefault(prd => prd.ProductID == id);
            return View(productDetails);
        }


        private async Task<string> Show()
        {
            int a = 20;
            var tt = await System.Threading.Tasks.Task.Run(() => string.Empty);
            return tt;
        }
    }
}