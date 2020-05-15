using System;

namespace bestpricedaily
{
    public class MyStoreSetting
    {
        public float Tax { get; set; }
        public float Shipping { get; set; }
        public string BaseAddress { get; set; }
        public int Port { get; set; }

        public string Paypal_client_id { get; set; }
        public string Paypal_client_secret { get; set; }
        public string Paypal_access_token_url { get; set; }
        public string Paypal_create_payment_url { get; set; }
        public string Paypal_capture_payment_url { get; set; }
        public string Paypal_error_url { get; set; }
        public string Paypal_cancel_url { get; set; }
        public string Paypal_success_url { get; set; }
    }
}