namespace BookStore.Order.Entity
{
    public class PaymentRequestEntity
    {
        public string key { get; set; }
        public string txnid { get; set; }
        public string amount { get; set; }
        public string productinfo { get; set; }
        public string firstname { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string surl { get; set; }
        public string furl { get; set; }
        public string hash { get; set; }

    }
}
