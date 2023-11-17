using ECom.Api.Search.Interfaces;

namespace ECom.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private IOrderService orderService;

        public SearchService(IOrderService orderService)
        {
            this.orderService = orderService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var ordersResult = await orderService.GetOrderAsync(customerId);
            if (ordersResult.IsSuccess)
            {
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