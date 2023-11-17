using ECom.Api.Search.Interfaces;
using ECom.Api.Search.Models;

namespace ECom.Api.Search.Services
{
    public class ProductsService : IProductsService
    {
        private IHttpClientFactory httpClientFactory;
        private ILogger<ProductsService> logger;

        public ProductsService(IHttpClientFactory httpClientFactory, ILogger<ProductsService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }
        public Task<(bool IsSuccess, IEnumerable<Product>, string ErrorMessage)> GetProductsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
