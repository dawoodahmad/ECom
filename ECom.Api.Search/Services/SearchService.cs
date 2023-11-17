using ECom.Api.Search.Interfaces;

namespace ECom.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private IOrdersService orderService;
        private IProductsService productsService;

        public SearchService(IOrdersService orderService, IProductsService productsService)
        {
            this.orderService = orderService;
            this.productsService = productsService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var ordersResult = await orderService.GetOrderAsync(customerId);
            var productsResult = await productsService.GetProductsAsync();
            if (ordersResult.IsSuccess)
            {
                foreach (var order in ordersResult.orders)
                {
                    foreach (var item in order.Items)
                    {
                        item.ProductName = productsResult.IsSuccess ? 
                            productsResult.Products.FirstOrDefault(p => p.Id == item.ProductId)?.Name : 
                            "Product Informatoin is not available";
                    }
                }

                var result = new
                {
                    Orders = ordersResult.orders
                };
                return (true, result);
            }

            return (false, null);
        }
    }
}