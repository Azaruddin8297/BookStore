using BookStore.Order.Entity;

namespace BookStore.Order.Interface
{
    public interface IPaymentServices
    {
         Task<string> PayOrder(PaymentRequestEntity paymentRequest);
         Task<PaymentResponseEntity> GetPaymentStatus(Stream body);

    }
}
