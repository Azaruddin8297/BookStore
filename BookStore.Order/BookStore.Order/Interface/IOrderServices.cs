using BookStore.Order.Entity;

namespace BookStore.Order.Interface
{
    public interface IOrderServices
    {
        Task<string> PlaceOrder(string token, int userId, int bookId, int quantity);
        IEnumerable<OrderEntity> GetOrders(int userId);
        OrderEntity AfterPayment(string orderId);
    }
}
