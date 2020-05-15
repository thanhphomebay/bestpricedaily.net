using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bestpricedaily.Paypal
{
    public class ItemBought
    {
        public int quantity { get; set; }
        public Guid id { get; set; }
    }
    public class ListItemBought
    {
        public ItemBought[] items { get; set; }
    }


    public class UnitAmount
    {
        public string currency_code { get; set; }
        public float value { get; set; }
    }
    public class PaypalOrderPurchaseUnitItem
    {
        public string name { get; set; }
        public string description { get; set; }
        public string sku { get; set; }        
        public int quantity { get; set; }
        public UnitAmount unit_amount { get; set; }
        //public UnitAmount tax { get; set; }

    }

    public class PaypalOrderPurchaseUnitAmountBreakdown
    {
        public UnitAmount item_total { get; set; }
        public UnitAmount shipping { get; set; }
        public UnitAmount tax_total { get; set; }
    }
    public class PaypalOrderPurchaseUnitAmount
    {
        public string currency_code { get; set; }
        public float value { get; set; }
        public PaypalOrderPurchaseUnitAmountBreakdown breakdown { get; set; }
    }
    public class PaypalOrderPurchaseUnit
    {
        public PaypalOrderPurchaseUnit()
        {

        }
        public string invoice_id { get; set; }
        public PaypalOrderPurchaseUnitAmount amount { get; set; }
        public PaypalOrderPurchaseUnitItem[] items { get; set; }
    }
    public class PaypalOrderApplicationContext
    {
        public string return_url { get; set; }
        public string cancel_url { get; set; }
        public string brand_name { get; set; }
        // "locale": "fr-FR",
        public string user_action { get; set; }
    }
    public class PaypalOrder
    {
        public string intent { get; set; }
        public PaypalOrderApplicationContext application_context { get; set; }
        public PaypalOrderPurchaseUnit[] purchase_units { get; set; }
    }
    public class PaypalAccessTokenResponse
    {
        public string scope { get; set; }
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string app_id { get; set; }
        public int expires_in { get; set; }
    }
}
