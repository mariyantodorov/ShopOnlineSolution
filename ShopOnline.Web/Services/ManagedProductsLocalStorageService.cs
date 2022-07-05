using Blazored.LocalStorage;
using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Services
{
    public class ManagedProductsLocalStorageService : IManagedProductsLocalStorageService
    {
        private readonly ILocalStorageService localStorageService;
        private readonly IProductService productService;

        private const string KEY = "ProductCollection";

        public ManagedProductsLocalStorageService(ILocalStorageService localStorageService, IProductService productService)
        {
            this.localStorageService = localStorageService;
            this.productService = productService;
        }

        public async Task<IEnumerable<ProductDto>> GetCollection()
        {
            return await this.localStorageService.GetItemAsync<IEnumerable<ProductDto>>(KEY)
                ?? await AddCollection();
        }

        public async Task RemoveCollection()
        {
            await this.localStorageService.RemoveItemAsync(KEY);
        }

        private async Task<IEnumerable<ProductDto>> AddCollection()
        {
            var productCollection = await this.productService.GetItems();

            if (productCollection != null)
            {
                await this.localStorageService.SetItemAsync(KEY, productCollection);
            }

            return productCollection;
        }
    }
}
