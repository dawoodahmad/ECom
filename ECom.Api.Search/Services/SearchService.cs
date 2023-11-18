using ECom.Api.Search.Interfaces;

namespace ECom.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private IOrdersService orderService;
        private IProductsService productsService;
        private ICustomersService customerService;

        public SearchService(IOrdersService orderService, IProductsService productsService, ICustomersService customersService)
        {
            this.orderService = orderService;
            this.productsService = productsService;
            this.customerService = customersService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var customersResult = await customerService.GetCustomerAsync(customerId);
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
                    Customer = customersResult.IsSuccess ? customersResult.Customer : new { Name = "Customer information is not available" },
                    Orders = ordersResult.orders
                };
                return (true, result);
            }

            return (false, null);
        }
    }
}