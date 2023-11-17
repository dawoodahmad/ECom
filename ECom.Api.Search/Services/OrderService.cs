using ECom.Api.Search.Interfaces;
using ECom.Api.Search.Models;
using System.Diagnostics.Contracts;
using System.Text.Json;

namespace ECom.Api.Search.Services
{
    public class OrderService : IOrderService
    {
        private IHttpClientFactory httpClientFactory;
        private ILogger<OrderService> logger;

        public OrderService(IHttpClientFactory httpClientFactory, ILogger<OrderService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }
        public async Task<(bool IsSuccess, IEnumerable<Order> orders, string ErrorMessage)> GetOrderAsync(int customerId)
        {
            try
            {
                var client = httpClientFactory.CreateClient("OrdersService");
                var response = await client.GetAsync($"api/orders/{customerId}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<IEnumerable<Order>>(content, options);

                    return (true, result, response.ReasonPhrase);
                }
                return (false, null, null);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.ToString());
            }
        }
    }
}
