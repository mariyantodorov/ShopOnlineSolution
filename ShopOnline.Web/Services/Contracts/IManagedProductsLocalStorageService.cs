using ShopOnline.Models.Dtos;

namespace ShopOnline.Web.Services.Contracts
{
    public interface IManagedProductsLocalStorageService
    {
        Task<IEnumerable<ProductDto>> GetCollection();
        Task RemoveCollection();
    }
}
