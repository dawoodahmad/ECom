namespace ECom.Api.Search.Interfaces
{
    public interface IOrderService
    {
        Task<(bool IsSuccess, IEnumerable<Models.Order> orders, string ErrorMessage)> GetOrderAsync(int customerId);
    }
}
