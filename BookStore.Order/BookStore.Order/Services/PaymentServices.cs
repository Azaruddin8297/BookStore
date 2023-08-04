using BookStore.Order.Entity;
using BookStore.Order.Interface;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BookStore.Order.Services
{
    public class PaymentServices : IPaymentServices
    {
        private readonly IConfiguration _config;
        public PaymentServices(IConfiguration config)
        {
              _config = config;
        }
        public async Task<string> PayOrder(PaymentRequestEntity paymentRequest)
        {
            using(HttpClient client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(ToDictionary(paymentRequest));
                HttpResponseMessage response = await client.PostAsync(_config["post"],content);
                string responseContent = response.RequestMessage.RequestUri.ToString();
                return responseContent;
            }
           
        }
        private string GetHash(PaymentRequestEntity orderDetails, string merchantSalt)
        {
            string generatedHash = "";
            var hashString = $"{orderDetails.key}|{orderDetails.txnid}|{orderDetails.amount}|{orderDetails.productinfo}|{orderDetails.firstname}|{orderDetails.email}|||||||||||{merchantSalt}";

            using (var sha512 = SHA512.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(hashString);
                byte[] hashBytes = sha512.ComputeHash(bytes);

                foreach (byte hashByte in hashBytes)
                {
                    generatedHash += String.Format("{0:x2}", hashByte);
                }

                return generatedHash;
            }

        }
        private Dictionary<string, string> ToDictionary(PaymentRequestEntity paymentDetails)
        {
            string successUrl = _config["successUrl"];
            string failureUrl = _config["failureUrl"];
            string key = _config["key"];
            string salt = _config["salt"];

            paymentDetails.surl = successUrl;
            paymentDetails.furl = failureUrl;
            paymentDetails.key = key;
            paymentDetails.hash = GetHash(paymentDetails, salt);

            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

            var properties = typeof(PaymentRequestEntity).GetProperties();
            foreach (var property in properties)
            {
                var value = property.GetValue(paymentDetails).ToString();
                keyValuePairs.Add(property.Name, value);
            }
            return keyValuePairs;
        }
        public async Task<PaymentResponseEntity> GetPaymentStatus(Stream body)
        {
            string content = await new StreamReader(body).ReadToEndAsync();
            var parseData = HttpUtility.ParseQueryString(content);
            PaymentResponseEntity response = new()
            {
                txnid = parseData["txnid"],
                mihpayid = parseData["mihpayid"],
                status = parseData["status"],
                error = parseData["error"],
                error_message = parseData["error_message"],
                bank_ref_num = parseData["bank_ref_num"],
                hash = parseData["hash"]
            };
            return response;
        }
    }
}
