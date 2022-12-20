using CarvedRock.Admin.Logic;
using CarvedRock.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarvedRock.Admin.Controllers;

public class ProductsController: Controller
{
    private readonly IProductLogic _logic;
    // public List<ProductModel> Products {get;set;}
    public ProductsController(IProductLogic logic)
    {
        // Products=GetSampleProducts();
        _logic=logic;
    }
    public async Task<IActionResult> Index()
    {
        var products=await _logic.GetAllProducts();
        return View(products);
    }
    public async Task<IActionResult> Details(int id)
    {
        var product=await _logic.GetProductById(id);
        return product ==null?NotFound():View(product);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("id, Name, escription, Price, IsActive")] ProductModel product)
    {
        if (ModelState.IsValid)
        {
            await _logic.AddNewProduct(product);
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }
    public async Task<IActionResult> Edit(int? id)
    {
        if(id==null)
        {
            return View("NotFound");
        }
        var productModel=await _logic.GetProductById(id.Value);
        if(productModel==null)
        {
            return View("Not Found");
        }
        return View(productModel);
    }
    public async Task<IActionResult> Delete(int? id)
    {
        if(id==null) return View("NotFound");

        var productModel = await _logic.GetProductById(id.Value);
        if(productModel==null)return View("NotFound");

        return View(productModel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _logic.RemoveProduct(id);
        return RedirectToAction(nameof(Index));
    }
    // private List<ProductModel>? GetSampleProducts()
    // {
    //     return new List<ProductModel>
    //     {
    //         new ProductModel {Id=1, Name="Trailblazer", Price=69.99M, IsActive=true, Description="Great support in this high-top to take you to great heights and trails."},
    //         new ProductModel {Id=2, Name="Coastliner", Price=49.99M, IsActive=true, Description="Easy in and out with this lightweight but ruggety shoe with great ventiliation to get something"},
    //         new ProductModel {Id=3, Name="Woodsman", Price=64.99M, IsActive=true, Description="All the insulation and support you need when wandering the rugget trails of the woods."},
    //         new ProductModel {Id=4, Name="Basecamp", Price=249.99M, IsActive=true, Description="Great insulation and plenty of room for 2 in this spacious but highly-portable tent."},
    //     };
    // }
}