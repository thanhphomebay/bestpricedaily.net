using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bestpricedaily.Models
{
    using bestpricedaily.Misc.Repository;
    public class Order : BaseEntity
    {
        public string invoice_id { get; set; } //your generated invoice id
        
        public string order_id { get; set; } //from paypal
        public string paypal_payment_capture_id { get; set; }
        public string payer_id { get; set; } //from paypal
        public string surename { get; set; }
        public string givenname { get; set; }
        public string email { get; set; }
        public float total { get; set; }
      
    }
}
