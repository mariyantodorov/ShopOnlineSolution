using Blazored.LocalStorage;
using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Services
{
    public class ManagedCartItemsLocalStorageService : IManagedCartItemsLocalStorageService
    {
        private readonly ILocalStorageService localStorageService;
        private readonly IShoppingCartService shoppingCartService;

        private const string KEY = "CartItemsCollection";

        public ManagedCartItemsLocalStorageService(ILocalStorageService localStorageService, IShoppingCartService shoppingCartService)
        {
            this.localStorageService = localStorageService;
            this.shoppingCartService = shoppingCartService;
        }

        public async Task<List<CartItemDto>> GetCollection()
        {
            return await this.localStorageService.GetItemAsync<List<CartItemDto>>(KEY)
                ?? await AddCollection();
        }

        public async Task RemoveCollection()
        {
            await this.localStorageService.RemoveItemAsync(KEY);
        }

        public async Task SaveCollection(List<CartItemDto> cartItemDtos)
        {
            await this.localStorageService.SetItemAsync(KEY, cartItemDtos);
        }

        private async Task<List<CartItemDto>> AddCollection()
        {
            var shoppingCartCollection = await this.shoppingCartService.GetItems(HardCoded.UserId);

            if (shoppingCartCollection != null)
            {
                await this.localStorageService.SetItemAsync(KEY, shoppingCartCollection);
            }

            return shoppingCartCollection;
        }
    }
}
