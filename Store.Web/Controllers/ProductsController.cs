using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Web.Models;
using Store.Web.Roles;
using Store.Web.Services.Contracts;

namespace Store.Web.Controllers
{
    [Authorize(Roles = Role.Admin)]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index()
        {
            var token = await GetAccessToken();

            var products = await _productService.GetAllProducts(token);

            if (products is null)
                return View("Error");

            return View(products);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var token = await GetAccessToken();

            ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(token), 
                                                "Id", 
                                                "Name");

            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(ProductViewModel productViewModel)
        {
            var token = await GetAccessToken();
            if (ModelState.IsValid)
            {
                var productCreated = await _productService.CreateProduct(productViewModel, token);

                if (productCreated is not null)
                    return RedirectToAction(nameof(Index));
            }

            ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(token),
                                                "Id",
                                                "Name");
            return View(productViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            var token = await GetAccessToken();

            ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(token),
                                                "Id",
                                                "Name");

            var product = await _productService.GetProductById(id, token);

            if(product == null) return View("Error");

            return View(product);
        }

        [HttpPost]
        public async Task<ActionResult> Update(ProductViewModel productViewModel)
        {
            if(ModelState.IsValid)
            {
                string token = await GetAccessToken();

                var productUpdated = await _productService.UpdateProduct(productViewModel, token);

                if(productUpdated is not null) return RedirectToAction(nameof(Index));
            }

            return View(productViewModel);
        }

        [HttpGet]
        public async Task<ActionResult<ProductViewModel>> Delete(int id)
        {
            string token = await GetAccessToken();

            var productViewModel = await _productService.GetProductById(id, token);

            if (productViewModel is null) return View("Error");

            return View(productViewModel);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            string token = await GetAccessToken();

            var result = await _productService.DeleteProductbyId(id, token);

            if (result == false) return View("Error");

            return RedirectToAction("Index");
        }

        private async Task<string> GetAccessToken() => await HttpContext.GetTokenAsync("access_token");
    }
}