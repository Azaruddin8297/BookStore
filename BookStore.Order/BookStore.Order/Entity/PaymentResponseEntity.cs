namespace BookStore.Order.Entity
{
    public class PaymentResponseEntity
    {
        public string mihpayid { get; set; }
        public string txnid { get; set; }
        public string status { get; set; }
        public string error { get;  set; }
        public string error_message { get; set; }
        public string hash { get; set; }
        public string bank_ref_num { get; set; }
    }
}
