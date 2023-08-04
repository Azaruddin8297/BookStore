using BookStore.Order.Entity;
using BookStore.Order.Interface;
using BookStore.Order.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Timers;

namespace BookStore.Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class orderController : ControllerBase
    {
        private readonly IOrderServices _order;
        public ResponseEntity response;
        private readonly IPaymentServices _payment;
        public orderController(IOrderServices order, IPaymentServices payment)
        {
            _order = order;
            response = new ResponseEntity();
            _payment = payment;
        }
        [Authorize]
        [HttpPost]
        [Route("Create")]
        public async Task<ResponseEntity> AddOrder(int bookId, int quantity)
        {
            string jwtTokenwithBearer = HttpContext.Request.Headers["Authorization"];
            string jwtToken = jwtTokenwithBearer.Substring("Bearer".Length);
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.Sid));
            string newOrder = await _order.PlaceOrder(jwtToken, userId, bookId, quantity);
            if (newOrder != null)
            {
                response.Data = newOrder;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Something went Wrong";
            }
            return response;
        }
        [Authorize]
        [HttpPost]
        [Route("GetOrders")]
        public ResponseEntity GetOrders()
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.Sid));
            IEnumerable<OrderEntity> orders = _order.GetOrders(userId);

            if (orders.Any())
            {
                response.Data = orders;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Something went Wrong";
            }
            return response;
        }
        
        [HttpPost]
        [Route("PaymentConfirmation")]
        public async Task<ResponseEntity> PaymentConfirmation()
        {
            Stream body = Request.Body;
            PaymentResponseEntity paymentResponse = await _payment.GetPaymentStatus(body);
            if (paymentResponse.status == "success")
            {
                OrderEntity updateOrder = _order.AfterPayment(paymentResponse.txnid);
                response.Data = paymentResponse;
                response.IsSuccess = true;
                response.Message = "payment is Successful";

            }
            else
            {
                response.IsSuccess = false;
                response.Message = "payment Unsuccessful";
            }
            return response;
        }

    }
}
