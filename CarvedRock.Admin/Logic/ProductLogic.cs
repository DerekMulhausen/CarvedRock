using CarvedRock.Admin.Models;
using CarvedRock.Admin.Repository;

namespace CarvedRock.Admin.Logic;

public class ProductLogic:IProductLogic
{
    private readonly ICarvedRockRepository _repo;

    public ProductLogic(ICarvedRockRepository repo)
    {
        _repo=repo;
    }

    public async Task<List<ProductModel>>GetAllProducts()
    {
        var products=await _repo.GetAllProductsAsync();
        return products.Select(ProductModel.FromProduct).ToList();
    }
    public async Task<ProductModel?> GetProductById(int id)
    {
        var product =await _repo.GetProductByIdAsync(id);
        return product == null?null:ProductModel.FromProduct(product);
    }
    public async Task AddNewProduct(ProductModel productToAdd)
    {
        var productToSave=productToAdd.ToProduct();
        await _repo.AddProductAsync(productToSave);
    }
    public async Task RemoveProduct(int id)
    {
        await _repo.RemoveProductAsync(id);
    }
    public async Task UpdateProduct(ProductModel productToUpdate)
    {
        var productToSave=productToUpdate.ToProduct();
        await _repo.UpdateProductAsync(productToSave);
    }
    // public async Task<ProductModel>InitializeProductModel()
    // {
    //     return new ProductModel
    //     {
    //         AvailableCategories=await GetAvailableCategoriesFromDb()
    //     };
    // }
    // public async Task GetAvailableCategories(ProductModel productModel)
    // {
    //     productModel.AvailableCategories=await GetAvailableCategoriesFromDb();
    // }

    // private async Task<List<SelectListItem>> GetAvailableCategoriesFromDb()
    // {
    //     var cats = await _repo.GetAllCategoriesAsync();
    //     var returnList = new List<SelectListItem> { new("None", "") };
    //     var availCatList = cats.Select(cat => new SelectListItem(cat.Name, cat.Id.ToString())).ToList();
    //     returnList.AddRange(availCatList);
    //     return returnList;
    // }
}