using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Web.Models;
using Store.Web.Services.Contracts;
using System.Diagnostics;

namespace Store.Web.Controllers;

public class HomeController : Controller
{
    private readonly IProductService _productService;

    public HomeController(IProductService productService)
    {
        _productService = productService;
    }

    //[Authorize]
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllProducts(string.Empty);

        if (products is null) return View("Error");

        return View(products);
    }

    public async Task<IActionResult> ProductDetails(int id)
    {
        var product = await _productService.GetProductById(id, string.Empty);

        if (product is null) return View("Error");

        return View(product);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [Authorize]
    public async Task<ActionResult> Login()
    {
        var acessToken = await HttpContext.GetTokenAsync("access_token");

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Logout()
    {
        return SignOut("Cookies", "oidc");
    }
}