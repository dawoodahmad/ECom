using ECom.Api.Search.Models;

namespace ECom.Api.Search.Interfaces
{
    public interface IProductsService
    {
        Task<(bool IsSuccess, IEnumerable<Product>, string ErrorMessage)> GetProductsAsync();
    }
}
