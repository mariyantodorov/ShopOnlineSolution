using Microsoft.AspNetCore.Components;
using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Pages
{
    public class ProductDetailsBase : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        public IProductService ProductService { get; set; }

        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        [Inject]
        public IManagedProductsLocalStorageService ManagedProductsLocalStorageService { get; set; }

        [Inject]
        public IManagedCartItemsLocalStorageService ManagedCartItemsLocalStorageService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public ProductDto Product { get; set; }

        public string ErrorMessage { get; set; }

        private List<CartItemDto> ShoppingCartItems { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ShoppingCartItems = await ManagedCartItemsLocalStorageService.GetCollection();
                Product = await GetProductById(Id);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }      
        }

        protected async Task AddToCart_Click(CartItemToAddDto cartItemToAddDto)
        {
            try
            {
                var cartItemToAdd = await ShoppingCartService.AddItem(cartItemToAddDto);

                if (cartItemToAdd != null)
                {
                    ShoppingCartItems.Add(cartItemToAdd);
                    await ManagedCartItemsLocalStorageService.SaveCollection(ShoppingCartItems);
                }

                NavigationManager.NavigateTo("/ShoppingCart");


            }
            catch (Exception)
            {
                //log exception
            }
        }

        private async Task<ProductDto> GetProductById(int id)
        {
            var productDtos = await ManagedProductsLocalStorageService.GetCollection();

            if (productDtos != null)
            {
                return productDtos.SingleOrDefault(p => p.Id == id);
            }

            return null;
        }
    }
}
